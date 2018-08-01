using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace CTRL_ShutDown
{
	public partial class Form1 : Form
	{
		
		public Form1()
		{
			InitializeComponent();
		}

		// 这个结构体将会传递给API。使用StructLayout
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct TokPriv1Luid
		{
			public int Count;
			public long Luid;
			public int Attr;
		}

		// 导入的方法必须是static extern的，并且没有方法体。调用这些方法就相当于调用Windows API。
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GetCurrentProcess();

		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
		ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool ExitWindowsEx(int flg, int rea);


		// 以下定义了在调用WinAPI时需要的常数。这些常数通常可以从Platform SDK的包含文件（头文件）中找到
		internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
		internal const int TOKEN_QUERY = 0x00000008;
		internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
		internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
		internal const int EWX_SHUTDOWN = 0x00000001;
		//internal const int EWX_LOGOFF = 0x00000000;
		//internal const int EWX_REBOOT = 0x00000002;
		//internal const int EWX_FORCE = 0x00000004;
		internal const int EWX_POWEROFF = 0x00000008;
		//internal const int EWX_FORCEIFHUNG = 0x00000010;
		internal const int EWX_HYBRID_SHUTDOWN = 0x00400000;

     

		// 通过调用WinAPI实现关机，主要代码再最后一行ExitWindowsEx，这调用了同名的WinAPI，正好是关机用的。
		private static void DoExitWin(object o)
		{
            int flg = (int)o;
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            if (flg == 0 || flg == 1 || flg == 2)
            {
                CtrlShutDown(flg);
            }
            else
            {
                ok = ExitWindowsEx(flg, 0);
            }
			
			
		}
		private void btnShutDown_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(@"Are you sure you want to shut down your computer now？Please save all works and click 'YES', or 'NO'", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (dr == DialogResult.Yes)
			{
				DoExitWin(EWX_SHUTDOWN);
			}
		}

		private void btnCtrlShutDown_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(@"Are you sure you want to 'QUICK' shut down your computer now？Any unsaved works and changes may be lost, Please save all works and click 'YES', or 'NO'", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (dr == DialogResult.Yes)
			{
                ParameterizedThrFunc(ShutDown);
				//CtrlShutDown(ShutDown);//快速关机
			}

		}

        private void ParameterizedThrFunc(int index)
        {
            //帶有參數 執行Tread
            //只能帶一個參數 型態必須為object
            ParameterizedThreadStart myPar = new ParameterizedThreadStart(DoExitWin);
            Thread myThread = new Thread(myPar);
            myThread.IsBackground = true;
            myThread.Start(index);
        }

		private void btnHybirdboot_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(@"Are you sure you want to 'HYBRID BOOT' your computer now？Please save all works and click 'YES', or 'NO'", "message:Win8/Win8.1 have to enable the setting", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (dr == DialogResult.Yes)
			{
				DoExitWin(EWX_HYBRID_SHUTDOWN);//混和關機
			}
		}

		private void btnReBoot_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(@"Are you sure you want to 'QUICK' reboot your computer now？Any unsaved works and changes may be lost, Please save all works and click 'YES', or 'NO'", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (dr == DialogResult.Yes)
			{
                ParameterizedThrFunc(RESTART);
				//CtrlShutDown(RESTART);//reboot
			}
		}

        /// <summary> 获得关机所需要的权限</summary>
        /// <param name="Privilege">权限类型</param>
        [DllImport("ntdll.dll")]
        private static extern void RtlAdjustPrivilege(int Privilege, int NewValue, int NewThread, out bool OldValue);
        /// <summary>关闭计算机权限</summary>
        const int SE_SHUTDOWN_PRIVILEGE = 19;

        /// <summary>执行瞬间关机等操作</summary>
        /// <param name="ShutdownAction">关机动作</param>
        [DllImport("ntdll.dll")]
        private static extern void NtShutdownSystem(int ShutdownAction);
        /// <summary>关闭系统</summary>
        const int ShutDown = 0;
        /// <summary>重启系统</summary>
        const int RESTART = 1;
        /// <summary>切断电源(快速关机)</summary>
        const int POWEROFF = 2;
        /// <summary>关机操作 </summary>
        /// <param name="Index">操作类型：1关机、2重启动、3切断电源(快速关机)</param>
        private static void CtrlShutDown(object o)
        {
            int Index = (int)o;
            bool a;
            RtlAdjustPrivilege(SE_SHUTDOWN_PRIVILEGE, 1, 0, out a);    //取得关机权限            
            switch (Index)
            {
                case ShutDown://关机 
                    NtShutdownSystem(ShutDown);
                    break;
                case RESTART: //重启动
                    NtShutdownSystem(RESTART);
                    break;
                case POWEROFF: //切断电源(快速关机)
                    NtShutdownSystem(POWEROFF);
                    break;
            }
        }
        /// <summary>
        /// feedback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void linkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//System.Diagnostics.Process.Start("https://plus.google.com/u/0/105802546272481120917/posts/MikWjXyaVrb");
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "";
            proc.Start();
		}



	}
}

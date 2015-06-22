namespace CTRL_ShutDown
{
	partial class Form1
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
		/// 修改這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
            this.btnShutDown = new System.Windows.Forms.Button();
            this.btnCtrlShutDown = new System.Windows.Forms.Button();
            this.btnReBoot = new System.Windows.Forms.Button();
            this.btnHybirdboot = new System.Windows.Forms.Button();
            this.linkLbl = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnShutDown
            // 
            this.btnShutDown.Location = new System.Drawing.Point(49, 21);
            this.btnShutDown.Name = "btnShutDown";
            this.btnShutDown.Size = new System.Drawing.Size(125, 23);
            this.btnShutDown.TabIndex = 0;
            this.btnShutDown.Text = "Normal ShutDown";
            this.btnShutDown.UseVisualStyleBackColor = true;
            this.btnShutDown.Click += new System.EventHandler(this.btnShutDown_Click);
            // 
            // btnCtrlShutDown
            // 
            this.btnCtrlShutDown.Location = new System.Drawing.Point(49, 54);
            this.btnCtrlShutDown.Name = "btnCtrlShutDown";
            this.btnCtrlShutDown.Size = new System.Drawing.Size(125, 23);
            this.btnCtrlShutDown.TabIndex = 1;
            this.btnCtrlShutDown.Text = "Ctrl Quick ShutDown";
            this.btnCtrlShutDown.UseVisualStyleBackColor = true;
            this.btnCtrlShutDown.Click += new System.EventHandler(this.btnCtrlShutDown_Click);
            // 
            // btnReBoot
            // 
            this.btnReBoot.Location = new System.Drawing.Point(49, 87);
            this.btnReBoot.Name = "btnReBoot";
            this.btnReBoot.Size = new System.Drawing.Size(125, 23);
            this.btnReBoot.TabIndex = 1;
            this.btnReBoot.Text = "Ctrl Quick ReBoot";
            this.btnReBoot.UseVisualStyleBackColor = true;
            this.btnReBoot.Click += new System.EventHandler(this.btnReBoot_Click);
            // 
            // btnHybirdboot
            // 
            this.btnHybirdboot.Location = new System.Drawing.Point(49, 120);
            this.btnHybirdboot.Name = "btnHybirdboot";
            this.btnHybirdboot.Size = new System.Drawing.Size(125, 23);
            this.btnHybirdboot.TabIndex = 2;
            this.btnHybirdboot.Text = "HyBrid Boot";
            this.btnHybirdboot.UseVisualStyleBackColor = true;
            this.btnHybirdboot.Click += new System.EventHandler(this.btnHybirdboot_Click);
            // 
            // linkLbl
            // 
            this.linkLbl.AutoSize = true;
            this.linkLbl.Location = new System.Drawing.Point(162, 158);
            this.linkLbl.Name = "linkLbl";
            this.linkLbl.Size = new System.Drawing.Size(49, 12);
            this.linkLbl.TabIndex = 3;
            this.linkLbl.TabStop = true;
            this.linkLbl.Text = "Feedback";
            this.linkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 179);
            this.Controls.Add(this.linkLbl);
            this.Controls.Add(this.btnHybirdboot);
            this.Controls.Add(this.btnReBoot);
            this.Controls.Add(this.btnCtrlShutDown);
            this.Controls.Add(this.btnShutDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SHUTDOWN Helper v1.1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnShutDown;
		private System.Windows.Forms.Button btnCtrlShutDown;
		private System.Windows.Forms.Button btnReBoot;
		private System.Windows.Forms.Button btnHybirdboot;
		private System.Windows.Forms.LinkLabel linkLbl;
	}
}


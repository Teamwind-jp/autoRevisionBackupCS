namespace autoRevisionBackupCS
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.Button3 = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.Label4 = new System.Windows.Forms.Label();
			this.msg1 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.maxrev1 = new System.Windows.Forms.TextBox();
			this.Button1 = new System.Windows.Forms.Button();
			this.bkfolder1 = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.min1 = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.file1 = new System.Windows.Forms.TextBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// Button3
			// 
			this.Button3.Location = new System.Drawing.Point(402, 25);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(75, 23);
			this.Button3.TabIndex = 20;
			this.Button3.Text = "Log";
			this.Button3.UseVisualStyleBackColor = true;
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// Button2
			// 
			this.Button2.Location = new System.Drawing.Point(402, 77);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(75, 23);
			this.Button2.TabIndex = 19;
			this.Button2.Text = "View";
			this.Button2.UseVisualStyleBackColor = true;
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// Label4
			// 
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(11, 113);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(80, 12);
			this.Label4.TabIndex = 16;
			this.Label4.Text = "Max Revisions";
			// 
			// msg1
			// 
			this.msg1.AutoSize = true;
			this.msg1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.msg1.Location = new System.Drawing.Point(356, 178);
			this.msg1.Name = "msg1";
			this.msg1.Size = new System.Drawing.Size(37, 12);
			this.msg1.TabIndex = 17;
			this.msg1.Text = "0 revs";
			this.msg1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Label3
			// 
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(12, 61);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(107, 12);
			this.Label3.TabIndex = 18;
			this.Label3.Text = "Drop Backup Folder";
			// 
			// maxrev1
			// 
			this.maxrev1.Location = new System.Drawing.Point(12, 128);
			this.maxrev1.Name = "maxrev1";
			this.maxrev1.Size = new System.Drawing.Size(57, 19);
			this.maxrev1.TabIndex = 13;
			// 
			// Button1
			// 
			this.Button1.Location = new System.Drawing.Point(402, 173);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(75, 23);
			this.Button1.TabIndex = 14;
			this.Button1.Text = "Start";
			this.Button1.UseVisualStyleBackColor = true;
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// bkfolder1
			// 
			this.bkfolder1.AllowDrop = true;
			this.bkfolder1.Location = new System.Drawing.Point(12, 79);
			this.bkfolder1.Name = "bkfolder1";
			this.bkfolder1.Size = new System.Drawing.Size(384, 19);
			this.bkfolder1.TabIndex = 15;
			this.bkfolder1.DragDrop += new System.Windows.Forms.DragEventHandler(this.bkfolder1_DragDrop);
			this.bkfolder1.DragEnter += new System.Windows.Forms.DragEventHandler(this.bkfolder1_DragEnter);
			// 
			// Label2
			// 
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(11, 162);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(73, 12);
			this.Label2.TabIndex = 12;
			this.Label2.Text = "Interval mins.";
			// 
			// min1
			// 
			this.min1.Location = new System.Drawing.Point(12, 177);
			this.min1.Name = "min1";
			this.min1.Size = new System.Drawing.Size(57, 19);
			this.min1.TabIndex = 11;
			// 
			// Label1
			// 
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(12, 9);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(89, 12);
			this.Label1.TabIndex = 10;
			this.Label1.Text = "Drop backup file";
			// 
			// file1
			// 
			this.file1.AllowDrop = true;
			this.file1.Location = new System.Drawing.Point(12, 27);
			this.file1.Name = "file1";
			this.file1.Size = new System.Drawing.Size(384, 19);
			this.file1.TabIndex = 9;
			this.file1.DragDrop += new System.Windows.Forms.DragEventHandler(this.file1_DragDrop);
			this.file1.DragEnter += new System.Windows.Forms.DragEventHandler(this.file1_DragEnter);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(489, 212);
			this.Controls.Add(this.Button3);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.msg1);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.maxrev1);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.bkfolder1);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.min1);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.file1);
			this.Name = "Form1";
			this.Text = "auto Revision Backup";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label msg1;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox maxrev1;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.TextBox bkfolder1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox min1;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox file1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
	}
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ini;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace autoRevisionBackupCS
{

	public partial class Form1 : Form
	{

		#region "my data"

		//Currently operating data 現在稼働中のデータ
		private ini.ST_LOG stLog = new ini.ST_LOG();

		#endregion

		#region "delegates"

		//Drop backup file
		public string dlg_getTargetFile()
		{
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<string>(dlg_getTargetFile));
			}
			else
			{
				return file1.Text;
			}
		}

		private void dlg_setTargetFile(string str)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => file1.Text = str));
			}
			else
			{
				file1.Text = str;
			}
		}

		//Drop backup file
		public string dlg_getBkfolder()
		{
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<string>(dlg_getBkfolder));
			}
			else
			{
				return bkfolder1.Text;
			}
		}

		private void dlg_setBkfolder(string str)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => bkfolder1.Text = str));
			}
			else
			{
				bkfolder1.Text = str;
			}
		}

		//Max Revisions
		public int dlg_getMaxRev()
		{
			if (InvokeRequired)
			{
				return (int)Invoke(new Func<int>(dlg_getMaxRev));
			}
			else
			{
				return int.Parse(maxrev1.Text);
			}
		}
		private void dlg_setMaxRev(int i)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => maxrev1.Text = i.ToString()));
			}
			else
			{
				maxrev1.Text = i.ToString();
			}
		}

		//Interval mins.
		public int dlg_getInterval()
		{
			if (InvokeRequired)
			{
				return (int)Invoke(new Func<int>(dlg_getInterval));
			}
			else
			{
				return int.Parse(min1.Text);
			}
		}
		private void dlg_setInterval(int i)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => min1.Text = i.ToString()));
			}
			else
			{
				min1.Text = i.ToString();
			}
		}

		//msg
		public void dlg_setMsg(string str)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => msg1.Text = str));
			}
			else
			{
				msg1.Text = str;
			}
		}

		//set button text
		public void dlg_setButtonText(string str)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => Button1.Text = str));
			}
			else
			{
				Button1.Text = str;
			}
		}



		#endregion

		#region "backgroundWorker Main Thread メインスレッド "

		private long threadBackup(System.ComponentModel.BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
		{
			do
			{
				System.Threading.Thread.Sleep(1000);

				//check go
				if(stLog.go == false)
				{
					continue;
				}

				//Remaining time until the next execution 次回実行までの残り時間
				TimeSpan ts = stLog.nextDate - DateTime.Now;
				if(ts.TotalSeconds > 0)
				{
					dlg_setButtonText(ts.ToString("hh\\:mm\\:ss"));
					continue;
				}

				//Check the existence of backup files バックアップファイルの存在確認
				if(System.IO.File.Exists(stLog.targetFile) == false)
				{
					//If the file does not exist ファイルが存在しない場合停止
					stLog.go = false;
					dlg_setButtonText("Start");
					continue;
				}


				//Get the update time of the backup file. バックアップファイルの更新日時を取得
				DateTime lastUpdate = System.IO.File.GetLastWriteTime(stLog.targetFile);
				if (lastUpdate == stLog.lastUpdate)
				{
					//If it has not been updated, do nothing and set the next execution time. 更新されていない場合は何もしないで次の実行時間を設定
					stLog.nextDate = DateTime.Now.AddMinutes(stLog.intervalMin);
					continue;
				}

				//Perform backup. 更新されている場合はバックアップを実行

				//Generate backup file name. バックアップファイル名を生成
				string backupFileName = System.IO.Path.Combine(stLog.backupPath, 
					System.IO.Path.GetFileNameWithoutExtension(stLog.targetFile) + "_" + 
					DateTime.Now.ToString("yyyyMMdd_HHmmss") + 
					System.IO.Path.GetExtension(stLog.targetFile));

				//copy the target file to the backup folder. ターゲットファイルをバックアップフォルダにコピー
				try
				{
					System.IO.File.Copy(stLog.targetFile, backupFileName);
					//Display message.  バックアップ時刻を表示
					dlg_setMsg(DateTime.Now.ToString("HH:mm:ss"));
					//recording ログに記録
					stLog.lastUpdate = lastUpdate;
					ini.log_addLog(stLog);
				}
				catch (Exception ex)
				{
					dlg_setMsg("Backup failed");
				}

				//バックアップフォルダのファイル数がmaxRevisionを超えているか確認
				System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(stLog.backupPath);
				System.IO.FileInfo[] files = di.GetFiles(System.IO.Path.GetFileNameWithoutExtension(stLog.targetFile) + "_*.*");
				if (files.Length > stLog.maxRevision)
				{
					//If the number of files exceeds maxRevision, delete the oldest file. ファイル数がmaxRevisionを超えた場合は最も古いファイルを削除
					Array.Sort(files, (x, y) => x.CreationTime.CompareTo(y.CreationTime));
					files[0].Delete();
				}

				//Set the next execution time 次の実行時間を設定
				stLog.nextDate = DateTime.Now.AddMinutes(stLog.intervalMin);

			} while (true);

		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			//BackgroundWorkerの取得
			System.ComponentModel.BackgroundWorker objWorker = sender as System.ComponentModel.BackgroundWorker;

			//ここから別世界
			//時間のかかる裏で動かしたい処理
			e.Result = threadBackup(objWorker, e);

		}
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//MessageBox.Show("process completed.");
		}


		#endregion

		#region "onload"

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

			//Loading log files. ログファイルの読み込み
			ini.log_read();

			//実行データ初期化
			stLog.init();

			//バックグラウンド開始
			backgroundWorker1.RunWorkerAsync();

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Stop the background worker when closing the form.
			if (backgroundWorker1.IsBusy)
			{
				backgroundWorker1.CancelAsync();
			}
		}

		#endregion

		#region "Content processing コンテンツ処理"

		private void resetContents(string szfile)
		{

			ini.ST_LOG sttemp = new ini.ST_LOG();

			if(ini.log_getLog(szfile, ref sttemp) >= 0)
			{
				dlg_setBkfolder(sttemp.backupPath);
				dlg_setMaxRev(sttemp.maxRevision);
				dlg_setInterval(sttemp.intervalMin);
			} else
			{
				//If the log does not exist, set the default values. ログが存在しない場合はデフォルト値を設定
				dlg_setBkfolder("");
				dlg_setMaxRev(100);
				dlg_setInterval(10);
			}

			dlg_setMsg("");

		}

		//ボタン押下時の処理
		private void Button1_Click(object sender, EventArgs e)
		{

			//If currently in operation, stop. 現在稼働中なら停止
			if (stLog.go == true)
			{
				stLog.go = false;
				dlg_setButtonText("Start");
				return;
			}

			ini.ST_LOG st = new ini.ST_LOG();
			st.targetFile = dlg_getTargetFile();
			st.backupPath = dlg_getBkfolder();
			st.intervalMin = dlg_getInterval();
			st.maxRevision = dlg_getMaxRev();
			st.lastUpdate = DateTime.Now;
			st.nextDate = DateTime.Now.AddMinutes(st.intervalMin);

			//Confirm existence of target file. targetFile存在確認
			if (System.IO.File.Exists(st.targetFile) == false)
			{
				MessageBox.Show("Target file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//targetFileの更新日時を取得. targetFileの更新日時を取得
			st.lastUpdate = System.IO.File.GetLastWriteTime(st.targetFile);

			//Confirm existence of backupPath. backupPath存在確認
			if (string.IsNullOrEmpty(st.backupPath))
			{
				MessageBox.Show("\"Backup folder is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (System.IO.Directory.Exists(st.backupPath) == false)
			{
				//confirm the creation. 無いので作成確認する
				DialogResult result = MessageBox.Show("The backup folder does not exist. Do you want to create it?", "Create Backup Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					try
					{
						System.IO.Directory.CreateDirectory(st.backupPath);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Failed to create backup folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				else
				{
					return;
				}
			}

			//Max Revisions check.
			if(st.maxRevision < 1)
			{
				MessageBox.Show("Max Revisions must be at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//Interval mins check.
			if(st.intervalMin < 1)
			{
				MessageBox.Show("Interval minutes must be at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//Save the settings to the log
			ini.log_addLog(st);
			stLog.copy(st);

			//start the process
			stLog.go = true;
		}

		//EXPLORER
		private void Button2_Click(object sender, EventArgs e)
		{
			Process.Start("EXPLORER.EXE", dlg_getBkfolder());
		}

		//Select the target file
		private void Button3_Click(object sender, EventArgs e)
		{
			//form2呼び出し
			Form2 dlg = new Form2();
			dlg.ShowDialog();
			int index = dlg.GetSelectedIndex();
			dlg.Dispose();


			if (index >= 0 && index < ini.g_logs)
			{
				dlg_setTargetFile(ini.g_log[index].targetFile);
				resetContents(ini.g_log[index].targetFile);
			}

		}

		#endregion

		#region "d&d process. d&d処理"

		private void file1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
		private void file1_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length > 0)
			{
				//The existence of the directory is checked, and only if it exists is the path displayed in the text box.
				if (System.IO.File.Exists(files[0]))
				{
					dlg_setTargetFile(files[0]);
					resetContents(files[0]);
				}
				else
				{
					resetContents("");
				}
			}
		}

		private void bkfolder1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
		private void bkfolder1_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length > 0)
			{
				//The existence of the directory is checked, and only if it exists is the path displayed in the text box.
				if (System.IO.Directory.Exists(files[0]))
				{
					dlg_setBkfolder(files[0]);
				}
				else
				{
					dlg_setBkfolder("");
				}
			}
		}




		#endregion


	}
}

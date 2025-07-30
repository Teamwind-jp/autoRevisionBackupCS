using System;
using System.Reflection;
using System.Security.Policy;
using static ini;

static class ini
{

	#region "log globals"


	// ST_LOG 構造体からパラメーターなしコンストラクターを削除し、フィールドの初期化はプロパティやメソッドで行ってください。
	public struct ST_LOG
	{
		public string targetFile;
		public string backupPath;
		public int intervalMin;
		public int maxRevision;
		public DateTime lastUpdate;
		public DateTime nextDate;
		public bool go;

		public void init()
		{
			targetFile = "";
			backupPath = "";
			intervalMin = 0;
			maxRevision = 0;
			lastUpdate = DateTime.MinValue;
			nextDate = DateTime.MinValue;
			go = false;
		}

		public void copy(ST_LOG src)
		{
			targetFile = src.targetFile;
			backupPath = src.backupPath;
			intervalMin = src.intervalMin;
			maxRevision = src.maxRevision;
			lastUpdate = src.lastUpdate;
			nextDate = src.nextDate;
			go = src.go;
		}
	}

	public static ST_LOG[] g_log;
	public static int g_logs = 0;

	#endregion


	#region "log io"


	public static Boolean log_read()
	{
		string sztemp;

		//reading file.log
		try
		{
			sztemp = System.IO.File.ReadAllText(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\file.log", System.Text.Encoding.UTF8);
			string[] lines = sztemp.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			//カンマで分割して配列に格納
			g_logs = lines.Length;
			g_log = new ST_LOG[g_logs];
			for (int i = 0; i < g_logs; i++)
			{
				string[] parts = lines[i].Split(',');
				if (parts.Length >= 5)
				{
					g_log[i].init();
					g_log[i].targetFile = parts[0];
					g_log[i].backupPath = parts[1];
					g_log[i].intervalMin = int.Parse(parts[2]);
					g_log[i].maxRevision = int.Parse(parts[3]);
					g_log[i].lastUpdate = DateTime.Parse(parts[4]);
				}
				else
				{
					//Console.WriteLine("Invalid line format: " + lines[i]);
				}
			}

		}
		catch (Exception ex)
		{
			//Console.WriteLine("Error reading file.log: " + ex.Message);
			return false;
		}

		return true;
	}

	//============================================================
	//   Write
	//============================================================
	public static Boolean log_write()
	{
		try
		{
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\file.log",false, System.Text.Encoding.UTF8))
			{
				for (int i = 0; i < g_logs; i++)
				{
					sw.WriteLine($"{g_log[i].targetFile},{g_log[i].backupPath},{g_log[i].intervalMin},{g_log[i].maxRevision},{g_log[i].lastUpdate}");
				}
			}
			return true;
		}
		catch
		{
			return false;
		}
	}

	//指定されたファイルのLOG取得
	public static int log_getLog(string targetFile, ref ST_LOG st)
	{
		for (int i = 0; i < g_logs; i++)
		{
			if (g_log[i].targetFile == targetFile)
			{
				st.copy(g_log[i]);
				return i;
			}
		}
		return -1; // Not found
	}

	//logの追加
	public static void log_addLog(ST_LOG st)
	{
		ST_LOG sttemp = new ST_LOG();

		int idx = log_getLog(st.targetFile, ref sttemp);

		if (idx >= 0)
		{
			//既に存在する場合は更新
			g_log[idx].copy(st);
			return;
		} else
		{
			//新規追加
			Array.Resize(ref g_log, g_logs + 1);
			g_log[g_logs].copy(st);
			g_logs++;
		}

		//ログファイルに書き込み
		log_write();
	}


	#endregion

}
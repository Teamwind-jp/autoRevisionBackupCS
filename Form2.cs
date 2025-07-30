using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autoRevisionBackupCS
{
	public partial class Form2 : Form
	{

		private int selectedIndex = -1;


		public Form2()
		{
			InitializeComponent();
		}

		private void Form2_Load(object sender, EventArgs e)
		{

			selectedIndex = -1;

			//listboxに過去logを表示
			if (ini.g_logs > 0)
			{
				for (int i = 0; i < ini.g_logs; i++)
				{
					var log = ini.g_log[i];
					listBox1.Items.Add($"{log.targetFile}");
				}
				listBox1.SelectedIndex = 0;
			}
			else
			{
				listBox1.Items.Add("No logs available.");
				this.Close();
			}
		}


		//選択したインデックスを返す
		public int GetSelectedIndex()
		{
			return selectedIndex;
		}

		//cancelボタン
		private void Button2_Click(object sender, EventArgs e)
		{
			selectedIndex = -1; // No selection
			this.Close();
		}

		//OKボタン
		private void Button1_Click(object sender, EventArgs e)
		{
			selectedIndex = listBox1.SelectedIndex; // Set the selected index
			this.Close();
		}

	}
}

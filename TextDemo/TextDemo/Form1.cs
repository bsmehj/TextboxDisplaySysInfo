using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public void DispalyInfoMsg(string msg)
        {
            string err = string.Empty;
            string str = string.Empty;
            try
            {
                if (textBoxTest.InvokeRequired)
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        str = string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg) +
                            Environment.NewLine;
                        textBoxTest.Text = textBoxTest.Text.Insert(0, str);
                        textBoxTest.ScrollToCaret();
                    }));
                }
                else
                {
                    str = string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg) +
                        Environment.NewLine;
                    textBoxTest.Text = textBoxTest.Text.Insert(0, str);
                    textBoxTest.ScrollToCaret();
                }
            }
            catch (System.Exception ex)
            {
                err = "清空数据出现错误：" + ex.Message;
                MessageBox.Show(msg);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timerDemo.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerDemo.Enabled = false;
        }

        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public void FileAdd(string filepath, string strings)
        {
            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate|FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs); // 创建写bai入duzhi流
            //sw.WriteLine(strings); // 写入Hello World
            //sw.Close(); //关闭文件

            //StreamWriter sw = File.AppendText(strings);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }

        private void timerDemo_Tick(object sender, EventArgs e)
        {
            DispalyInfoMsg("ABC");

            string target = Application.StartupPath + @"\SystemInfo\";
            // 检查目标文件夹是否存在，不存在则创建
            if (Directory.Exists(target) == false)
            {
                Directory.CreateDirectory(target);
            }
            string filePath = target + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            // 数据回滚实现
            // int lineNum = textBoxTest.Lines.Count();
            int num = textBoxTest.Lines.Count();
            if (num >= 11) 
            {
                int start = textBoxTest.GetFirstCharIndexFromLine(num - 2);
                int end = textBoxTest.GetFirstCharIndexFromLine(num - 1);
                textBoxTest.Select(start, end);
                FileAdd(filePath, textBoxTest.SelectedText);
                textBoxTest.SelectedText = "";

                // timerDemo.Enabled = false;
                // FileAdd(string filepath, string strings)
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxTest.Clear();
        }
    }
}

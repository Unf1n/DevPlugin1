using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlugin
{
    public partial class Form1 : Form
    {
        string path = "";
        string path1 = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            string[] args = Environment.GetCommandLineArgs();
            int a=0;
            foreach (string arg in args)
            {
                if (a == 0)
                {
                    path1= arg;
                    int len = path1.IndexOf("\\DevPlugin.exe");
                    if (len > 0)
                    {
                        path1 = path1.Substring(0,len);
                    }
                }
                    a++;
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = new string[500];
            if (path == "")
            {
                MessageBox.Show("Выберите путь для файла");
            }
            else
            {
                string filename = path1 + "\\data\\data.csproj";
                MessageBox.Show(path1);
                MessageBox.Show(filename);

                using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
                {
                    var list = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        list.Add(line);
                        Console.WriteLine(list[list.Count - 1]);
                    }
            sr.Close();
                    lines = list.ToArray();
                }



                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i].IndexOf("<RootNamespace>(\\W+?)</RootNamespace>") > 0)
                    {
                        lines[i] = $"    <RootNamespace>TCG.{textBox1.Text}</RootNamespace>";
                    }
                }

                Directory.CreateDirectory(path + '\\' + textBox1.Text);
                Directory.CreateDirectory(path + '\\' + textBox1.Text + "\\TCG.Fractures");

                File.AppendAllLines(path + '\\' + textBox1.Text + "\\TCG.Fractures\\TCG." + textBox1.Text,lines);
                File.Create(path + '\\' + textBox1.Text + "\\TCG.Fractures\\TCG.Fractures.csproj.backup_13.08.2015 22-17-53");


            }
        }
        
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
                textBox3.Text = path;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

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

        public bool change (string seek, string text)
        {
                if (Regex.IsMatch(text, seek))
                {
                   return true;
                }
            return false;
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
                string filename = path1 + "\\simple-plugin\\TCG.SimplePlugin\\TCG.SimplePlugin.csproj";
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

               var guID1 = Guid.NewGuid();
                string strid = guID1.ToString();
                for (int i = 1; i < lines.Length; i++)
                {
                    if (change(" <RootNamespace>(\\w+?.\\w+?)</RootNamespace>", lines[i]))
                        lines[i] = $"<RootNamespace>{textBox1.Text}</RootNamespace>";
                    if (change(" <AssemblyName>(\\w+?.\\w+?)</AssemblyName>", lines[i]))
                        lines[i] = $" <AssemblyName>{textBox1.Text}</AssemblyName>";
                    if (change("<ProductVersion>(\\w+?.\\w+?.\\w+?)</ProductVersion>", lines[i]))
                        lines[i] = $"<ProductVersion>{textBox4.Text}</ProductVersion>";
                    if (change("<ProductVersion>(\\w+?.\\w+?.\\w+?)</ProductVersion>", lines[i]))
                        lines[i] = $"<ProductVersion>{textBox4.Text}</ProductVersion>";
                    if (lines[i].IndexOf("<ProjectGuid>{034805E2-9B80-4F18-930B-0E789C4B9A8C}</ProjectGuid>") > 0)
                        lines[i] = $"<ProjectGuid>{{{strid}}}</ProjectGuid>";

                    if (lines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Data.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Data.dll</HintPath>";

                    if (lines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>>";

                    if (lines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.Modeling.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Slb.Ocean.Petrel.Modeling.dll</HintPath>";

                    if (lines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>";

                    if (lines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>";

                    if (lines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2020\\Public\\Slb.Ocean.Units.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Units.dll</HintPath>";


                    if (lines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\slb.ocean.core.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\slb.ocean.core.dll</HintPath>";

                    if (lines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\slb.ocean.petrel.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\slb.ocean.petrel</HintPath>";

                    if (lines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\Slb.Ocean.Basics.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\Slb.Ocean.Basics.dll</HintPath>";

                    if (lines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\Slb.Ocean.Geometry.dll</HintPath>") > 0)
                        lines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\Slb.Ocean.Geometry.dll</HintPath>";

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

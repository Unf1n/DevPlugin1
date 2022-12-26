using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public string[] WriteF(string file)
        {
            string[] f = new string[500];
            using (StreamReader sr = new StreamReader(file, System.Text.Encoding.Default))
            {
                var list = new List<string>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    list.Add(line);
                    Console.WriteLine(list[list.Count - 1]);
                }
                sr.Close();
                f = list.ToArray();
            }
            return f;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] TCGClines = new string[500];
            string[] linesSLN = new string[500];
            string[] linesAPINI = new string[500];
            string[] linesTEMP = new string[500];



            if (path == "")
            {
                MessageBox.Show("Выберите путь для файла");
            }
            else
            {
                string PName = "";

                string filenameTCG = path1 + "\\simple-plugin\\TCG.SimplePlugin\\TCG.SimplePlugin.csproj";
                string filenameSLN = path1 + "\\simple-plugin\\TCG.SimplePlugin.sln";
                string filenameAPINI = path1 + "\\simple-plugin\\Appinfo.ini";
                string filenameTEMP = path1 + "\\simple-plugin\\TCG.SimplePlugin\\obj\\Debug\\TempPE\\Properties.Resources.Designer.cs.dll";


                TCGClines = WriteF(filenameTCG);
                linesSLN = WriteF(filenameSLN);
                linesAPINI = WriteF(filenameAPINI);
                linesTEMP = WriteF(filenameTEMP);


                var guID1 = Guid.NewGuid();
                string strid1 = guID1.ToString();

                for(int i = 0; i < textBox1.Text.Length; i ++)
                {
                    PName += textBox1.Text[i];
                    PName += " ";
                }
                for (int i = 0; i < linesSLN.Length; i++)
                {
                    
                       linesSLN[4] = $"Project(\"{{{Guid.NewGuid().ToString()}}}\") = \"TCG.{textBox1.Text}\", \"TCG.{textBox1.Text}\\TCG.{textBox1.Text}.csproj\", \"{{{strid1}}}\"";

                    if (linesSLN[i].IndexOf("GlobalSection(ProjectConfigurationPlatforms) = postSolution") > 0)
                    {
                        MessageBox.Show("123");
                        linesSLN[i + 1] = $"\t\t{{{strid1}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU";
                        linesSLN[i + 2] = $"\t\t{{{strid1}}}.Debug|Any CPU.Build.0 = Debug|Any CPU";
                        linesSLN[i + 3] = $"\t\t{{{strid1}}}.Release|Any CPU.ActiveCfg = Release|Any CPU";
                        linesSLN[i + 4] = $"\t\t{{{strid1}}}.Release|Any CPU.Build.0 = Release|Any CPU";
                    }
                    if (linesSLN[i].IndexOf("SolutionGuid ") > 0)
                        linesSLN[i] = $"\t\tSolutionGuid = {{{Guid.NewGuid().ToString()}}}";

                }
                for (int i = 1; i < TCGClines.Length; i++)
                {
                    if (change(" <RootNamespace>(\\w+?.\\w+?)</RootNamespace>", TCGClines[i]))
                        TCGClines[i] = $"    <RootNamespace>TCG.{textBox1.Text}</RootNamespace>";
                    if (change(" <AssemblyName>(\\w+?.\\w+?)</AssemblyName>", TCGClines[i]))
                        TCGClines[i] = $"    <AssemblyName>TCG.{textBox1.Text}</AssemblyName>";
                    if (change("<ProductVersion>(\\w+?.\\w+?.\\w+?)</ProductVersion>", TCGClines[i]))
                        TCGClines[i] = $"    <ProductVersion>{textBox4.Text}</ProductVersion>";
                    if (TCGClines[i].IndexOf("<ProjectGuid>{034805E2-9B80-4F18-930B-0E789C4B9A8C}</ProjectGuid>") > 0)
                        TCGClines[i] = $"    <ProjectGuid>{{{strid1}}}</ProjectGuid>";

                    if (TCGClines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Data.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Data.dll</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>>";

                    if (TCGClines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.Modeling.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Slb.Ocean.Petrel.Modeling.dll</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2013\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>C:\\Program Files\\Schlumberger\\Petrel 2020\\Public\\Slb.Ocean.Units.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Units.dll</HintPath>";


                    if (TCGClines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\slb.ocean.core.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\slb.ocean.core.dll</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\slb.ocean.petrel.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\slb.ocean.petrel</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\Slb.Ocean.Basics.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\Slb.Ocean.Basics.dll</HintPath>";

                    if (TCGClines[i].IndexOf("<HintPath>c:\\program files\\schlumberger\\petrel 2013\\Public\\Slb.Ocean.Geometry.dll</HintPath>") > 0)
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\Slb.Ocean.Geometry.dll</HintPath>";

                    TCGClines[176] = $"\"%25OCEAN{comboBox1.Text}HOME%25\\PluginPackager.exe\" / g \"$(TargetPath)\" \"$(ProjectDir)\\plugin.xml\" \"%25OCEAN{comboBox1.Text}HOME_x64%25\\petrel.exe\"";
                    TCGClines[177] = $"\"%25OCEAN{comboBox1.Text}HOME%25\\PluginPackager.exe\" /m \"$(ProjectDir)\\plugin.xml\" \"%25OCEAN{comboBox1.Text}HOME_x64%25\\petrel.exe\" \"$(TargetDir)\"</PostBuildEvent>";

                }
                for (int i = 1; i < linesAPINI.Length; i++)
                {

                    linesAPINI[1] = $"AppName={textBox1.Text}";
                    linesAPINI[3] = $"Version={textBox4.Text}";
                    linesAPINI[4] = $"PetrelVersion={comboBox1.Text}.{textBox2.Text}(64-bit)";
                }

                Directory.CreateDirectory($"{path}\\{textBox1.Text}");
                Directory.CreateDirectory($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}");
                Directory.CreateDirectory($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Properties");
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Properties\Resources.resx", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Properties\\Resources.resx",true);


                File.AppendAllLines($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\obj\\Debug\\TempPE\\Properties.Resources.Designer.cs.dll", linesTEMP);
                File.AppendAllLines($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\TCG.{textBox1.Text}.csproj",TCGClines);
                File.AppendAllLines($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}.sln", linesSLN);
                File.AppendAllLines($"{path}\\{textBox1.Text}\\Appinfo.ini", linesAPINI);
                File.Create($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\TCG.Fractures.csproj.backup_13.08.2015 22-17-53");


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

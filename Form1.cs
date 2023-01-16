using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            int a = 0;
            foreach (string arg in args)
            {
                if (a == 0)
                {
                    path1 = arg;
                    int len = path1.IndexOf("\\DevPlugin.exe");
                    if (len > 0)
                    {
                        path1 = path1.Substring(0, len);
                    }
                }
                a++;
            }
            try
            {
                Directory.GetFiles(path1 + "\\simple-plugin");
            }
            catch
            {
                MessageBox.Show("DirectoryNotFoundException", "Erorr!");
                this.Close();
            }

        }
        // string[] TCGClines1 = new string[2];

        public bool change(string seek, string text)
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
        string[] TCGClines = new string[500];
        string[] linesModu = new string[500];
        string[] linesSLN = new string[500];
        string[] linesSTD = new string[500];
        string[] linesAPINI = new string[500];
        string[] linesAssemblyInfo = new string[500];
        string[] linesResourcesDesigner = new string[500];
        string[] linesPlugincs = new string[500];
        string[] linesPlugin = new string[500];
        string[] linesCSpro = new string[500];
        string[] linesWorkstep1 = new string[500];
        string[] linesWorkstep1UI = new string[500];
        string[] linesWorkstep1UIDesigner = new string[500];
        public void Create(string a, string b)
        {
            #region var
            string filenameTCG = $"{a}\\TCG.{b}\\TCG.{b}.csproj";
            string filenameModu = $"{a}\\TCG.{b}\\Module.cs";
            string filenameSLN = $"{a}\\TCG.{b}.sln";
            string filenameAPINI = $"{a}\\Appinfo.ini";
            string filenameAssemblyInfo = $"{a}\\TCG.{b}\\Properties\\AssemblyInfo.cs";
            string filenameResourcesDesigner = $"{a}\\TCG.{b}\\Properties\\Resources.Designer.cs";
            string filenameSTD = $"{a}\\TCG.{b}\\Properties\\Settings.Designer.cs";
            string filenamePlugincs = $"{a}\\TCG.{b}\\Plugin.cs";
            string filenamePlugin = $"{a}\\TCG.{b}\\plugin.xml";
            string filenameCSpro = $"{a}\\TCG.{b}\\TCG.{b}.csproj.user";
            string filenameWorkstep1 = $"{a}\\TCG.{b}\\Workstep1.cs";
            string filenameWorkstep1UI = $"{a}\\TCG.{b}\\Workstep1UI.cs";
            string filename1UIDesigner = $"{a}\\TCG.{b}\\Workstep1UI.Designer.cs";

            TCGClines = WriteF(filenameTCG);
            linesModu = WriteF(filenameModu);
            linesSLN = WriteF(filenameSLN);
            linesAPINI = WriteF(filenameAPINI);
            linesAssemblyInfo = WriteF(filenameAssemblyInfo);
            linesResourcesDesigner = WriteF(filenameResourcesDesigner);
            linesSTD = WriteF(filenameSTD);
            linesPlugincs = WriteF(filenamePlugincs);
            linesPlugin = WriteF(filenamePlugin);
            linesCSpro = WriteF(filenameCSpro);
            linesWorkstep1 = WriteF(filenameWorkstep1);
            linesWorkstep1UI = WriteF(filenameWorkstep1UI);
            linesWorkstep1UIDesigner = WriteF(filename1UIDesigner);
            #endregion
            var guID1 = Guid.NewGuid();
            string strid1 = guID1.ToString();
            DateTime today = new DateTime();
            for (int i = 1; i < linesAssemblyInfo.Length; i++)
            {
                if (change(@"\[assembly: AssemblyTitle\(""TCG.(\w+?)""\)\]", linesAssemblyInfo[i]))
                    linesAssemblyInfo[i] = $"[assembly: AssemblyTitle(\"TCG.{textBox1.Text}\")]";
                if (change(@"\[assembly: AssemblyProduct\(""TCG.(\w+?)""\)\]", linesAssemblyInfo[i]))
                    linesAssemblyInfo[11] = $"[assembly: AssemblyProduct(\"TCG.{textBox1.Text}\")]";
                if (change(@"\[assembly: Guid\(""(\w+?-\w+?-\w+?-\w+?-\w+?)""\)\]", linesAssemblyInfo[i]))
                    linesAssemblyInfo[22] = $"[assembly: Guid(\"{Guid.NewGuid().ToString()}\")]";
            }
            for (int i = 1; i < linesResourcesDesigner.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesResourcesDesigner[i]))
                    linesResourcesDesigner[i] = $"namespace TCG.{textBox1.Text}.Properties {{";
                if (change(@"global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager\(""TCG.(\w+?).Properties.Resources"", typeof\(Resources\).Assembly\);", linesResourcesDesigner[i]))
                    linesResourcesDesigner[i] = $"                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(\"TCG.{textBox1.Text}.Properties.Resources\", typeof(Resources).Assembly);";
                if (change(@"///AppName=(\w+)", linesResourcesDesigner[i]))
                    linesResourcesDesigner[i] = $"        ///AppName={textBox1.Text}";
                if (change(@"///Version=(\w+.\w+.\w+)", linesResourcesDesigner[i]))
                    linesResourcesDesigner[i] = $"        ///Version={textBox4.Text}";
                if (change(@"///PetrelVersion=(\w+?.\w+?)\(64-bit\)", linesResourcesDesigner[i]))
                    linesResourcesDesigner[i] = $"        ///PetrelVersion={comboBox1.Text}.";
            }
            for (int i = 1; i < linesSTD.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesSTD[i]))
                    linesSTD[i] = $"namespace TCG.{textBox1.Text}.Properties {{";
            }
            for (int i = 1; i < linesPlugincs.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesPlugincs[i]))
                    linesPlugincs[i] = $"namespace TCG.{textBox1.Text}";
                if (change(@"get { return ""(\w+.\w+.\w+)""; }", linesPlugincs[i]))
                    linesPlugincs[i] = $"            get {{ return \"{textBox4.Text}\"; }}";
                if (change(@"get { return ""TCG.(\w+)""; }", linesPlugincs[i]))
                    linesPlugincs[i] = $"            get {{ return \"TCG.{textBox1.Text}\"; }}";
            }
            for (int i = 1; i < linesPlugin.Length; i++)
            {
                if (change(@"<Name>TCG.(\w+)</Name>", linesPlugin[i]))
                    linesPlugin[i] = $"    <Name>TCG.{textBox1.Text}</Name>";
                if (change(@"<PluginTypeName>TCG.(\w+).Plugin, TCG.(\w+), Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a</PluginTypeName>", linesPlugin[i]))
                    linesPlugin[i] = $"    <PluginTypeName>TCG.{textBox1.Text}.Plugin, TCG.{textBox1.Text}, Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a</PluginTypeName>";
                if (change(@"<PluginId>://TCG.(\w+).Plugin/1.0.3.0</PluginId>", linesPlugin[i]))
                    linesPlugin[i] = $"    <PluginId>://TCG.{textBox1.Text}.Plugin/1.0.3.0</PluginId>";
                if (change(@"<ReleaseDate>(\w+-\w+-\w+)</ReleaseDate>", linesPlugin[i]))
                    linesPlugin[i] = $"    <ReleaseDate>{today.ToString("yyyy/MM/dd")}</ReleaseDate>";
                if (change(@"<AppVersion>(\w+)</AppVersion>", linesPlugin[i]))
                    linesPlugin[i] = $"    <AppVersion>{comboBox1.Text}</AppVersion>";
                if (change(@"<Module TypeName=""TCG.(\w+).Module, TCG.(\w+), Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a"" />", linesPlugin[i]))
                    linesPlugin[i] = $"      <Module TypeName=\"TCG.{textBox1.Text}.Module, TCG.{textBox1.Text}, Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a\" />";
                if (change(@"<Assembly Name=""TCG.(\w+), Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a"" />", linesPlugin[i]))
                    linesPlugin[i] = $" < Assembly Name=\"TCG.{textBox1.Text}, Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a\" />";
            }
            for (int i = 1; i < linesCSpro.Length; i++)
            {
                if (change(@"<StartProgram>c:\\program files\\schlumberger\\Petrel (\w+)\\petrel.exe</StartProgram>", linesCSpro[i]))
                    linesCSpro[i] = $"    <StartProgram>c:\\program files\\schlumberger\\Petrel {comboBox1.Text}\\petrel.exe</StartProgram>";
                if (change(@"<StartWorkingDirectory>c:\\program files\\schlumberger\\Petrel (\w+)\\</StartWorkingDirectory>", linesCSpro[i]))
                    linesCSpro[i] = $"    <StartWorkingDirectory>c:\\program files\\schlumberger\\Petrel {comboBox1.Text}\\</StartWorkingDirectory>";
                if (change(@"<ReferencePath>C:\\Program Files\\Schlumberger\\Petrel (\w+)\\Public\\;D:\\svn\\svn_kz\\Tools\\bin\\</ReferencePath>", linesPlugin[i]))
                    linesCSpro[i] = $" < ReferencePath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\;D:\\svn\\svn_kz\\Tools\\bin\\</ReferencePath>";
            }
            for (int i = 1; i < linesWorkstep1.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesWorkstep1[i]))
                    linesWorkstep1[i] = $"namespace TCG.{textBox1.Text}";
                if (change(@"return ""(\w+-\w+-\w+-\w+-\w+)"";", linesWorkstep1[i]))
                    linesWorkstep1[i] = $"				return \"{Guid.NewGuid().ToString()}\";";
            }
            for (int i = 1; i < linesWorkstep1UI.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesWorkstep1UI[i]))
                    linesWorkstep1UI[i] = $"namespace TCG.{textBox1.Text}";
            }
            for (int i = 1; i < linesWorkstep1UIDesigner.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesWorkstep1UIDesigner[i]))
                    linesWorkstep1UIDesigner[0] = $"namespace TCG.{textBox1.Text}";
            }
            for (int i = 0; i < linesSLN.Length; i++)
            {
                if (change(@"Project\(""{(\w+?-\w+?-\w+?-\w+?-\w+?)}""\) = ""TCG.(\w+?)"", ""TCG.(\w+?)\\TCG.(\w+?).csproj"", ""{(\w+?-\w+?-\w+?-\w+?-\w+?)}""", linesSLN[i]))
                    linesSLN[i] = $"Project(\"{{{Guid.NewGuid().ToString()}}}\") = \"TCG.{textBox1.Text}\", \"TCG.{textBox1.Text}\\TCG.{textBox1.Text}.csproj\", \"{{{strid1}}}\"";



                if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Debug\|Any CPU.ActiveCfg = Debug\|Any CPU", linesSLN[i]))
                    linesSLN[i] = $"\t\t{{{strid1}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU";
                if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Debug\|Any CPU.Build.0 = Debug\|Any CPU", linesSLN[i]))
                    linesSLN[i] = $"\t\t{{{strid1}}}.Debug|Any CPU.Build.0 = Debug|Any CPU";

                if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Release\|Any CPU.ActiveCfg = Release\|Any CPU", linesSLN[i]))
                    linesSLN[i] = $"\t\t{{{strid1}}}.Release|Any CPU.ActiveCfg = Release|Any CPU";

                if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Release\|Any CPU.Build.0 = Release\|Any CPU", linesSLN[i]))
                    linesSLN[i] = $"\t\t{{{strid1}}}.Release|Any CPU.Build.0 = Release|Any CPU";


                if (change(@"SolutionGuid = {(\w+?-\w+?-\w+?-\w+?-\w+?)}", linesSLN[i]))
                    linesSLN[i] = $"\t\tSolutionGuid = {{{Guid.NewGuid().ToString()}}}";

            }
            for (int i = 0; i < linesModu.Length; i++)
            {
                if (change(@"namespace TCG.(\w+)", linesModu[i]))
                    linesModu[i] = $"namespace TCG.{textBox1.Text}";
            }
            string vp = "";
            for (int i = comboBox1.Text.Length - 1; i > 0; i--)
            {
                if (comboBox1.Text[i] == '.')
                {
                    vp = comboBox1.Text.Substring(0, i);
                    break;
                }
            }
            for (int i = 1; i < TCGClines.Length; i++)
            {
                if (change(@"<RootNamespace>(\w+?.\w+?)</RootNamespace>", TCGClines[i]))
                    TCGClines[i] = $"    <RootNamespace>TCG.{textBox1.Text}</RootNamespace>";
                if (change(@"<AssemblyName>(\w+?.\w+?)</AssemblyName>", TCGClines[i]))
                    TCGClines[i] = $"    <AssemblyName>TCG.{textBox1.Text}</AssemblyName>";
                if (change(@"<ProductVersion>(\w+?.\w+?.\w+?)</ProductVersion>", TCGClines[i]))
                    TCGClines[i] = $"    <ProductVersion>{textBox4.Text}</ProductVersion>";
                if (change(@"<ProjectGuid>{(\w+?-\w+?-\w+?-\w+?-\w+?)}</ProjectGuid>", TCGClines[i]))
                    TCGClines[i] = $"    <ProjectGuid>{{{strid1}}}</ProjectGuid>";
                if (change(@"<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\w+)\\Public\\Slb.Ocean.Data.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {vp}\\Public\\Slb.Ocean.Data.dll</HintPath>";
                if (change(@"<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\w+?)\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {vp}\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>";
                if (change(@"<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\w+?)\\Public\\Slb.Ocean.Petrel.Modeling.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {vp}\\Public\\Slb.Ocean.Petrel.Modeling.dll</HintPath>";
                if (change(@"<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\w+?)\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {vp}\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>";
                if (change(@"<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\w+?)\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {vp}\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>";
                if (change(@"<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\w+?)\\Public\\Slb.Ocean.Units.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {vp}\\Public\\Slb.Ocean.Units.dll</HintPath>";
                if (change(@"<HintPath>c:\\program files\\schlumberger\\petrel (\w+?)\\Public\\slb.ocean.core.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {vp}\\Public\\slb.ocean.core.dll</HintPath>";
                if (change(@"<HintPath>c:\\program files\\schlumberger\\petrel (\w+?)\\Public\\slb.ocean.petrel.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {vp}\\Public\\slb.ocean.petrel</HintPath>";
                if (change(@"<HintPath>c:\\program files\\schlumberger\\petrel (\w+?)\\Public\\Slb.Ocean.Basics.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {vp}\\Public\\Slb.Ocean.Basics.dll</HintPath>";
                if (change(@"<HintPath>c:\\program files\\schlumberger\\petrel (\w+?)\\Public\\Slb.Ocean.Geometry.dll</HintPath>", TCGClines[i]))
                    TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {vp}\\Public\\Slb.Ocean.Geometry.dll</HintPath>";
                if (change(@"""%25OCEAN(\w+?)HOME%25\\PluginPackager\.exe"" /g ""\$\(TargetPath\)"" ""\$\(ProjectDir\)\\plugin\.xml"" ""%25OCEAN(\w+?)HOME_x64%25\\petrel\.exe""", TCGClines[i]))
                    TCGClines[i] = $"\"%25OCEAN{vp}HOME%25\\PluginPackager.exe\" / g \"$(TargetPath)\" \"$(ProjectDir)\\plugin.xml\" \"%25OCEAN{vp}HOME_x64%25\\petrel.exe\"";
                if (change(@"""%25OCEAN(\w+?)HOME%25\\PluginPackager.exe"" /m ""\$\(ProjectDir\)\\plugin.xml"" ""%25OCEAN(\w+?)HOME_x64%25\\petrel.exe"" ""\$\(TargetDir\)""</PostBuildEvent>", TCGClines[i]))
                    TCGClines[i] = $"\"%25OCEAN{vp}HOME%25\\PluginPackager.exe\" /m \"$(ProjectDir)\\plugin.xml\" \"%25OCEAN{vp}HOME_x64%25\\petrel.exe\" \"$(TargetDir)\"</PostBuildEvent>";
            }
            for (int i = 1; i < linesAPINI.Length; i++)
            {
                if (change(@"AppName=(\w+)", linesAPINI[i]))
                    linesAPINI[i] = $"AppName={textBox1.Text}";
                linesAPINI[3] = $"Version={textBox4.Text}";
                if (change(@"Version=(\w+?).(\w+?).(\w+)", linesAPINI[i]))
                    linesAPINI[3] = $"Version={textBox4.Text}";
                if (change(@"PetrelVersion=(\w+?).(\w+?)\(64-bit\)", linesAPINI[i]))
                    linesAPINI[4] = $"PetrelVersion={comboBox1.Text}";
            }




        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                path = FBD.SelectedPath;
            }

            if (path == "")
            {
                MessageBox.Show("Выберите путь для файла");
            }
            else
            {

                string filenameTCG = path1 + "\\simple-plugin\\TCG.SimplePlugin\\TCG.SimplePlugin.csproj";
                string filenameSLN = path1 + "\\simple-plugin\\TCG.SimplePlugin.sln";
                string filenameAPINI = path1 + "\\simple-plugin\\Appinfo.ini";
                string filenameAssemblyInfo = path1 + "\\simple-plugin\\TCG.SimplePlugin\\Properties\\AssemblyInfo.cs";
                string filenameResourcesDesigner = path1 + "\\simple-plugin\\TCG.SimplePlugin\\Properties\\Resources.Designer.cs";

                string filenamePlugincs = path1 + "\\simple-plugin\\TCG.SimplePlugin\\Plugin.cs";
                string filenamePlugin = path1 + "\\simple-plugin\\TCG.SimplePlugin\\plugin.xml";
                string filenameCSpro = path1 + "\\simple-plugin\\TCG.SimplePlugin\\TCG.SimplePlugin.csproj.user";
                string filenameWorkstep1 = path1 + "\\simple-plugin\\TCG.SimplePlugin\\Workstep1.cs";
                string filenameWorkstep1UI = path1 + "\\simple-plugin\\TCG.SimplePlugin\\Workstep1UI.cs";
                string filename1UIDesigner = path1 + "\\simple-plugin\\TCG.SimplePlugin\\Workstep1UI.Designer.cs";


                TCGClines = WriteF(filenameTCG);
                linesSLN = WriteF(filenameSLN);
                linesAPINI = WriteF(filenameAPINI);
                linesAssemblyInfo = WriteF(filenameAssemblyInfo);
                linesResourcesDesigner = WriteF(filenameResourcesDesigner);
                linesPlugincs = WriteF(filenamePlugincs);
                linesPlugin = WriteF(filenamePlugin);
                linesCSpro = WriteF(filenameCSpro);
                linesWorkstep1 = WriteF(filenameWorkstep1);
                linesWorkstep1UI = WriteF(filenameWorkstep1UI);
                linesWorkstep1UIDesigner = WriteF(filename1UIDesigner);



                var guID1 = Guid.NewGuid();
                string strid1 = guID1.ToString();
                DateTime today = new DateTime();
                for (int i = 1; i < linesAssemblyInfo.Length; i++)
                {
                    if (change(@"\[assembly: AssemblyTitle\(""TCG.(\w+?)""\)\]", linesAssemblyInfo[i]))
                        linesAssemblyInfo[i] = $"[assembly: AssemblyTitle(\"TCG.{textBox1.Text}\")]";
                    if (change(@"\[assembly: AssemblyProduct\(""TCG.(\w+?)""\)\]", linesAssemblyInfo[i]))
                        linesAssemblyInfo[i] = $"[assembly: AssemblyProduct(\"TCG.{textBox1.Text}\")]";
                    if (change(@"\[assembly: Guid\(""(\w+?-\w+?-\w+?-\w+?-\w+?)""\)\]", linesAssemblyInfo[i]))
                        linesAssemblyInfo[i] = $"[assembly: Guid(\"{Guid.NewGuid().ToString()}\")]";
                }
                for (int i = 1; i < linesResourcesDesigner.Length; i++)
                {
                    if (change(@"global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager\(""TCG.(\w+?).Properties.Resources"", typeof\(Resources\).Assembly\);", linesResourcesDesigner[i]))
                        linesResourcesDesigner[i] = $"                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(\"TCG.{textBox1.Text}.Properties.Resources\", typeof(Resources).Assembly);";
                    if (change(@"///AppName=(\w+)", linesResourcesDesigner[i]))
                        linesResourcesDesigner[i] = $"        ///AppName={textBox1.Text}";
                    if (change(@"///Version=(\w+.\w+.\w+)", linesResourcesDesigner[i]))
                        linesResourcesDesigner[i] = $"        ///Version={textBox4.Text}";
                    if (change(@"///PetrelVersion=(\w+?.\w+?)\(64-bit\)", linesResourcesDesigner[i]))
                        linesResourcesDesigner[i] = $"        ///PetrelVersion={comboBox1.Text}.";
                }
                for (int i = 1; i < linesPlugincs.Length; i++)
                {
                    if (change(@"namespace TCG.(\w+)", linesPlugincs[i]))
                        linesPlugincs[i] = $"namespace TCG.{textBox1.Text}";
                    if (change(@"get { return ""(\w+.\w+.\w+)""; }", linesPlugincs[i]))
                        linesPlugincs[i] = $"            get {{ return \"{textBox4.Text}\"; }}";
                    if (change(@"get { return ""TCG.(\w+)""; }", linesPlugincs[i]))
                        linesPlugincs[i] = $"            get {{ return \"TCG.{textBox1.Text}\"; }}";
                }
                for (int i = 1; i < linesPlugin.Length; i++)
                {
                    if (change(@"<Name>TCG.(\w+)</Name>", linesPlugin[i]))
                        linesPlugin[i] = $"    <Name>TCG.{textBox1.Text}</Name>";
                    if (change(@"<PluginTypeName>TCG.(\w+).Plugin, TCG.(\w+), Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a</PluginTypeName>", linesPlugin[i]))
                        linesPlugin[i] = $"    <PluginTypeName>TCG.{textBox1.Text}.Plugin, TCG.{textBox1.Text}, Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a</PluginTypeName>";
                    if (change(@"<PluginId>://TCG.(\w+).Plugin/1.0.3.0</PluginId>", linesPlugin[i]))
                        linesPlugin[i] = $"    <PluginId>://TCG.{textBox1.Text}.Plugin/1.0.3.0</PluginId>";
                    if (change(@"<ReleaseDate>(\w+-\w+-\w+)</ReleaseDate>", linesPlugin[i]))
                        linesPlugin[i] = $"    <ReleaseDate>{today.ToString("yyyy/MM/dd")}</ReleaseDate>";
                    if (change(@"<AppVersion>(\w+)</AppVersion>", linesPlugin[i]))
                        linesPlugin[i] = $"    <AppVersion>{comboBox1.Text}</AppVersion>";
                    if (change(@"<Module TypeName=""TCG.(\w+).Module, TCG.(\w+), Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a"" />", linesPlugin[i]))
                        linesPlugin[i] = $"      <Module TypeName=\"TCG.{textBox1.Text}.Module, TCG.{textBox1.Text}, Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a\" />";
                    if (change(@"<Assembly Name=""TCG.(\w+), Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a"" />", linesPlugin[i]))
                        linesPlugin[i] = $" < Assembly Name=\"TCG.{textBox1.Text}, Version=1.0.3.0, Culture=neutral, PublicKeyToken=9de9635fa700ae8a\" />";
                }
                for (int i = 1; i < linesCSpro.Length; i++)
                {
                    if (change(@"<StartProgram>c:\\program files\\schlumberger\\Petrel (\w+)\\petrel.exe</StartProgram>", linesCSpro[i]))
                        linesCSpro[i] = $"    <StartProgram>c:\\program files\\schlumberger\\Petrel {comboBox1.Text}\\petrel.exe</StartProgram>";
                    if (change(@"<StartWorkingDirectory>c:\\program files\\schlumberger\\Petrel (\w+)\\</StartWorkingDirectory>", linesCSpro[i]))
                        linesCSpro[i] = $"    <StartWorkingDirectory>c:\\program files\\schlumberger\\Petrel {comboBox1.Text}\\</StartWorkingDirectory>";
                    if (change(@"<ReferencePath>C:\\Program Files\\Schlumberger\\Petrel (\w+)\\Public\\;D:\\svn\\svn_kz\\Tools\\bin\\</ReferencePath>", linesPlugin[i]))
                        linesCSpro[i] = $" < ReferencePath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\;D:\\svn\\svn_kz\\Tools\\bin\\</ReferencePath>";
                }
                for (int i = 1; i < linesWorkstep1.Length; i++)
                {
                    if (change(@"namespace TCG.(\w+)", linesWorkstep1[i]))
                        linesWorkstep1[i] = $"namespace TCG.{textBox1.Text}";
                    if (change(@"return ""(\w+-\w+-\w+-\w+-\w+)"";", linesWorkstep1[i]))
                        linesWorkstep1[i] = $"				return \"{Guid.NewGuid().ToString()}\";";
                }
                for (int i = 1; i < linesWorkstep1UI.Length; i++)
                {
                    if (change(@"namespace TCG.(\w+)", linesWorkstep1UI[i]))
                        linesWorkstep1UI[i] = $"namespace TCG.{textBox1.Text}";
                }
                for (int i = 1; i < linesWorkstep1UIDesigner.Length; i++)
                {
                    if (change(@"namespace TCG.(\w+)", linesWorkstep1UIDesigner[i]))
                        linesWorkstep1UIDesigner[i] = $"namespace TCG.{textBox1.Text}";
                }
                for (int i = 0; i < linesSLN.Length; i++)
                {
                    if (change(@"Project\(""{(\w+?-\w+?-\w+?-\w+?-\w+?)}""\) = ""TCG.(\w+?)"", ""TCG.(\w+?)\\TCG.(\w+?).csproj"", ""{(\w+?-\w+?-\w+?-\w+?-\w+?)}""", linesSLN[i]))
                        linesSLN[i] = $"Project(\"{{{Guid.NewGuid().ToString()}}}\") = \"TCG.{textBox1.Text}\", \"TCG.{textBox1.Text}\\TCG.{textBox1.Text}.csproj\", \"{{{strid1}}}\"";



                    if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Debug\|Any CPU.ActiveCfg = Debug\|Any CPU", linesSLN[i]))
                        linesSLN[i] = $"\t\t{{{strid1}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU";
                    if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Debug\|Any CPU.Build.0 = Debug\|Any CPU", linesSLN[i]))
                        linesSLN[i] = $"\t\t{{{strid1}}}.Debug|Any CPU.Build.0 = Debug|Any CPU";

                    if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Release\|Any CPU.ActiveCfg = Release\|Any CPU", linesSLN[i]))
                        linesSLN[i] = $"\t\t{{{strid1}}}.Release|Any CPU.ActiveCfg = Release|Any CPU";

                    if (change(@"{(\w+?-\w+?-\w+?-\w+?-\w+?)}.Release\|Any CPU.Build.0 = Release\|Any CPU", linesSLN[i]))
                        linesSLN[i] = $"\t\t{{{strid1}}}.Release|Any CPU.Build.0 = Release|Any CPU";


                    if (change(@"SolutionGuid = {(\w+?-\w+?-\w+?-\w+?-\w+?)}", linesSLN[i]))
                        linesSLN[i] = $"\t\tSolutionGuid = {{{Guid.NewGuid().ToString()}}}";

                }
                for (int i = 1; i < TCGClines.Length; i++)
                {
                    if (change("<RootNamespace>(\\w+?.\\w+?)</RootNamespace>", TCGClines[i]))
                        TCGClines[i] = $"    <RootNamespace>TCG.{textBox1.Text}</RootNamespace>";
                    if (change("<AssemblyName>(\\w+?.\\w+?)</AssemblyName>", TCGClines[i]))
                        TCGClines[i] = $"    <AssemblyName>TCG.{textBox1.Text}</AssemblyName>";
                    if (change("<ProductVersion>(\\w+?.\\w+?.\\w+?)</ProductVersion>", TCGClines[i]))
                        TCGClines[i] = $"    <ProductVersion>{textBox4.Text}</ProductVersion>";
                    if (change("<ProjectGuid>{(\\w+?-\\w+?-\\w+?-\\w+?-\\w+?)}</ProjectGuid>", TCGClines[i]))
                        TCGClines[i] = $"    <ProjectGuid>{{{strid1}}}</ProjectGuid>";
                    if (change("<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\\w+?)\\Public\\Slb.Ocean.Data.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Data.dll</HintPath>";
                    if (change("<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\\w+?)\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.Geology.dll</HintPath>>";
                    if (change("<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\\w+?)\\Public\\Slb.Ocean.Petrel.Modeling.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Slb.Ocean.Petrel.Modeling.dll</HintPath>";
                    if (change("<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\\w+?)\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.UI.Controls.dll</HintPath>";
                    if (change("<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\\w+?)\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Petrel.Well.dll</HintPath>";
                    if (change("<HintPath>C:\\Program Files\\Schlumberger\\Petrel (\\w+?)\\Public\\Slb.Ocean.Units.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>C:\\Program Files\\Schlumberger\\Petrel {comboBox1.Text}\\Public\\Slb.Ocean.Units.dll</HintPath>";
                    if (change("<HintPath>c:\\program files\\schlumberger\\petrel (\\w+?)\\Public\\slb.ocean.core.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\slb.ocean.core.dll</HintPath>";
                    if (change("<HintPath>c:\\program files\\schlumberger\\petrel (\\w+?)\\Public\\slb.ocean.petrel.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\slb.ocean.petrel</HintPath>";
                    if (change("<HintPath>c:\\program files\\schlumberger\\petrel (\\w+?)\\Public\\Slb.Ocean.Basics.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\Slb.Ocean.Basics.dll</HintPath>";
                    if (change("<HintPath>c:\\program files\\schlumberger\\petrel (\\w+?)\\Public\\Slb.Ocean.Geometry.dll</HintPath>", TCGClines[i]))
                        TCGClines[i] = $"<HintPath>c:\\program files\\schlumberger\\petrel {comboBox1.Text}\\Public\\Slb.Ocean.Geometry.dll</HintPath>";
                    if (change(@"""%25OCEAN(\w+?)HOME%25\\PluginPackager\.exe"" /g ""\$\(TargetPath\)"" ""\$\(ProjectDir\)\\plugin\.xml"" ""%25OCEAN(\w+?)HOME_x64%25\\petrel\.exe""", TCGClines[i]))
                        TCGClines[i] = $"\"%25OCEAN{comboBox1.Text}HOME%25\\PluginPackager.exe\" / g \"$(TargetPath)\" \"$(ProjectDir)\\plugin.xml\" \"%25OCEAN{comboBox1.Text}HOME_x64%25\\petrel.exe\"";
                    if (change(@"""%25OCEAN(\w+?)HOME%25\\PluginPackager.exe"" /m ""\$\(ProjectDir\)\\plugin.xml"" ""%25OCEAN(\w+?)HOME_x64%25\\petrel.exe"" ""\$\(TargetDir\)""</PostBuildEvent>", TCGClines[i]))
                        TCGClines[i] = $"\"%25OCEAN{comboBox1.Text}HOME%25\\PluginPackager.exe\" /m \"$(ProjectDir)\\plugin.xml\" \"%25OCEAN{comboBox1.Text}HOME_x64%25\\petrel.exe\" \"$(TargetDir)\"</PostBuildEvent>";
                }
                for (int i = 1; i < linesAPINI.Length; i++)
                {
                    if (change(@"AppName=(\w+)", linesAPINI[i]))
                        linesAPINI[i] = $"AppName={textBox1.Text}";
                    linesAPINI[i] = $"Version={textBox4.Text}";
                    if (change(@"Version=(\w+?).(\w+?).(\w+)", linesAPINI[i]))
                        linesAPINI[i] = $"Version={textBox4.Text}";
                    if (change(@"PetrelVersion=(\w+?).(\w+?)\(64-bit\)", linesAPINI[i]))
                        linesAPINI[i] = $"PetrelVersion={comboBox1.Text}";
                }

                Directory.CreateDirectory($"{path}\\{textBox1.Text}");
                Directory.CreateDirectory($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}");
                Directory.CreateDirectory($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Properties");
                Directory.CreateDirectory($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Resources");

                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Properties\Resources.resx", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Properties\\Resources.resx", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Properties\Settings.Designer.cs", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Properties\\Settings.Designer.cs", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Properties\Settings.settings", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Properties\\Settings.settings", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\app.config", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\app.config", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Fractures.key.snk", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Fractures.key.snk", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Module.cs", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Module.cs", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\newkey.snk", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\newkey.snk", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Workstep1UI.resx", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Workstep1UI.resx", true);
                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\build_plugin.bat", $"{path}\\{textBox1.Text}\\build_plugin.bat", true);

                File.Copy(@"C:\Users\Unf1n\Desktop\Unf1n\Проекты\DevPlugin\DevPlugin\bin\Debug\simple-plugin\TCG.SimplePlugin\Resources\Logo_Timal_Final32x32.png", $"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\Resources\\Logo_Timal_Final32x32.png", true);


                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\Properties\AssemblyInfo.cs", linesAssemblyInfo);
                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\Properties\Resources.Designer.cs", linesResourcesDesigner);

                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\Plugin.cs", linesPlugincs);
                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\plugin.xml", linesPlugin);
                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\TCG.{textBox1.Text}.csproj.user", linesCSpro);
                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\Workstep1.cs", linesWorkstep1);
                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\Workstep1UI.cs", linesWorkstep1UI);
                File.AppendAllLines($@"{path}\{textBox1.Text}\TCG.{textBox1.Text}\Workstep1UI.Designer.cs", linesWorkstep1UIDesigner);

                File.AppendAllLines($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}\\TCG.{textBox1.Text}.csproj", TCGClines);
                File.AppendAllLines($"{path}\\{textBox1.Text}\\TCG.{textBox1.Text}.sln", linesSLN);
                File.AppendAllLines($"{path}\\{textBox1.Text}\\Appinfo.ini", linesAPINI);


            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            button1.Visible = true;
        }

        string[] AppChange = new string[200];
        string filenameChange = "";
        string pathChang = "";
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            OpenFileDialog filed = new OpenFileDialog();
            if (filed.ShowDialog() == DialogResult.OK)
            {
                pathChang = filed.FileName;
                string pathth = "";
                for (int i = pathChang.Length - 1; i > 0; i--)
                {
                    int th = pathChang.Length - 1;
                    if (pathChang[i] == '.')
                    {
                        for (int i1 = i + 1; i1 < th + 1; i1++)
                        {
                            pathth += pathChang[i1];
                        }
                        break;
                    }
                }

                if (pathth != "sln")
                {
                    MessageBox.Show("Wrong project file, select \"sln\" file");
                    button3_Click(sender, e);
                }
                else
                {
                    for (int i = pathChang.Length - 1; i > 0; i--)
                    {
                        if (pathChang[i] == '\\')
                        {
                            pathChang = pathChang.Substring(0, i);
                            break;
                        }
                    }
                    filenameChange = $"{pathChang}\\Appinfo.ini";
                    try
                    {
                        AppChange = WriteF(filenameChange);
                    }
                    catch
                    {
                        MessageBox.Show("Wrong project file");
                        button3_Click(sender, e);
                    }
                    for (int i1 = 1; i1 < AppChange.Length; i1++)
                    {
                        string[] name = AppChange[1].Split('=');

                        textBox1.Text = name[1];
                    }
                    for (int i1 = 1; i1 < AppChange.Length; i1++)
                    {
                        string[] name = AppChange[3].Split('=');

                        textBox4.Text = name[1];
                    }
                    for (int i1 = 1; i1 < AppChange.Length; i1++)
                    {
                        string[] name = AppChange[4].Split('=');

                        comboBox1.Text = name[1];
                    }

                }

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            string lastname = "";
            string newname = "";
            SaveFileDialog open = new SaveFileDialog();
            open.FileName = textBox1.Text;
            if (open.ShowDialog() != DialogResult.Cancel)
            {
                for (int i = pathChang.Length - 1; i > 0; i--)
                {
                    int th = pathChang.Length - 1;
                    if (pathChang[i] == '\\')
                    {
                        for (int i1 = i + 1; i1 < th + 1; i1++)
                        {
                            lastname += pathChang[i1];
                        }
                        break;
                    }
                }
                for (int i = open.FileName.Length - 1; i > 0; i--)
                {
                    int th = open.FileName.Length - 1;
                    if (open.FileName[i] == '\\')
                    {
                        for (int i1 = i + 1; i1 < th + 1; i1++)
                        {
                            newname += open.FileName[i1];
                        }
                        break;
                    }
                }
                textBox1.Text = newname;
                #region Copy Folder
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = @"C:\WINDOWS\system32\xcopy.exe";
                proc.StartInfo.Arguments = $"{pathChang} {open.FileName} /E /I";
                proc.Start();
                #endregion
                Thread.Sleep(2000);
                Create(pathChang, lastname);
                System.IO.Directory.Move($"{open.FileName}\\TCG.{lastname}", $"{open.FileName}\\TCG.{textBox1.Text}");
                File.Delete($"{open.FileName}\\Appinfo.ini");
                File.Delete($"{open.FileName}\\TCG.{lastname}.sln");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\TCG.{lastname}.csproj");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\TCG.{lastname}.csproj.user");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Module.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Plugin.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\plugin.xml");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Workstep1.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Workstep1UI.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Workstep1UI.Designer.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Properties\\AssemblyInfo.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Properties\\Resources.Designer.cs");
                File.Delete($"{open.FileName}\\TCG.{textBox1.Text}\\Properties\\Settings.Designer.cs");

                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Properties\AssemblyInfo.cs", linesAssemblyInfo);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Properties\Resources.Designer.cs", linesResourcesDesigner);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Properties\Settings.Designer.cs", linesSTD);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Plugin.cs", linesPlugincs);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\plugin.xml", linesPlugin);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\TCG.{textBox1.Text}.csproj.user", linesCSpro);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Workstep1.cs", linesWorkstep1);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Workstep1UI.cs", linesWorkstep1UI);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Workstep1UI.Designer.cs", linesWorkstep1UIDesigner);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\TCG.{textBox1.Text}.csproj", TCGClines);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}.sln", linesSLN);
                File.AppendAllLines($@"{open.FileName}\TCG.{textBox1.Text}\Module.cs", linesModu);
                File.AppendAllLines($@"{open.FileName}\Appinfo.ini", linesAPINI);
            }
           



        }

        private void newFileToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

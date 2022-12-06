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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = "";
            string[] args = Environment.GetCommandLineArgs();
            int a=0;
            foreach (string arg in args)
            {
                if (a == 1)
                {
                    path= arg;
                }
                    a++;
            }

            string filename = path;
            var lines = File.ReadAllLines(filename);
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Split('=');
                switch (i)
                {
                    case 1:
                        label2.Text = line[1];
                        break;
                    case 3:
                        label5.Text = line[1];
                        break;
                    case 4:
                        comboBox1.Items.Add(line[1]);
                        break;
                }
            }
        }
    }
}

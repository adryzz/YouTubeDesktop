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

namespace YouTubeDesktop
{
    public partial class Form3 : Form
    {
        string[] Config;
        string ConfigPath;
        Form1 Form1;
        public Form3(string configpath, Form1 f1)
        {
            InitializeComponent();
            ConfigPath = configpath;
            Config = File.ReadAllLines(configpath);
            Form1 = f1;
            if (Config[0].EndsWith("true"))
            {

            }
            else if (Config[0].EndsWith("false"))
            {
                button3.Text = "Enable all extensions";
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure?", "YouTubeDesktop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                //Close the browser, then delete all files in appdata/YTDesktop
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure?", "YouTubeDesktop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                //Close the browser, then delete all files in appdata/YTDesktop/Cache, CodeCache and GPUCache
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Config[0].EndsWith("true"))
            {
                button3.Text = "Enable all extensions";
                Config[0] = Config[0].Replace("true", "false");
            }
            else if (Config[0].EndsWith("false"))
            {
                button3.Text = "Disable all extensions";
                Config[0] = Config[0].Replace("false", "true");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(ConfigPath, Config);
            Application.Exit();
        }


        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.Enabled = true;//re-enable form1
        }

    }
}

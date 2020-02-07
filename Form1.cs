using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using CefSharp;
using CefSharp.WinForms;

namespace YouTubeDesktop
{
    public partial class Form1 : Form
    {
        bool FullScreen = false;
        Location PLocation;
        string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YouTubeDesktop", "config.txt");
        string[] Config;

        public Form1()
        {
            InitializeConfig(ConfigPath);//create config if it doesn't exist
            Config = File.ReadAllLines(ConfigPath);//read config
            InitializeComponent();//initialize gui
            InitializeChromium("https://youtube.com/", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YouTubeDesktop"));
            //initialize browser
            if (Config[0].EndsWith("true"))//Check if extensions are enabled, and run them if they are
            {
                InitializeExtensions(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YouTubeDesktop", "Extensions"));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Using cef version: " + Cef.CefVersion);
            if (Config[1].EndsWith("true"))//check for first run, and show a messagebox if so
            {
                Config[1] = Config[1].Replace("true", "false");//set first run to false
                File.WriteAllLines(ConfigPath, Config);
                MessageBox.Show("YouTubeDesktop is a youtube desktop app. (this is a wrapper but shhh)\nThis is an UNOFFICIAL app.\nHave fun!", "YouTubeDesktop");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private delegate void Title(string title);

        private delegate void FScreen();

        private delegate Form GetForm();

        private void ChangeTitle(string title)
        {
            Text = title;
        }

        private void ChangeFullScreen()
        {
            if (FullScreen)
            {
                Location = PLocation.Position;
                Bounds = PLocation.Bounds;
                FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                PLocation = new Location(Location, Bounds);
                FormBorderStyle = FormBorderStyle.None;
                Location = new Point(0, 0);
                Bounds = Screen.PrimaryScreen.Bounds;
            }
        }

        private Form GetWindow()
        {
            return this;
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
           if (e.Address.Contains("youtube"))//allow only yt pages
           {
                //Notification not = new Notification((Location.X + Width/2)-150, Location.Y, "Page loaded", 3000);
                //not.Show();
           }
           else
           {
                Process.Start(e.Address);//run any non-youtube link in the default browser
                Browser.Back();
           }
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Invoke(new Title(ChangeTitle), e.Title);
        }

        private void Browser_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)//If F11 key is pressed
            {
                Invoke(new FScreen(ChangeFullScreen));
                e.Handled = true;
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.KeyCode == Keys.S)//Ctrl+S, open Settings
            {
                Form3 f3 = new Form3(ConfigPath, this);
                f3.Show();
                Enabled = false;//disable form1, so that you cannot mess up with config while watching a video
                e.Handled = true;
            }
        }
    }
}

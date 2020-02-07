using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;

namespace YouTubeDesktop
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public ChromiumWebBrowser Browser;
        List<Assembly> Assemblies = new List<Assembly>();

        public void InitializeChromium(string url, string cachepath)
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            settings.CachePath = cachepath;
            //Handle cookies, such as yt login
            if (!Directory.Exists(Path.Combine(cachepath, "Extensions")))
            {
                Directory.CreateDirectory(Path.Combine(cachepath, "Extensions"));
            }

            Cef.Initialize(settings);
            // Create a browser component
            Browser = new ChromiumWebBrowser(url);
            // Add it to the form and fill it to the form window.

            this.Controls.Add(Browser);
            Browser.Dock = DockStyle.Fill;
            Browser.AddressChanged += Browser_AddressChanged;
            Browser.TitleChanged += Browser_TitleChanged;
            KeyDown += Browser_KeyDown;
        }

        public void InitializeExtensions(string extpath)
        {
            foreach (string dll in Directory.GetFiles(extpath, "*.dll"))
            {
                Assemblies.Add(Assembly.LoadFile(dll));
            }

            foreach (Assembly DLL in Assemblies)
            {

                foreach (Type type in DLL.GetExportedTypes())
                {
                    try
                    {
                        var c = Activator.CreateInstance(type);
                        type.InvokeMember("Initialize", BindingFlags.InvokeMethod, null, c, new object[] { this });
                    }
                    catch (MissingMethodException)
                    {
                        MessageBox.Show(String.Format("Could not initialize the extension {0}: The extension is for another version of YouTubeDesktop, is damaged or not working (Missing Initialize() method)", Path.GetFileName(DLL.Location)), "YouTubeDesktop - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        public void InitializeConfig(string cfgpath)
        {
            if (!File.Exists(cfgpath))
            {
                List<string> cfg = new List<string>();
                cfg.Add("EnableExtensions = true");
                cfg.Add("FirstRun = true");
                File.WriteAllLines(cfgpath, cfg);
            }
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDesktop
{
    public partial class Notification : Form
    {
        public Notification(int x, int y, string text, int delay)
        {
            InitializeComponent();
            Location = new Point(x, y);
            label1.Text = text;
            Wait(delay);
        }

        private void Notification_Load(object sender, EventArgs e)
        {

        }

        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
            Close();
            Dispose();
        }

        private async void Wait(int delay)
        {
            await PutTaskDelay(delay);
        }
    }
}

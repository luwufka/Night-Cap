using NightCap.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace NightCap
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Window window = new Window(this.Handle, Window.Style.Dark);
            window.IsDark(true);
            versionLabel.Text = GetVersion();
        }

        private string GetVersion()
        {
            List<string> vp = new List<string>(Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.'));
            vp.RemoveAt(3);
            return string.Join(".", vp) + " - Release";

        }

        

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void githubPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/luwufka");
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}

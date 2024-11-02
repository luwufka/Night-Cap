using NightCap.Core;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NightCap
{
    public class Program
    {
        static Form aboutForm = new About();
        const string MUTEX_NAME = "Night_Cap_Mutex";

        private static ContextMenuStrip ContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            menu.ForeColor = Color.White;
            menu.Font = new Font("Segoe UI", 9);
            menu.Renderer = new ToolStripProfessionalRenderer(new DarkStrip());

            ToolStripMenuItem exit = new ToolStripMenuItem("Exit", Properties.Resources.Close);
            ToolStripMenuItem about = new ToolStripMenuItem("About", Properties.Resources.About);

            menu.Items.Add(about);
            menu.Items.Add(exit);

            exit.Click += exitClick;
            about.Click += aboutClick;

            return menu;
        }

        private static void aboutClick(object sender, EventArgs e)
        {
            aboutForm.Show();
        }

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool onlyInstance;
            new Mutex(true, MUTEX_NAME, out onlyInstance);
            if (!onlyInstance)
            {
                Environment.Exit(-1);
            }

            Utils.CheckSystem();

            NotifyIcon trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.AppIcon,
                Text = "Night Cap",
                Visible = true
            };

            ContextMenuStrip menu = ContextMenu();
            trayIcon.ContextMenuStrip = menu;

            Updater updater = new Updater();
            updater.WindowCreated += windowCreated;
            updater.Start();

            Application.Run();
        }

        private static void windowCreated(Window window)
        {
            try
            {
                if (window.WindowStyle != Window.Style.Dark)
                {
                    window.IsDark(true);
                }
            }
            catch { }
        }

        private static void exitClick(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }
    }

    public class DarkStrip : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(80, 80, 80);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(70, 70, 70);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(70, 70, 70);
        public override Color MenuItemBorder => Color.FromArgb(90, 90, 90);
        public override Color MenuBorder => Color.FromArgb(60, 60, 60);
        public override Color ToolStripDropDownBackground => Color.FromArgb(40, 40, 40);
        public override Color ImageMarginGradientBegin => Color.FromArgb(40, 40, 40);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(40, 40, 40);
        public override Color ImageMarginGradientEnd => Color.FromArgb(40, 40, 40);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(60, 60, 60);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(60, 60, 60);
        public override Color MenuItemPressedGradientMiddle => Color.FromArgb(60, 60, 60);
    }
}

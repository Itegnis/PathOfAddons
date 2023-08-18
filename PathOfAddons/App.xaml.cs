using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Forms = System.Windows.Forms;

namespace PathOfAddons
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Forms.NotifyIcon _notifyIcon;

        public App()
        {
            _notifyIcon = new Forms.NotifyIcon();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            _notifyIcon.Icon = new System.Drawing.Icon("Resources/gear.ico");
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "PathOfAddons";
            _notifyIcon.Click += NotifyIcon_Click;

            _notifyIcon.ContextMenu = new Forms.ContextMenu();
            _notifyIcon.ContextMenu.MenuItems.Add("Exit", OnExitClicked);
            
            base.OnStartup(e);
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.ShowInTaskbar = true;
            MainWindow.Activate();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }


}

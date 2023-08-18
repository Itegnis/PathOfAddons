using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace PathOfAddons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void themeToogle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (theme.GetBaseTheme() == BaseTheme.Dark)
            {
                theme.SetBaseTheme(Theme.Light);
                Properties.Settings.Default.themeStatus = false;
            }
            else
            {
                theme.SetBaseTheme(Theme.Dark);
                Properties.Settings.Default.themeStatus = true;
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Properties.Settings.Default.Save();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        //SetPath

        private void poePath_Click(object sender, RoutedEventArgs e)
        {
            var poePath = new OpenFileDialog();
            poePath.Filter = "Client.exe|*.exe";
            poePath.ShowDialog(this);
            pathParent.Text = poePath.FileName;
            Properties.Settings.Default.pathParent = pathParent.Text;
        }

        private void firstPath_Click(object sender, RoutedEventArgs e)
        {
            var firstPath = new OpenFileDialog();
            firstPath.Filter = "Addons (.exe or .ahk)|*.exe;*.ahk";
            firstPath.ShowDialog(this);
            pathChild1.Text = firstPath.FileName;
            Properties.Settings.Default.pathChild1 = pathChild1.Text;
        }

        private void secondPath_Click(object sender, RoutedEventArgs e)
        {
            var secondPath = new OpenFileDialog();
            secondPath.Filter = "Addons (.exe or .ahk)|*.exe;*.ahk";
            secondPath.ShowDialog(this);
            pathChild2.Text = secondPath.FileName;
            Properties.Settings.Default.pathChild2 = pathChild2.Text;
        }

        private void thirdPath_Click(object sender, RoutedEventArgs e)
        {
            var thirdPath = new OpenFileDialog();
            thirdPath.Filter = "Addons (.exe or .ahk)|*.exe;*.ahk";
            thirdPath.ShowDialog(this);
            pathChild3.Text = thirdPath.FileName;
            Properties.Settings.Default.pathChild3 = pathChild3.Text;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
            Properties.Settings.Default.Save();


            string[] childPath = new string[3];
            childPath[0] = pathChild1.Text;
            childPath[1] = pathChild2.Text;
            childPath[2] = pathChild3.Text;


            Process parentProcess = new Process();
            if (pathParent.Text != "")
            {
                parentProcess.StartInfo.FileName = pathParent.Text;
                parentProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(pathParent.Text);
                parentProcess.Start();




                Process[] childProcess = new Process[3];
                for (int i = 0; i < childProcess.Length; i++)
                {
                    if (childPath[i] != "")
                    {
                        childProcess[i] = new Process();
                        childProcess[i].StartInfo.FileName = childPath[i];
                        childProcess[i].StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(childPath[i]);
                        childProcess[i].Start();
                    }
                }
                parentProcess.WaitForExit();

                for (int i = 0; i < childProcess.Length; i++)
                {
                    if (childPath[i] != "")
                    {
                        try
                        {
                            childProcess[i].Kill();
                        }
                        catch { }
                    }
                }
                this.WindowState = WindowState.Normal;
                this.ShowInTaskbar = true;
                Activate();
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (Properties.Settings.Default.themeStatus == false)
            {
                themeToogle.IsChecked = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                themeToogle.IsChecked = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            pathParent.Text = Properties.Settings.Default.pathParent;
            pathChild1.Text = Properties.Settings.Default.pathChild1;
            pathChild2.Text = Properties.Settings.Default.pathChild2;
            pathChild3.Text = Properties.Settings.Default.pathChild3;
            paletteHelper.SetTheme(theme);
        }
    }
}

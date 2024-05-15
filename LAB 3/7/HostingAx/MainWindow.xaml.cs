using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HostingAx
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the ActiveX control.
            WmpAxLib.WmpAxControl axWmp = new WmpAxLib.WmpAxControl();

            // Assign the ActiveX control as the host control's child.
            host.Child = axWmp.axWindowsMediaPlayer1;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);

            // Play a .wav file with the ActiveX control.
            axWmp.axWindowsMediaPlayer1.URL = @"C:\Users\Pawel\Desktop\COMLab3\7\Ring01.wav";
        }
    }
}

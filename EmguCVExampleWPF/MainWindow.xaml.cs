using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV.Util;
using AForge.Video.DirectShow;
using Quadrep.Device;
using Quadrep.Interface;
using System.IO;
using System.Drawing;
using System.Threading;
using Quadrep.Camera;
namespace Quadrep
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MainPanel.Children.Add(new CameraControl());
            MainPanel.Children.Add(new CV.ImageProcessingControl());
        }
    }
}

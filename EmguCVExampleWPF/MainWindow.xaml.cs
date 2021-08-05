using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.Cuda;
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

namespace Quadrep
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _vmModel = new ViewModel();
        private Dictionary<string, string> _deviceMap = new Dictionary<string, string>();
        private ICamera _cam;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _vmModel;
            SearchWebCam();
            var image = CvInvoke.Imread("images.jpg", Emgu.CV.CvEnum.ImreadModes.Color);
            var img = new Image<Bgr, byte>("images.jpg");
            _vmModel.ImgDown = ToImageSource(img.ToBitmap());
            //var bitmap = new Bitmap("123.jpg");
            
            //var img = new Image<Bgr, byte>(bitmap)
            //CvInvoke.Imshow("image", image);

            var result = new VectorOfPoint();
            var s = CvInvoke.FindChessboardCorners(image, new System.Drawing.Size(4, 3), result);

            //imgbox.Source = BitmapSourceConvert.ToBitmapSource(image);
            //image.Dispose();
            //CvInvoke.Imshow("result", result);
            //CvInvoke.WaitKey();
            ;
        }

        private void Btn_SearchWebcam(object sender, RoutedEventArgs e) => SearchWebCam();

        private ImageSource ToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }
        private void SearchWebCam()
        {
            var USB_Webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            _vmModel.Devices.Clear();
            _deviceMap.Clear();
            _vmModel.IsDeviceExit = false;
            if (USB_Webcams.Count > 0)
            {
                foreach (var webcam in USB_Webcams)
                {
                    _vmModel.Devices.Add(((FilterInfo)webcam).Name);
                    var str = ((FilterInfo)webcam).MonikerString;
                    _deviceMap.Add(((FilterInfo)webcam).Name, ((FilterInfo)webcam).MonikerString);
                }
                _vmModel.DevicesIndex = 0;
                _vmModel.IsDeviceExit = true;
            }
        }
        private void Disconnect()
        {
            if (_cam != null && _cam.IsConnected)
            {
                _cam.BitmapEvent -= _cam_BitmapEvent;
                _cam.Disconnect();
            }
        }
        private void Connect()
        {
            var str = _deviceMap[_vmModel.Devices[_vmModel.DevicesIndex]];
            _cam = new Device.Webcam(_deviceMap[_vmModel.Devices[_vmModel.DevicesIndex]]);
            _cam.Connect();
            _cam.BitmapEvent += _cam_BitmapEvent;
        }

        private void Btn_Connect(object sender, RoutedEventArgs e) => Connect();

        private void _cam_BitmapEvent(System.Drawing.Bitmap bitmap)
        { 
            this.Dispatcher.Invoke(() =>
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();
                    _vmModel.ImgUp = bitmapimage;
                }
            });
        }

        private void Btn_Disconnect(object sender, RoutedEventArgs e) => Disconnect();

        private void Btn_Capture(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Disconnect();
    }
}

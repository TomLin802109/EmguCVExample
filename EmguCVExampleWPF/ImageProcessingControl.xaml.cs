using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Quadrep.Presenter;
using Quadrep.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace Quadrep.CV
{
    /// <summary>
    /// Interaction logic for ImageProcessingControl.xaml
    /// </summary>
    public partial class ImageProcessingControl : UserControl
    {
        private ViewModel _vmModel = new ViewModel();
        private Dictionary<string, string> _deviceMap = new Dictionary<string, string>();
        private ICamera _cam;
        private YOLOv3 Detector = null;
        public ImageProcessingControl()
        {
            InitializeComponent();
            this.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            this.DataContext = _vmModel;
            SearchWebCam();

            //var path = "CalibBoard1.jpg";
            //var image = CvInvoke.Imread(path, Emgu.CV.CvEnum.ImreadModes.Color);
            //CvInvoke.Resize(image, image, new System.Drawing.Size(480, 640));
            //Mat imgGray = new Mat();
            //var gray = new Image<Gray, byte>(image.Width, image.Height, new Gray(128));
            //CvInvoke.CvtColor(image, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            //CvInvoke.CvtColor(image, imgGray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            ////CvInvoke.AdaptiveThreshold(gray, gray, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Otsu, 3, 1);
            //var th = CvInvoke.Threshold(gray, gray, 200, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
            //Mat imgHsv = new Mat();
            //CvInvoke.CvtColor(image, imgHsv, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);
            //var hsv = imgHsv.Split();
            //var result = new VectorOfPoint();
            //CvInvoke.FindChessboardCorners(gray, new System.Drawing.Size(11, 15), result);
            //var points = result.ToArray();
            //foreach(var i in points)
            //    CvInvoke.Circle(image, i, 1, new MCvScalar(0, 0, 255),2);

            //CvInvoke.Imshow("img", image);
            //CvInvoke.Imshow("gray", gray);
            ////CvInvoke.Imshow("hsv", imgHsv);
            //CvInvoke.Imshow("H", hsv[0]);
            //CvInvoke.Imshow("S", hsv[1]);
            //CvInvoke.Imshow("V", hsv[2]);
            //CvInvoke.Imshow("cornor", cornor);

            //var img = new Image<Bgr, byte>("images.jpg");
            //_vmModel.ImgDown = ToImageSource(img.ToBitmap());
            //var bitmap = new Bitmap("123.jpg");

            //var img = new Image<Bgr, byte>(bitmap)
            //var s = CvInvoke.FindChessboardCorners(image, new System.Drawing.Size(4, 3), result);

            //imgbox.Source = BitmapSourceConvert.ToBitmapSource(image);
            //image.Dispose();
            //CvInvoke.Imshow("result", result);
            //CvInvoke.WaitKey();
            ;
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            Btn_StopDetect(this, null);
            Disconnect();
        }
        private void Btn_SearchWebcam(object sender, RoutedEventArgs e) => SearchWebCam();

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
            _cam.Exposure = -7;
            _cam.BitmapEvent += _cam_BitmapEvent;
        }

        private void Btn_Connect(object sender, RoutedEventArgs e) => Connect();

        private void _cam_BitmapEvent(System.Drawing.Bitmap bitmap)
        {
            this.Dispatcher.Invoke(() => _vmModel.ImgUp = bitmap.ToImageSource());
        }

        private void Btn_Disconnect(object sender, RoutedEventArgs e) => Disconnect();

        private void Btn_Capture(object sender, RoutedEventArgs e)
        {
            if (_vmModel.ImgUp == null) return;
            var img = _vmModel.ImgUp.ToBitmap().ToImage<Bgr, byte>().Mat;
            CvInvoke.Imshow("img", img);
        }
        
        private async void Btn_ShowMat(object sender, RoutedEventArgs e)
        {
            if (Detector == null) Detector = new YOLOv3("yolov3.cfg", "yolov3.weights", "coco.names");
            var img = _vmModel.ImgUp.ToBitmap().ToImage<Bgr, byte>().Mat;
            //var img = new Image<Bgr, byte>("dog.jpg").Mat;
            var st = DateTime.Now;
            var boxes = Detector.Detected(img, false);
            foreach (var i in boxes) img.Draw(i, System.Drawing.Color.Red);
            var bmp = img.ToBitmap();
            this.Dispatcher?.Invoke(() => _vmModel.ImgDown = bmp.ToImageSource());
            var ct = (DateTime.Now - st).TotalSeconds;
            var fps = 1 / ct;
            ;
            //var path = "CalibBoard1.jpg";
            //var image1 = CvInvoke.Imread(path, Emgu.CV.CvEnum.ImreadModes.Color);
            //var image2 = CvInvoke.Imread("CalibBoard2.jpg", Emgu.CV.CvEnum.ImreadModes.Color);
            //await Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        if (i % 2 == 1) this.Dispatcher.Invoke(() => _vmModel.ImgDown = image1.ToBitmap().ToImageSource());
            //        else this.Dispatcher.Invoke(() => _vmModel.ImgDown = image2.ToBitmap().ToImageSource());
            //        Thread.Sleep(1000);
            //    }
            //});
        }

        

        private void Btn_ShowTable(object sender, RoutedEventArgs e)
        {
            var ls = new List<BBox>();
            for(int i = 0; i < 50; i++)
            {
                ls.Add(new BBox(i, 123.546f, i * 2,i*3,150-i,30+i ));
            }
            var window = new SubWindow(new DataTableControl(ls), "Data Viewer");
            window.Show();
        }

        private void Btn_StartDetect(object sender, RoutedEventArgs e)
        {
            if(Detector==null) Detector = new YOLOv3("yolov4.cfg", "yolov4.weights", "coco.names");
            _cam.BitmapEvent -= _cam_BitmapEvent;
            _cam.BitmapEvent += _cam_BitmapEventYOLO;
            //var img = _vmModel.ImgUp.ToBitmap().ToImage<Bgr, byte>().Mat;
            ////var img = new Image<Bgr, byte>("dog.jpg").Mat;
            //var st = DateTime.Now;
            //var boxes = Detector.Detected(img, false);
            //foreach (var i in boxes) img.Draw(i, System.Drawing.Color.Red);
            //var bmp = img.ToBitmap();
            //this.Dispatcher?.Invoke(() => _vmModel.ImgDown = bmp.ToImageSource());
            //var ct = (DateTime.Now - st).TotalSeconds;
            //var fps = 1 / ct;
            ;
        }
        private bool IsProcessing = false;
        private void _cam_BitmapEventYOLO(Bitmap obj)
        {
            if (IsProcessing || Detector == null) return;
            IsProcessing = true;
            var img = obj.ToImage<Bgr, byte>().Mat;
            var st = DateTime.Now;
            var boxes = Detector.Detected(img, false);
            foreach (var i in boxes) img.Draw(i, System.Drawing.Color.Red);
            var ct = (DateTime.Now - st).TotalSeconds;
            CvInvoke.PutText(img, $"FPS:{1 / ct:F1}", new System.Drawing.Point(0, img.Height), FontFace.HersheyComplex, 
                                         1, new Bgr(System.Drawing.Color.Red).MCvScalar, 3);
            this.Dispatcher.Invoke(() => _vmModel.ImgDown = img.ToBitmap().ToImageSource());
            IsProcessing = false;
        }

        private void Btn_StopDetect(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_cam != null)
                {
                    
                    _cam.BitmapEvent += _cam_BitmapEvent;
                    _cam.BitmapEvent -= _cam_BitmapEventYOLO;
                    
                }
                
                //IsProcessing = false;
            });
            
            
        }
    }
}

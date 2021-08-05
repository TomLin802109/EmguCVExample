using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Quadrep
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private ImageSource imgUp;
        public ImageSource ImgUp
        {
            get => imgUp;
            set
            {
                imgUp = value;
                OnPropertyChanged();
            }
        }

        private ImageSource imgDown;
        public ImageSource ImgDown
        {
            get => imgDown;
            set
            {
                imgDown = value;
                OnPropertyChanged();
            }
        }
        private bool isConnected = false;
        public bool IsConnected
        {
            get => isConnected;
            set
            {
                isConnected = value;
                IsDeviceExitxXorIsConnected = value;
                OnPropertyChanged();
            }
        }

        private bool isDeviceExit = false;
        public bool IsDeviceExit
        {
            get => isDeviceExit;
            set
            {
                isDeviceExit = value;
                IsDeviceExitxXorIsConnected = value;
                OnPropertyChanged();
            }
        }

        public bool IsDeviceExitxXorIsConnected
        {
            get => isDeviceExit ^ isConnected;
            set => OnPropertyChanged();
        }

        private int focus_min = 0;
        public int Focus_min
        {
            get => focus_min;
            set
            {
                focus_min = value;
                OnPropertyChanged();
            }
        }

        private int focus_max = 0;
        public int Focus_max
        {
            get => focus_max;
            set
            {
                focus_max = value;
                OnPropertyChanged();
            }
        }

        private int focus = 0;
        public int Focus
        {
            get => focus;
            set
            {
                focus = value;
                OnPropertyChanged();
            }
        }

        private int exposure_min = 0;
        public int Exposure_min
        {
            get => exposure_min;
            set
            {
                exposure_min = value;
                OnPropertyChanged();
            }
        }

        private int exposure_max = 0;
        public int Exposure_max
        {
            get => exposure_max;
            set
            {
                exposure_max = value;
                OnPropertyChanged();
            }
        }

        private int exposure = 0;
        public int Exposure
        {
            get => exposure;
            set
            {
                exposure = value;
                OnPropertyChanged();
            }
        }

        private int brightness_min = 0;
        public int Brightness_min
        {
            get => brightness_min;
            set
            {
                brightness_min = value;
                OnPropertyChanged();
            }
        }

        private int brightness_max = 0;
        public int Brightness_max
        {
            get => brightness_max;
            set
            {
                brightness_max = value;
                OnPropertyChanged();
            }
        }

        private int brightness = 0;
        public int Brightness
        {
            get => brightness;
            set
            {
                brightness = value;
                OnPropertyChanged();
            }
        }

        private int colorEnable_min = 0;
        public int ColorEnable_min
        {
            get => colorEnable_min;
            set
            {
                colorEnable_min = value;
                OnPropertyChanged();
            }
        }

        private int colorEnable_max = 0;
        public int ColorEnable_max
        {
            get => colorEnable_max;
            set
            {
                colorEnable_max = value;
                OnPropertyChanged();
            }
        }

        private int colorEnable = 0;
        public int ColorEnable
        {
            get => colorEnable;
            set
            {
                colorEnable = value;
                OnPropertyChanged();
            }
        }

        private int contrast_min = 0;
        public int Contrast_min
        {
            get => contrast_min;
            set
            {
                contrast_min = value;
                OnPropertyChanged();
            }
        }

        private int contrast_max = 0;
        public int Contrast_max
        {
            get => contrast_max;
            set
            {
                contrast_max = value;
                OnPropertyChanged();
            }
        }

        private int contrast = 0;
        public int Contrast
        {
            get => contrast;
            set
            {
                contrast = value;
                OnPropertyChanged();
            }
        }

        private int gain_min = 0;
        public int Gain_min
        {
            get => gain_min;
            set
            {
                gain_min = value;
                OnPropertyChanged();
            }
        }

        private int gain_max = 0;
        public int Gain_max
        {
            get => gain_max;
            set
            {
                gain_max = value;
                OnPropertyChanged();
            }
        }

        private int gain = 0;
        public int Gain
        {
            get => gain;
            set
            {
                gain = value;
                OnPropertyChanged();
            }
        }

        private int gamma_min = 0;
        public int Gamma_min
        {
            get => gamma_min;
            set
            {
                gamma_min = value;
                OnPropertyChanged();
            }
        }

        private int gamma_max = 0;
        public int Gamma_max
        {
            get => gamma_max;
            set
            {
                gamma_max = value;
                OnPropertyChanged();
            }
        }

        private int gamma = 0;
        public int Gamma
        {
            get => gamma;
            set
            {
                gamma = value;
                OnPropertyChanged();
            }
        }

        private int hue_min = 0;
        public int Hue_min
        {
            get => hue_min;
            set
            {
                hue_min = value;
                OnPropertyChanged();
            }
        }

        private int hue_max = 0;
        public int Hue_max
        {
            get => hue_max;
            set
            {
                hue_max = value;
                OnPropertyChanged();
            }
        }

        private int hue = 0;
        public int Hue
        {
            get => hue;
            set
            {
                hue = value;
                OnPropertyChanged();
            }
        }

        private int saturation_min = 0;
        public int Saturation_min
        {
            get => saturation_min;
            set
            {
                saturation_min = value;
                OnPropertyChanged();
            }
        }

        private int saturation_max = 0;
        public int Saturation_max
        {
            get => saturation_max;
            set
            {
                saturation_max = value;
                OnPropertyChanged();
            }
        }

        private int saturation = 0;
        public int Saturation
        {
            get => saturation;
            set
            {
                saturation = value;
                OnPropertyChanged();
            }
        }

        private int sharpness_min = 0;
        public int Sharpness_min
        {
            get => sharpness_min;
            set
            {
                sharpness_min = value;
                OnPropertyChanged();
            }
        }

        private int sharpness_max = 0;
        public int Sharpness_max
        {
            get => sharpness_max;
            set
            {
                sharpness_max = value;
                OnPropertyChanged();
            }
        }

        private int sharpness = 0;
        public int Sharpness
        {
            get => sharpness;
            set
            {
                sharpness = value;
                OnPropertyChanged();
            }
        }

        private int whiteBalance_min = 0;
        public int WhiteBalance_min
        {
            get => whiteBalance_min;
            set
            {
                whiteBalance_min = value;
                OnPropertyChanged();
            }
        }

        private int whiteBalance_max = 0;
        public int WhiteBalance_max
        {
            get => whiteBalance_max;
            set
            {
                whiteBalance_max = value;
                OnPropertyChanged();
            }
        }

        private int whiteBalance = 0;
        public int WhiteBalance
        {
            get => whiteBalance;
            set
            {
                whiteBalance = value;
                OnPropertyChanged();
            }
        }

        private List<string> devices = new List<string>();
        public List<string> Devices
        {
            get => devices;
            set
            {
                devices = value;
                OnPropertyChanged();
            }
        }

        private int devicesIndex;
        public int DevicesIndex
        {
            get => devicesIndex;
            set
            {
                devicesIndex = value;
                OnPropertyChanged();
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
}

using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Kinect;

namespace Model
{
    public class KinectManager : INotifyPropertyChanged
    {
        public bool Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged(nameof(status));
            }
        }
        private bool status = false;
        public string StatusText
        {
            get
            {
                return statusText;
            }

            set
            {
                statusText = value;
                OnPropertyChanged(nameof(statusText));
            }
        }
        private string statusText = null;

        public KinectSensor Sensor;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void StartSensor()
        {
            if(!Sensor.IsOpen) { 
                Sensor.Open();
            }
        }

        public void StopSensor()
        {
            if (Sensor.IsOpen)
            {
                Sensor.Close();
            }
        }

        public KinectManager()
        {
            Sensor = KinectSensor.GetDefault();
            Sensor.IsAvailableChanged += KinectSensor_IsAvailableChanged;
            StartSensor();
        }

        ~KinectManager()
        {
            StopSensor();
        }

        private void KinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            StatusText = Sensor.IsAvailable ? "Running" : "Kinect Sensor not Available";
            Status = Sensor.IsAvailable;
        }
    }
}

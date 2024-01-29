using KinectExercises.Stream;
using Model.Stream;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace KinectExercises.ViewModels
{
    public partial class MainWindowVM : ObservableObject
    {

        [ObservableProperty]
        private KinectStream? stream = null;

        [ObservableProperty]
        private KinectManager manager = new();
        private KinectStreamFactory factory;

        public MainWindowVM() 
        {
            factory = new(manager);
        }

        [RelayCommand]
        public void Closing()
        {
            Manager.StopSensor();
        }

        [RelayCommand]
        public void Start()
        {
            Manager.StartSensor();
        }

        [RelayCommand]
        public void SwitchStream(object parameter)
        {
            if (parameter is string sort)
            {
                var enumValue = StreamType.None;
                Enum.TryParse(sort, out enumValue);
                Stream = factory[enumValue];
            }
        }
    }
}

using Microsoft.Kinect;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using KinectExercises.ViewModels;
using System.Windows.Controls;

namespace Streams
{
    public class BodyAndColorImageStream : KinectStream
    {
        private BodyImageStream BodyImageStream { get; set; }
        private ColorImageStream ColorImageStream { get; set; }
        public Canvas Canva => BodyImageStream.Canva;
        public ImageSource Source => ColorImageStream.Source;

        public BodyAndColorImageStream(KinectManager manager) : base(manager)
        {
            BodyImageStream = new BodyImageStream(manager);
            ColorImageStream = new ColorImageStream(manager);
        }
    }
}

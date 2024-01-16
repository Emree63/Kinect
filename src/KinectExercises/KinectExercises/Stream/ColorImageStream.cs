using Microsoft.Kinect;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Model.Stream
{
    public class ColorImageStream : KinectStream
    {

        public ColorFrameReader colorFrameReader;

        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.Bitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.Bitmap.PixelWidth) && (colorFrameDescription.Height == this.Bitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.Bitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra
                            );

                            this.Bitmap.AddDirtyRect(new Int32Rect(0, 0, this.Bitmap.PixelWidth, this.Bitmap.PixelHeight));
                        }

                        this.Bitmap.Unlock();
                    }
                }
            }
        }

        public ColorImageStream(KinectManager manager, WriteableBitmap bitmap): base(manager, bitmap)
        {
            this.colorFrameReader = this.Manager.Sensor.ColorFrameSource.OpenReader();
            this.colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;

        }
    }
}

using Microsoft.Kinect;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Model.Stream
{
    public class DepthImageStream : KinectStream
    {

        public DepthFrameReader depthFrameReader;
        private byte[] depthPixels = null;
        private const int MapDepthToByte = 8000 / 256;
        private void Reader_DepthFrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame != null)
                {
                    FrameDescription depthFrameDescription = depthFrame.FrameDescription;

                    using (KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                    {
                        this.Bitmap.Lock();
                        // verify data and write the new depth frame data to the display bitmap
                        if (depthFrameDescription.Width == this.Bitmap.PixelWidth && depthFrameDescription.Height == this.Bitmap.PixelHeight){
                            var depthData = new byte[depthFrameDescription.LengthInPixels * depthFrameDescription.BytesPerPixel];

                            depthFrame.CopyFrameDataToIntPtr(
                                this.Bitmap.BackBuffer,
                                (uint)(depthData.Length)
                            );


                            this.Bitmap.AddDirtyRect(new Int32Rect(0, 0, this.Bitmap.PixelWidth, this.Bitmap.PixelHeight));
                        }

                        this.Bitmap.Unlock();
                    }
                }
            }
        }


        public DepthImageStream(KinectManager manager, WriteableBitmap bitmap): base(manager, bitmap)
        {
            this.depthFrameReader = this.Manager.Sensor.DepthFrameSource.OpenReader();
            this.depthFrameReader.FrameArrived += this.Reader_DepthFrameArrived;

        }
    }
}

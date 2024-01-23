using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Model.Stream
{
    public class DepthImageStream : KinectStream
    {

        private FrameDescription depthFrameDescription = null;
        private byte[] depthPixels = null;
        private const int MapDepthToByte = 8000 / 256;
        private WriteableBitmap Bitmap;

        override public ImageSource Source => Bitmap;

        public DepthFrameReader depthFrameReader;
        private void Reader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            bool depthFrameProcessed = false;

            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame != null)
                {
                    // the fastest way to process the body index data is to directly access 
                    // the underlying buffer
                    using (Microsoft.Kinect.KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                    {
                        // verify data and write the color data to the display bitmap
                        if (((this.depthFrameDescription.Width * this.depthFrameDescription.Height) == (depthBuffer.Size / this.depthFrameDescription.BytesPerPixel)) &&
                            (this.depthFrameDescription.Width == this.Bitmap.PixelWidth) && (this.depthFrameDescription.Height == this.Bitmap.PixelHeight))
                        {
                            // Note: In order to see the full range of depth (including the less reliable far field depth)
                            // we are setting maxDepth to the extreme potential depth threshold
                            ushort maxDepth = ushort.MaxValue;

                            // If you wish to filter by reliable depth distance, uncomment the following line:
                            //// maxDepth = depthFrame.DepthMaxReliableDistance

                            this.ProcessDepthFrameData(depthBuffer.UnderlyingBuffer, depthBuffer.Size, depthFrame.DepthMinReliableDistance, maxDepth);
                            depthFrameProcessed = true;
                        }
                    }
                }
            }

            if (depthFrameProcessed)
            {
                this.RenderDepthPixels();
            }
        }

        private unsafe void ProcessDepthFrameData(IntPtr depthFrameData, uint depthFrameDataSize, ushort minDepth, ushort maxDepth)
        {
            // depth frame data is a 16 bit value
            ushort* frameData = (ushort*)depthFrameData;

            // convert depth to a visual representation
            for (int i = 0; i < (int)(depthFrameDataSize / this.depthFrameDescription.BytesPerPixel); ++i)
            {
                // Get the depth for this pixel
                ushort depth = frameData[i];

                // To convert to a byte, we're mapping the depth value to the byte range.
                // Values outside the reliable depth range are mapped to 0 (black).
                this.depthPixels[i] = (byte)(depth >= minDepth && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
            }
        }

        private void RenderDepthPixels()
        {
            this.Bitmap.WritePixels(
                new Int32Rect(0, 0, this.Bitmap.PixelWidth, this.Bitmap.PixelHeight),
                this.depthPixels,
                this.Bitmap.PixelWidth,
                0);
        }

        public DepthImageStream(KinectManager manager): base(manager)
        {
            FrameDescription depthFrameDescription = this.Manager.Sensor.DepthFrameSource.FrameDescription;
            Bitmap = new WriteableBitmap(depthFrameDescription.Width, depthFrameDescription.Height, 96, 96, PixelFormats.Gray8, null);
            this.depthFrameReader = this.Manager.Sensor.DepthFrameSource.OpenReader();
            this.depthFrameDescription = this.Manager.Sensor.DepthFrameSource.FrameDescription;
            this.depthFrameReader.FrameArrived += this.Reader_FrameArrived;
            this.depthPixels = new byte[this.depthFrameDescription.Width * this.depthFrameDescription.Height];

        }
    }
}

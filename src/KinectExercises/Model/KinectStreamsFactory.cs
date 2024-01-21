using Model;
using Model.Stream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KinectExercises.Stream
{
    public class KinectStreamsFactory
    {
        private readonly Dictionary<StreamType, Func<KinectManager, KinectStream>> streamFactory;

        public KinectStreamsFactory(KinectManager kinectManager, WriteableBitmap bitmap)
        {
            streamFactory = new Dictionary<StreamType, Func<KinectManager, KinectStream>>
            {
                { StreamType.None, _ => null },
                { StreamType.ColorStream, manager => new ColorImageStream(manager, bitmap) },
                { StreamType.DepthStream, manager => new DepthImageStream(manager, bitmap) },
                { StreamType.InfraredStream, manager => new InfraredImageStream(manager, bitmap) }
            };

            KinectManager = kinectManager;
        }

        public KinectManager KinectManager { get; }

        public KinectStream this[StreamType stream]
        {
            get => streamFactory[stream](KinectManager);
        }
    }
}

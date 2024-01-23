using Model;
using Model.Stream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectExercises.Stream
{
    public class KinectStreamFactory
    {
        private readonly Dictionary<StreamType, Func<KinectManager, KinectStream>> streamFactory;

        public KinectStreamFactory(KinectManager kinectManager)
        {
            streamFactory = new Dictionary<StreamType, Func<KinectManager, KinectStream>>
            {
                { StreamType.None, _ => null },
                { StreamType.ColorStream, manager => new ColorImageStream(manager) },
                { StreamType.DepthStream, manager => new DepthImageStream(manager) },
                { StreamType.InfraredStream, manager => new InfraredImageStream(manager) },
                { StreamType.BodyStream, manager => new BodyImageStream(manager) }
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

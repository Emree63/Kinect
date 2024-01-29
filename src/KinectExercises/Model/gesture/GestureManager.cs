using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public static class GestureManager
    {

        private static readonly List<BaseGesture> _KnownGestures = new();

        public static ReadOnlyCollection<BaseGesture> KnownGestures = new(_KnownGestures);
        public static KinectManager KinectManager { get; set; }

        public static event EventHandler<GestureRecognizedEventArgs> GestureRecognized;
        public static void OnGestureRecognized()
        {
            EventHandler<GestureRecognizedEventArgs> handler = GestureRecognized;
            if (null != handler) handler(typeof(GestureManager), new());
        }

        public static void AddGestures(params BaseGesture[] gestures)
            => _KnownGestures.AddRange(gestures);


        public static void RemoveGesture(BaseGesture gesture)
            => _KnownGestures.Remove(gesture);
    }
}

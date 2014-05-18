using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using AForge.Video.DirectShow;

namespace CameraAPI
{
    public class Camera : VideoDeviceInterface
    {
        private string CameraName;
        private VideoCaptureDevice Camera;
        private Bitmap LastFrame;

        public event EventHandler NewFrame;

        public Camera(string CameraName, bool AutoStart = true)
        {
            if (!WebcamExists(CameraName))
            {
                throw new InvalidWebcamException(CameraName + " is not a valid Camera");
            }

            FilterInfoCollection WebcamList = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Webcam in WebcamList)
            {
                if (Webcam.Name == CameraName)
                {
                    this.CameraName = CameraName;
                    this.Camera = new VideoCaptureDevice(Webcam.MonikerString);
                    this.Camera.NewFrame += Webcam_NewFrame;
                    if (AutoStart)
                    {
                        this.Camera.Start();
                    }
                }
            }
        }

        void Webcam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (LastFrame != null)
            {
                LastFrame.Dispose();
            }

            LastFrame = eventArgs.Frame;

            if (NewFrame != null)
            {
                NewFrame(this, new NewFrameEvent(eventArgs.Frame));
            }
        }

        public Image getLastFrame()
        {
            return LastFrame;
        }

        public string getCameraName()
        {
            return CameraName;
        }


        // Static Members


        static public bool WebcamExists(string CameraName)
        {
            FilterInfoCollection WebcamList = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo Webcam in WebcamList)
            {
                if (Webcam.Name == CameraName)
                {
                    return true;
                }
            }

            return false;
        }

        static public List<string> WebcamList()
        {
            FilterInfoCollection WebcamList = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            List<string> result = new List<string>(WebcamList.Count);
            foreach (FilterInfo Webcam in WebcamList)
            {
                result.Add(Webcam.Name);
            }
            return result;
        }
    }
}

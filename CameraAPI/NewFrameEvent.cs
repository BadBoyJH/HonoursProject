using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraAPI
{
    public class NewFrameEvent : EventArgs
    {
        private DateTime _TimeThrown;
        private Image _NewFrame;

        private DateTime TimeThrown
        {
            get
            {
                return _TimeThrown;
            }
        }

        public Image NewFrame
        {
            get
            {
                return _NewFrame;
            }
        }

        internal NewFrameEvent(Image Frame)
        {
            _TimeThrown = DateTime.Now;
            _NewFrame = Frame;
        }
    }
}

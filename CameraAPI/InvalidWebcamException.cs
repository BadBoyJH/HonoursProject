using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraAPI
{
    class InvalidWebcamException : Exception
    {
        public InvalidWebcamException()
        {
        }

        public InvalidWebcamException(string Message)
            : base(Message)
        {
        }

        public InvalidWebcamException(string Message, Exception Inner)
            : base(Message, Inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class DataHandler
    {

        public DataHandler(string portName)
        {
            double[] massValues;
            SerialHandler SerialPort = new SerialHandler(portName);
        }
        
    }
}

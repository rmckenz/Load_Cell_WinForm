using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace DemoApp
{
    public class SerialHandler
    {
        private SerialPort _serialPort;
        public SerialHandler(string portName)
        {
            _serialPort = new SerialPort(portName);
        }
       
        public void Open()
        {

        }

        public void Close()
        {

        }


        public int poke()
        {

            return 1;
        }

        public void Read()
        {

        }

    }
}

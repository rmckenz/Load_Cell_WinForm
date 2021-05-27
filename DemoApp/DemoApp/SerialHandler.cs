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
        private SerialPort _serialPort; // Initializing an unspecified serial port called _serialPort

        public SerialHandler(string portName) //Constructor Class
        {
            _serialPort = new SerialPort(portName); //constructing the new serial port will take the string from the cbo box in application
        }
       
        public void sOpen(string selectedPort)
        {
            if (_serialPort.IsOpen)  //If the serial port is already open close it
            {
                _serialPort.Close();
            }

            if (string.IsNullOrEmpty(selectedPort))
            {
                Console.WriteLine("Select a port first");
                return;
            }

            _serialPort.Open();
        }

        public bool isOpen()
        {
            return _serialPort.IsOpen;
        }

        public void sClose()
        {
            if (!_serialPort.IsOpen)
            {
                return;
            }

            _serialPort.Close();

        }


        public int sPoke() //Sending info to microcontroller to signal it to take reading
        {

            return 1;
        }

        public void sRead() //Reading incoming data from microntroller
        {

        }

    }
}

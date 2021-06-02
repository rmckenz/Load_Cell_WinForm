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
        private string poke = "0";
        public bool readReady { get; set; }


        public SerialHandler(string portName) //Constructor Class
        {
            _serialPort = new SerialPort(portName); //constructing the new serial port will take the string from the cbo box in application
            _serialPort.DataReceived += _serialPort_DataReceived;
            readReady = false;
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            readReady = true;
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
            /*
            if (!(_serialPort.IsOpen))
            {
                return;
            } */

            _serialPort.Close();

        }


        public void sPoke() //Sending info to microcontroller to signal it to take reading
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
            _serialPort.WriteLine(this.poke);

            /*
            int sendSignal = 1;
            byte[] b = BitConverter.GetBytes(sendSignal);
            _serialPort.Write(b, 0, 4);
            */

        }

        public string sRead() //Reading incoming data from microntroller
        {
            string recieveData;
            recieveData = _serialPort.ReadLine();
          //  Console.WriteLine(recieveData);
            readReady = false;
            return recieveData;
        }

    }
}

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
            _serialPort.DataReceived += _serialPort_DataReceived; //Creating an event handler for when data is recieved through the selected serial port
            readReady = false; //Initializes it so that is is not ready to read - make sense we haven't poked
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            readReady = true; //If data is in the buffer on the serial port, the device is now ready to read the info
        }

        public void sOpen()
        {
            _serialPort.Open();
        }

        public bool isOpen()
        {
            return _serialPort.IsOpen;
        }

        public void sClose()
        {
            _serialPort.Close();
        }


        public void sPoke() //Sending info to microcontroller to signal it to take reading
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
            _serialPort.WriteLine(this.poke); //Sends "0" over the serial port

        }

        public string sRead() //Reading incoming data from microntroller
        {
            string recieveData;
            recieveData = _serialPort.ReadLine();
            readReady = false;
            return recieveData;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        Timer t;
        SerialHandler _serialCommunicator;

        public Form1()
        {
            InitializeComponent();
            t = new Timer();
            t.Interval = 100;
            t.Tick += t_Tick;
            ///string selectedPort = cboPort.SelectedItem.ToString();
        }

        private void t_Tick(object sender, EventArgs e)
        {
 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get all ports
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            //cboPort.SelectedIndex = 0;

            btnClose.Enabled = false; //Can't select the close button if open hasn't been clicked
            btnStop.Enabled = false; //Can't select the sto button if the start button hasn't been clickd
        }

        private void btnOpen_Click(object sender, EventArgs e) //Opens the serial port
        {
            btnClose.Enabled = true;
            btnOpen.Enabled = false;

            //Grabbing the selected port name and initializing the serial port (kinda unsure where to place this)
            string selectedPort = cboPort.SelectedItem.ToString(); //Grabbing the s=selected port from cbo box
            _serialCommunicator = new SerialHandler(selectedPort);
            _serialCommunicator.sOpen(selectedPort);
        }

        private void btnStart_Click(object sender, EventArgs e) //Starts reading from serial port
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            //Should poke then read from the microcontroller (maybe within the timer?)
            t.Start();
        }

        private void btnStop_Click(object sender, EventArgs e) //Stops reading from the serial port
        {
            t.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e) //Closes the serial port
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            _serialCommunicator.sClose();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialCommunicator.isOpen())
            {
                _serialCommunicator.sClose();
            }
        }

    }
}

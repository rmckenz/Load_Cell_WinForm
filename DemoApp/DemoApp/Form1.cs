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
using System.Threading;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer t;
        //ClassA a;
        SerialHandler _serialCommunicator;
        string selectedPort;



        public Form1() //Constructor method
        {
            InitializeComponent();
            t = new System.Windows.Forms.Timer();
            t.Interval = 1000;  // (millis) The frequency at which the timer clicks on and off
            t.Tick += t_Tick;
            //string selectedPort = cboPort.SelectedItem.ToString();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            _serialCommunicator.sPoke();
            while (!_serialCommunicator.readReady)
            {
                Thread.Sleep(1); //If you don't recieve data this will infintely run
            }
            txtReceive.Text += _serialCommunicator.sRead();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cboPort.SelectedIndex = 0; //Throws and error if there are no serial ports connect to computer

            btnClose.Enabled = false; //Can't select the close button if open hasn't been clicked
            btnStop.Enabled = false; //Can't select the sto button if the start button hasn't been clickd
            btnStart.Enabled = false;
            btnStop.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e) //Opens the serial port
        {
            if(selectedPort == null)
            {
                Form2 popup = new Form2();
                DialogResult dResult = popup.ShowDialog();
                if (dResult == DialogResult.OK)
                {
                    Console.WriteLine("You clicked OK");
                    popup.Dispose();
                }
                return;
            }
            /*
            try
            {
                
                _serialCommunicator = new SerialHandler(selectedPort);
                _serialCommunicator.sOpen(selectedPort);
                btnClose.Enabled = true;
                btnOpen.Enabled = false;
                btnStart.Enabled = true;
            }
            catch (Exception ex)
            {
                //Create popup window which yells at the user to select a port or connect a device
                Console.WriteLine("Im in the catch");
                Form2 popup = new Form2();
                DialogResult dResult = popup.ShowDialog();
                if(dResult == DialogResult.OK)
                {
                    Console.WriteLine("You clicked OK");
                    popup.Dispose();
                }
            } */

            btnClose.Enabled = true;
            btnOpen.Enabled = false;
            btnStart.Enabled = true;
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
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            t.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e) //Closes the serial port
        {
            if (btnStop.Enabled)
            {
                t.Stop();
            }
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            btnStop.Enabled = false;
            btnStart.Enabled = false;
            _serialCommunicator.sClose();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialCommunicator.isOpen())
            {
                _serialCommunicator.sClose();
            }
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.Text.Length; //Set the current caret position to end
            txtReceive.ScrollToCaret(); //Scroll automatically
        }

        private void cboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPort = cboPort.SelectedItem.ToString(); //Grabbing the selected port from cbo box if cbo box is not slected, this will be null
        }

        private void cboPort_MouseClick(object sender, MouseEventArgs e)
        {
            //Get all ports
            cboPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            

        }


        /*
       private void button1_Click(object sender, EventArgs e)
       {
           a = new ClassA();
       }

       private void button2_Click(object sender, EventArgs e)
       {
           a.Stop();
       }
        */
    }
}

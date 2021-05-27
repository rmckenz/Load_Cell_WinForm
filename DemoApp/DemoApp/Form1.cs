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
        private SerialHandler _serialCommunicator;

        public Form1()
        {
            InitializeComponent();
            t = new Timer();
            t.Interval = 100;
            t.Tick += t_Tick;
            double a;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                //Read text from port
                txtReceive.Text += serialPort1.ReadExisting().Trim();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get all ports
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            cboPort.SelectedIndex = 0;
            btnClose.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(_serialCommunicator != null)
            {
                _serialCommunicator.Close();
            }

            string selectedPort = cboPort.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selectedPort))
            {
                Console.WriteLine("Select a port first");
                return;
            }

            _serialCommunicator = new SerialHandler(selectedPort);

            _serialCommunicator.Open();

            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try
            {
                //Open port
                serialPort1.PortName = cboPort.Text;
                serialPort1.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            t.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            t.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

        private void cboPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

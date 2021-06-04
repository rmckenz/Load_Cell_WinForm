

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    /*Class ClassA
    {
        private ClassB secondaryClass; //represents serial handler
        public List<int> Values;
        public bool flag;
        private Thread readThread; //defining a thread called readThread
        public ClassA()
        {
            secondaryClass = new ClassB();
            flag = true;

            readThread = new Thread(new ThreadStart(Read)); //instantiating readThread
            readThread.Name = "Read Thread";
            readThread.IsBackground = true; //this runs in the background (form interaction in the foreground)
            readThread.Start(); //Starts the new thread when and object of class a is constructed
            
        }

        public void Read()
        {
            Console.WriteLine("Open serial port");
            //open serial port
            while (flag)
            {
                secondaryClass.print();
                Console.WriteLine("Reading from secondary thread");
                Thread.Sleep(1000); //This sleep functions as the timer and is what grabs a data point ever 1000millis/1s
            }
            //serial port close
            Console.WriteLine("Close serial port");

        }

        public void Stop()
        {
            flag = false;
        }

        public void ClosePort()
        {
            //_serialPort.Close();
        }
    } */
}
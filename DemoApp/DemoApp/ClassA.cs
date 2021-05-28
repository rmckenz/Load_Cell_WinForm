﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    class ClassA
    {
        private ClassB secondaryClass; //represents serial handler
        public List<int> Values;
        public bool flag;
        private Thread readThread;
        public ClassA()
        {
            secondaryClass = new ClassB();
            flag = true;

            readThread = new Thread(new ThreadStart(Read));
            readThread.Name = "Read Thread";
            readThread.IsBackground = true;
            readThread.Start();
            
        }

        public void Read()
        {
            Console.WriteLine("Open serial port");
            //open serial port
            while (flag)
            {
                secondaryClass.print();
                Console.WriteLine("Reading from secondary thread");
                Thread.Sleep(1000);
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
    }
}
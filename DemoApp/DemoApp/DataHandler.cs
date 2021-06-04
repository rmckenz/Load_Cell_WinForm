using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DemoApp
{
    class DataHandler
    {
        SerialHandler SerialPort;
        private Thread readThread; //Initializing a new thread - will be used to poke, read, and update the moving Array (timer as well?)
        private string portName;
        private bool flag;
        private double rawValues; //will be used to store the current read value
        private double[] valueArray;
        Queue<double> queue;

        public DataHandler(string portName) //Constructor - initializing class variables
        {
            
            readThread = new Thread(new ThreadStart(readData));
            readThread.Name = "Read Thread";
            readThread.IsBackground = true; //this runs in the background (form interaction in the foreground)

            this.portName = portName;
            SerialPort = new SerialHandler(portName); //port name will get passed in via selected cbo index on form
            flag = true;
        }

        public double[] movingArray(double values)
        {
            //TODO creates a FIFO queue that stores only the X most recent values within the array
            queue = new Queue<double>();
            queue.Enqueue(values);
            while(queue.Count() > 10) //10 is arbitrary - whatever size you want the moving array
            {
                queue.Dequeue();
            }
            double[] newDouble = queue.ToArray();
            return newDouble;
        }
        
        public void storageArray(double[] movingArray)
        {
            //TODO uses slope or moving average of the movingArray to detect if the difference is significant enough to store mass value
            // Once a significant jump has been detected, it evaluates the new value and stores in the array
            double sum = 0.0;
            for(int i = 0; i < movingArray.Length - 1; i++)
            {
                sum += movingArray[i]; //taking the sum for everything but the last element
            }
            double mAverage = sum / (movingArray.Length - 1); //Taking the moving average for all but the last element in the array
            if(Math.Abs(mAverage - rawValues) > 2) //TOSET th difference to trigger an append to the array is arbritray and should be reevaluated when load cell is observed
            {
                valueArray.Append(rawValues);
            }
        }

        public double processData(string serialOutput)
        {
            //A method to proceess the data - arbritray here but wil become very improtant to proceess load cell data
            double value = Convert.ToDouble(serialOutput);
            var rand = new Random();
            value = value / 2.0 + rand.NextDouble();
            return value;

        }

        public void readData()
        {
            //TODO: a method that starts the data collection process
            // May implement the sPoke, sRead methods of the SerialHandler class
            //Should run the data collection on a seperate thread - refer to ClassA for example

            //Open the serial port
            if (!SerialPort.isOpen()) //If the serial port is not open, open the serial port
            {
                SerialPort.sOpen();
            }

            readThread.Start();

            while (flag)
            {
                SerialPort.sPoke();
                while (!SerialPort.readReady)
                {
                    Thread.Sleep(1); //Waits for data to be returned from poke before reading
                }
                rawValues = processData(SerialPort.sRead()); //Converting most recent reading to double and storing it as rawValues
                double[] mArray = movingArray(rawValues); //Adds rawValues to the queue array and removes values to keep it at a length of X
                storageArray(mArray); //Using the void method to update valueArray
                Thread.Sleep(1000); //This sleep functions as the timer and is what grabs a data point ever 1000millis/1s
            }
            //serial port close
            readThread.Abort();
            SerialPort.sClose();

        }

        public void stopData()
        {
            //Should stop the data collection thread and stop the poke and recieve functionalality
            flag = false;
        }

        public void startData()
        {
            //Should stop the data collection thread and stop the poke and recieve functionalality
            flag = false;
        }
    }
}

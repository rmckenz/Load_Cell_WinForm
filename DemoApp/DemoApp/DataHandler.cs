using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class DataHandler
    {
        SerialHandler SerialPort;
        public DataHandler(string portName) //Constructor - initializing class variables
        {
            double rawValues;
            SerialPort = new SerialHandler(portName); //port name will get passed in via selected cbo index on form
        }

        public double[] movingArray()
        {
            //TODO creates a FIFO queue that stores only the X most recent values within the array
            double[] newDouble = { 1.1, 2.2, 3.3 };
            return newDouble;
        }
        
        public double[] storageArray(double[] movingArray)
        {   
            //TODO uses slope or moving average of the movingArray to detect if the difference is significant enough to store mass value
            // Once a significant jump has been detected, it evaluates the new value and stores in the array
            double[] newDouble = { 1.1, 2.2, 3.3 };
            return newDouble;
        }

        public double processData(string serialOutput)
        {
            //TODO grabs the recieve data and process it to the meaningful data type (i.e. weight, distance, etc)
            //A helper method
            return 1.23;
        }

        public void startData()
        {
            //TODO: a method that starts the data collection process
            // May implement the sPoke, sRead methods of the SerialHandler class
            //Should run the data collection on a seperate thread - refer to ClassA for example
        }

        public void stopData()
        {
            //Should stop the data collection thread and stop the poke and recieve functionalality
        }

        public void serialOpen(string portname)
        {

            //Utilizes SerialPort.sClose() or another method to close the serial COM port 
            SerialPort.sOpen(portname);
         }

        public void serialClose()
        {
            //Utilizes SerialPort.sClose() or antoher method to close the serial COM port
            SerialPort.sClose();
        }
    }
}

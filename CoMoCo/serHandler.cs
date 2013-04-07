using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo
{
    public class serHandler
    {
        Thread _Thread;

        private Queue<string> _SendQueue = new Queue<string>();
        private Queue<char> _ReceieveQueue = new Queue<char>();

        private bool _SerialOpen = false;
        public bool serOpen { get { return _SerialOpen; } }
        private int _SerialNumber = 0;

        private SerialPort _SerialPort = null;

        int _BaudRate = 9600;

        public serHandler()
        {
            _Thread = new Thread(new ThreadStart(_ThreadAction));
            _Thread.Start();
        }

        public void Dispose()
        {
            if (_SerialPort.IsOpen)
                _SerialPort.Close();
        }

        public void AddToSendQueue(string value)
        {
            Monitor.Enter(_SendQueue);
            try
            {
                 _SendQueue.Enqueue(value);
            }
            finally
            {
                Monitor.Exit(_SendQueue);
            }
        }

        public void _ThreadAction()
        {
            connect();
            string toSend = string.Empty;
            while (true)
            {
                // Send waiting message
                var send = false;
                if (_SendQueue.Count > 0)
                {
                    Monitor.Enter(_SendQueue);
                    try
                    {
                        toSend = _SendQueue.Dequeue();
                    }
                    finally
                    {
                        Monitor.Exit(_SendQueue);
                        send = true;
                    }
                }
                else
                {
                    Thread.Sleep(100); // Keeps infinite while loop from killing processor
                }

                if (send)
                {
                    var sendTime = DateTime.Now; // - startTime;
                    // serialSends.append([float(sendTime),str(toSend)])
                    Thread.Sleep(3);
                    if (_SerialOpen)
                    {
                        if (_SerialPort.IsOpen)
                        {
                            _SerialPort.Write(toSend);                            
                            Console.WriteLine("sent {0} to COM{1}", 
                                toSend.ToString().Replace("\r", "" ), _SerialNumber + 1);
                        }
                    }
                }

                // Receive code goes here
            }
        }

        public void connect()
        {
            var comPorts = SerialPort.GetPortNames();
            Console.WriteLine("Attempting to connect to Servotor");
            foreach (var comPort in comPorts)
            {
                try
                {
                    SerialPort serialPort;
                    try
                    {
                        serialPort = new SerialPort(comPort, _BaudRate);
                        serialPort.WriteTimeout = 2000;
                        serialPort.Open();
                    }
                    catch
                    {
                        throw;
                    }
                    serialPort.Write("V\n");
                    var result = serialPort.ReadLine();
                    if (result.Contains("SERVOTOR"))
                    {
                        Console.WriteLine("Connect Successful! Connected on port " + comPort);
                        _SerialPort = serialPort;
                        _SerialPort.DiscardInBuffer();
                        _SerialPort.DiscardOutBuffer();
                        _SerialOpen = true;
                        _SerialNumber = 1;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            if (!_SerialOpen)
            {
                Console.WriteLine("Trying Windows Method");
                for (int i = 0; i < 100; i++)
                {
                    SerialPort serialPort;
                    try
                    {
                        try
                        {
                            serialPort = new SerialPort("COM" + i.ToString(), _BaudRate);
                            serialPort.WriteTimeout = 1000;
                            serialPort.Open();
                        }
                        catch
                        {
                            throw;
                        }
                        // I think thjis is the equivilent of .flush
                        serialPort.DiscardInBuffer();
                        serialPort.DiscardOutBuffer();

                        Thread.Sleep(100);
                        _SerialPort.Write("V\n");
                        Thread.Sleep(5000);
                        var readReply = _SerialPort.ReadLine();
                        Console.WriteLine("Read: " + readReply);
                        if (readReply.Contains("SERVOTOR"))
                        {
                            Console.WriteLine("Connect Successful! Connected on port");
                            serialPort.DiscardInBuffer();
                            serialPort.DiscardOutBuffer();
                            _SerialPort = serialPort;
                            _SerialNumber = i;
                            _SerialOpen = true;
                            break;
                        }
                        else
                        {
                            serialPort.Close();
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
}

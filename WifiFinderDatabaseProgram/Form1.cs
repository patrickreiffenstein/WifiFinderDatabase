using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WifiFinderDatabaseProgram
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;

        public Form1()
        {
            InitializeComponent();
            buttonRefreshComPorts_Click(null, null);
        }

        private void buttonRefreshComPorts_Click(object sender, EventArgs e)
        {
            comboBoxComPort1.Items.Clear();
            comboBoxComPort1.Items.AddRange(SerialPort.GetPortNames());

            comboBoxComPort2.Items.Clear();
            comboBoxComPort2.Items.AddRange(SerialPort.GetPortNames());

            comboBoxComPort3.Items.Clear();
            comboBoxComPort3.Items.AddRange(SerialPort.GetPortNames());
        }

        private void comboBoxComPort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.Close();

            if (comboBoxComPort1.SelectedItem is null)
            {
                return;
            }

            serialPort1.PortName = (string)comboBoxComPort1.SelectedItem;
            serialPort1.Open();
        }

        private void comboBoxComPort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort2.Close();

            if (comboBoxComPort1.SelectedItem is null)
            {
                return;
            }

            serialPort2.PortName = (string)comboBoxComPort2.SelectedItem;
            serialPort2.Open();
        }

        private void comboBoxComPort3_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort3.Close();

            if (comboBoxComPort1.SelectedItem is null)
            {
                return;
            }

            serialPort3.PortName = (string)comboBoxComPort3.SelectedItem;
            serialPort3.Open();
        }

        private void buttonStartPulling_Click(object sender, EventArgs e)
        {
            timerSerialPortPull.Enabled = true;
            labelPullStatus.Text = "Samler...";
        }

        private void buttonStopPulling_Click(object sender, EventArgs e)
        {
            timerSerialPortPull.Enabled = false;
            labelPullStatus.Text = "Samler ikke.";
        }

        private void timerSerialPortPull_Tick(object sender, EventArgs e)
        {
            List<SerialPort> serialPorts = new List<SerialPort>()
            {
                serialPort1,
                serialPort2,
                serialPort3
            };

            WifiFinderSystem.WifiFinderSystem.RefreshData(5);

            foreach (var item in serialPorts)
            {
                if (!item.IsOpen)
                {
                    continue;
                }

                StringBuilder sb = new StringBuilder();

                byte deviceID = byte.Parse(item.PortName.Replace("COM", ""));
                
                while (item.BytesToRead > 0)
                {
                    string readData = item.ReadLine();

                    try
                    {
                        string[] inputSubs = readData.Split(',');

                        long macAddress = Convert.ToInt64(inputSubs[0].Replace(":", ""), 16);
                        byte RSSi = (byte)Math.Abs(int.Parse(inputSubs[1]));

                        WifiFinderSystem.WifiFinderSystem.AddData(macAddress, deviceID, RSSi);
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine("Exception: " + ex);
                    }

                    sb.Append($"{item.PortName};{readData}\n");
                }

                MethodInvoker textChangeDelegate = delegate ()
                {
                    richTextBoxDataPulled.Text += sb.ToString();
                };

                if (richTextBoxDataPulled.InvokeRequired)
                {
                    richTextBoxDataPulled.Invoke(textChangeDelegate);
                }
                else
                {
                    textChangeDelegate();
                }
            }

            labelDataCount.Text = WifiFinderSystem.WifiFinderSystem.CountDataEntries().ToString();

            NetworkLoop();
        }

        private List<TcpClient> connectedClients = new List<TcpClient>();

        private void NetworkLoop()
        {
            if (tcpListener is null)
            {
                return;
            }

            if (tcpListener.Pending())
            {
                connectedClients.Add(tcpListener.AcceptTcpClient());
            }

            byte[] data = null;

            foreach (var item in connectedClients)
            {
                if (!item.Connected)
                {
                    continue;
                }

                if (item.Available <= 0)
                {
                    continue;
                }

                NetworkStream stream = item.GetStream();

                if (stream.ReadByte() == 1)
                {
                    if (data is null)
                    {
                        data = Encoding.ASCII.GetBytes(WifiFinderSystem.WifiFinderSystem.PrepareSerializedData());
                    }

                    byte[] lengthBytes = BitConverter.GetBytes((uint)data.Length);

                    // skriv data længden.
                    stream.Write(lengthBytes, 0, lengthBytes.Length);

                    // skriv data til stream.
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBoxDataPulled.Text = string.Empty;
        }

        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            tcpListener?.Stop();
            tcpListener = new TcpListener(System.Net.IPAddress.Any, 20_000);
            tcpListener.Start();
        }
    }
}

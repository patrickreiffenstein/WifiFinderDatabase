using System;
using System.Windows.Forms;
using static WifiFinderSystem.WifiFinderSystem;

namespace WifiFinderDemo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			serialPort.DataReceived += SerialPort_DataReceived;
		}

		private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			try
			{
				string input = serialPort.ReadLine();
				string[] inputSubs = input.Split(',');

				long macAddress = Convert.ToInt64(inputSubs[0].Replace(":", ""), 16);
				byte RSSi = (byte)Math.Abs(int.Parse(inputSubs[1]));

				// TODO: fiks at den har '3' som ID i stedet for den rent faktiske ID
				AddData(macAddress, 3, RSSi);


				this.Invoke((MethodInvoker)delegate
				{
					inputRichTextBox.AppendText(input);
				});
			}
			catch (Exception)
			{
				return;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!serialPort.IsOpen)
			{
				serialPort.Open();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (!serialPort.IsOpen)
			{
				return;
			}

			RefreshData(5);

			this.Invoke((MethodInvoker)delegate
			{
				dataAmountLabel.Text = CountDataEntries().ToString();
			});
		}
	}
}

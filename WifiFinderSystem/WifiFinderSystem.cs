using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiFinderSystem
{
    public static class WifiFinderSystem
    {
        public readonly struct Data
        {
            public readonly byte ID;
            public readonly DateTime Timestamp;
            public readonly byte RSSi;

            public Data(byte ID, DateTime Timestamp, byte RSSi)
            {
                this.ID = ID;
                this.Timestamp = Timestamp;
                this.RSSi = RSSi;
            }
        }

        static Dictionary<long, Queue<Data>> dataDictionary = new Dictionary<long, Queue<Data>>();

        public static void RefreshData(int maxAge)
        {
            DateTime nowOffset = DateTime.Now.Subtract(TimeSpan.FromSeconds(maxAge));

            foreach (KeyValuePair<long, Queue<Data>> singleDevice in dataDictionary)
            {
                long deviceAddress = singleDevice.Key;
                Queue<Data> devicePackets = singleDevice.Value;

                while (devicePackets.Count != 0 && devicePackets.Peek().Timestamp < nowOffset)
                {
                    devicePackets.Dequeue();
                }
            }
        }

        public static int CountDataEntries()
        {
            int num = 0;
            foreach (KeyValuePair<long, Queue<Data>> singleDevice in dataDictionary)
            {
                long deviceAddress = singleDevice.Key;
                Queue<Data> devicePackets = singleDevice.Value;

                num += devicePackets.Count;
            }
            return num;
        }

        private static Dictionary<long, Dictionary<byte, byte>> GetMedianRSSi()
        {
            Dictionary<long, Dictionary<byte, byte>> medianValues = new Dictionary<long, Dictionary<byte, byte>>();

            foreach (var macDevice in dataDictionary)
            {
                Dictionary<byte, List<byte>> temporaryValues = new Dictionary<byte, List<byte>>();
                foreach (Data data in macDevice.Value)
                {
                    if (!temporaryValues.TryGetValue(data.ID, out List<byte> rssiValues))
                    {
                        rssiValues = new List<byte>();
                        temporaryValues.Add(data.ID, rssiValues);
                    }

                    rssiValues.Add(data.RSSi);
                }

                medianValues[macDevice.Key] = new Dictionary<byte, byte>();

                foreach (var item in temporaryValues)
                {
                    List<byte> rssiValues = item.Value;
                    rssiValues.Sort();

                    byte median = rssiValues[rssiValues.Count / 2];

                    medianValues[macDevice.Key][item.Key] = median;
                }
            }

            return medianValues;
        }

        public static string PrepareSerializedData()
        {
            RefreshData(5);

            StringBuilder sb = new StringBuilder();

            // MAC;DEVICE1.rssi,DEVIC2.rssi,DEVICE3.rssi
            foreach (var item in GetMedianRSSi())
            {
                sb.Append(item.Key);
                sb.Append(';');

                foreach (var capturedByDevice in item.Value)
                {
                    sb.Append(capturedByDevice.Key);
                    sb.Append('.');
                    sb.Append(capturedByDevice.Value);
                    sb.Append(',');
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static void AddData(long MacAddress, byte ID, byte RSSi)
        {
            Queue<Data> dataList;

            if (!dataDictionary.TryGetValue(MacAddress, out dataList))
            {
                dataList = new Queue<Data>();
                dataDictionary[MacAddress] = dataList;
            }

            dataList.Enqueue(new Data(ID, DateTime.Now, RSSi));
        }
    }
}
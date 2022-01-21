using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiFinderSystem
{
    public class WifiFinderSystem
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

			foreach (KeyValuePair <long, Queue<Data>> singleDevice in dataDictionary)
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
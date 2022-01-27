using System;
using WifiFinderAlgorithm;

namespace WifiFinderPositionSystemTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var ting = new WifiFinderAlgorithm.WifiFinderAlgorithm();
            Coordinate ting2 = ting.FindUnit(ting.receivers);
            Console.Read();
            Console.WriteLine(ting2.x + " " + ting2.y);
            Console.Read();
        }
    }
}

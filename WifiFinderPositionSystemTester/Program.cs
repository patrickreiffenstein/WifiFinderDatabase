using System;

namespace WifiFinderPositionSystemTester
{
    class Program
    {
        static void Main(string[] args)
        {
            WifiFinderPositionSystem.WifiFinderPositionSystem ting = new WifiFinderPositionSystem.WifiFinderPositionSystem();
            WifiFinderPositionSystem.Coordinate tin = ting.FindUnit(ting.receivers);
            Console.WriteLine(tin.x + " " + tin.y);
            Console.Read();
        }
    }
}

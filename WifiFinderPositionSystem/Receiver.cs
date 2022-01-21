namespace WifiFinderPositionSystem
{
    public class Receiver
    {
        public Coordinate coordinate;
        public double signalStrength;

        public Receiver(Coordinate Coord, byte SignalStrength)
        {
            coordinate = Coord;
            signalStrength = SignalStrength;
        }
    }
}
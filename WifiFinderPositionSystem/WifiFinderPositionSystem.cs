using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiFinderPositionSystem
{
	public class WifiFinderPositionSystem
	{
		public Receiver[] receivers = new Receiver[3]
		{
			new Receiver(new Coordinate(1, 1), 2),
			new Receiver(new Coordinate(1, 5), 3),
			new Receiver(new Coordinate(7, 1), 2)
		};

		public Coordinate FindUnit(Receiver[] receivers)
		{
			if (receivers.Length < 3)
			{
				return null;
			}


			double distBetweenReceivers0_1, distBetweenReceivers1_2, distBetweenReceivers2_0;
			bool found = false;
			float scale = 1;

			// Gør den størrer hvis den er for lille
			distBetweenReceivers0_1 = Math.Sqrt(Math.Pow(receivers[0].coordinate.x + receivers[1].coordinate.x, 2) + Math.Pow(receivers[0].coordinate.y + receivers[1].coordinate.y, 2));
			distBetweenReceivers1_2 = Math.Sqrt(Math.Pow(receivers[1].coordinate.x + receivers[2].coordinate.x, 2) + Math.Pow(receivers[1].coordinate.y + receivers[2].coordinate.y, 2));
			distBetweenReceivers2_0 = Math.Sqrt(Math.Pow(receivers[2].coordinate.x + receivers[0].coordinate.x, 2) + Math.Pow(receivers[2].coordinate.y + receivers[0].coordinate.y, 2));


			float dx = receivers[0].coordinate.x - receivers[1].coordinate.x;
			float dy = receivers[0].coordinate.y - receivers[1].coordinate.y;

			double d = Math.Sqrt(dy * dy + dx * dx);


			do
			{
				if (d < receivers[0].signalStrength * scale + receivers[1].signalStrength * scale || d > Math.Abs(receivers[0].signalStrength * scale + receivers[1].signalStrength * scale))
				/*if (distBetweenReceivers0_1 < receivers[0].signalStrength * scale + receivers[1].signalStrength * scale
					&& distBetweenReceivers1_2 < receivers[1].signalStrength * scale + receivers[2].signalStrength * scale
					&& distBetweenReceivers2_0 < receivers[2].signalStrength * scale + receivers[0].signalStrength * scale)*/
				{
					found = true;
				}
				else
				{
					scale *= 1.1f;
				}
			} while (!found);

			//Skalere cirklens signaler
			foreach (var receiver in receivers)
			{
				receiver.signalStrength = (float)(receiver.signalStrength * scale);
				Console.WriteLine(receiver.signalStrength);
			}

			//Finder ud af om arealet er for stort

			// Prøver at skrive kode fra internetttet:
			// https://stackoverflow.com/questions/19723641/find-intersecting-point-of-three-circles-programmatically

			double a = ((receivers[0].signalStrength * receivers[0].signalStrength) - (receivers[1].signalStrength * receivers[1].signalStrength) + (d * d)) / (2 * d);

			double point2x = receivers[0].coordinate.x + (dx * a/d);
			double point2y = receivers[0].coordinate.y + (dy * a / d);

			double h = Math.Sqrt(receivers[0].signalStrength * receivers[0].signalStrength - a*a);

			double rx = -dy * (h / d);
			double ry = dx * (h / d);

			double intersectionsPoint1x = point2x + rx;
			double intersectionsPoint2x = point2x - rx;
			double intersectionsPoint1y = point2y + ry;
			double intersectionsPoint2y = point2y - ry;
			Console.WriteLine(scale);
			Console.WriteLine(intersectionsPoint2x + " " + intersectionsPoint2y);
			return new Coordinate((float)intersectionsPoint1x, (float)intersectionsPoint1y);
		}
	}
}
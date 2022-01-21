using System;
using System.Collections.Generic;

namespace WifiFinderPositionSystem
{
    public class WifiFinderPositionSystem
    {
        public Receiver[] receivers = new Receiver[3]
        {
            new Receiver(new Coordinate(1, 1), 2),
            new Receiver(new Coordinate(1, 5), 3),
            new Receiver(new Coordinate(7, 1), 2),
        };

        public Coordinate FindUnit(Receiver[] receivers)
        {
            if (receivers.Length < 3)
            {
                return null;
            }

            CalculateThreeCircleIntersection(new Circle(1, 1, 2), new Circle(1, 5, 3), new Circle(7, 1, 2));

            return null;

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

            double point2x = receivers[0].coordinate.x + (dx * a / d);
            double point2y = receivers[0].coordinate.y + (dy * a / d);

            double h = Math.Sqrt(receivers[0].signalStrength * receivers[0].signalStrength - a * a);

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

        private const double EPSILON = 0.001;

        private bool CalculateThreeCircleIntersection(
            Circle circle1Original,
            Circle circle2Original,
            Circle circle3Original,
            double scale=1)
        {
            // TRIN 1: Find den rigtige skalering, så alle cirkler mindst skærer hinanden parvis.
            // TRIN 2: Udregn alle skæringspunkt kombinationerne, der skulle gerne være 6 punkter.
            // TRIN 3: Find 3 tætteste punkter (dem som er tæt på midten).
            // TRIN 4: Find det centrum de 3 punkter udgør.


            double a, dx, dy, d, h;

            // scaled radii
            Circle c0, c1, c2;

            c0 = new Circle(circle1Original.X, circle1Original.Y, circle1Original.Radius * scale);
            c1 = new Circle(circle2Original.X, circle2Original.Y, circle2Original.Radius * scale);
            c2 = new Circle(circle3Original.X, circle3Original.Y, circle3Original.Radius * scale);

            // TRIN 1
            // C0 — C1
            // dx and dy are the vertical and horizontal distances between
            // the circle centers.
            dx = c1.X - c0.X;
            dy = c1.Y - c0.Y;

            // Determine the straight-line distance between the centers.
            d = Math.Sqrt((dy * dy) + (dx * dx));

            // Check for solvability.
            if (d > (c0.Radius + c1.Radius))
            {
                // no solution. circles do not intersect.
                return CalculateThreeCircleIntersection(circle1Original, circle2Original, circle3Original, scale + EPSILON);
            }

            // C1 — C2
            // dx and dy are the vertical and horizontal distances between
            // the circle centers.
            dx = c2.X - c1.X;
            dy = c2.Y - c1.Y;

            // Determine the straight-line distance between the centers.
            d = Math.Sqrt((dy * dy) + (dx * dx));

            // Check for solvability.
            if (d > (c1.Radius + c2.Radius))
            {
                // no solution. circles do not intersect.
                return CalculateThreeCircleIntersection(circle1Original, circle2Original, circle3Original, scale + EPSILON);
            }

            // C0 — C2
            // dx and dy are the vertical and horizontal distances between
            // the circle centers.
            dx = c2.X - c0.X;
            dy = c2.Y - c0.Y;

            // Determine the straight-line distance between the centers.
            d = Math.Sqrt((dy * dy) + (dx * dx));

            // Check for solvability.
            if (d > (c0.Radius + c2.Radius))
            {
                // no solution. circles do not intersect.
                return CalculateThreeCircleIntersection(circle1Original, circle2Original, circle3Original, scale + EPSILON);
            }

            // ANDEN DEL
            if (d < Math.Abs(c0.Radius - c1.Radius))
            {
                // no solution. one circle is contained in the other
                return false;
            }

            // TRIN 2 her et sted
            List<Point> intersectionPoints;
            // TODO: Find alle skæringspunkter og kom dem i listen.

            // 'point 2' is the point where the line through the circle
            // intersection points crosses the line between the circle
            // centers.

            // Determine the distance from point 0 to point 2.
            a = ((c0.Radius * c0.Radius) - (c1.Radius * c1.Radius) + (d * d)) / (2.0 * d);

            Point p2;

            // Determine the coordinates of point 2.
            p2 = new Point(
                c0.X + (dx * a / d),
                c0.Y + (dy * a / d));

            // Determine the distance from point 2 to either of the
            // intersection points.
            h = Math.Sqrt((c0.Radius * c0.Radius) - (a * a));

            // Now determine the offsets of the intersection points from
            // point 2.
            Point r = new Point(-dy * (h / d), dx * (h / d));

            // Determine the absolute intersection points.
            Point ip1 = p2 + r;
            Point ip2 = p2 - r;

            Console.WriteLine($"INTERSECTION Circle1 AND Circle2: ({ip1.X}, {ip1.Y}) AND ({ip2.X}, {ip2.Y})");

            // Lets determine if circle 3 intersects at either of the above intersection points.
            dx = ip1.X - c2.X;
            dy = ip1.Y - c2.Y;
            double d1 = Math.Sqrt((dy * dy) + (dx * dx));

            dx = ip2.X - c2.X;
            dy = ip2.Y - c2.Y;
            double d2 = Math.Sqrt((dy * dy) + (dx * dx));

            if (Math.Abs(d1 - c2.Radius) < EPSILON)
            {
                Console.WriteLine($"INTERSECTION Circle1 AND Circle2 AND Circle3: ({ip1.X}, {ip1.Y})");
            }
            else if (Math.Abs(d2 - c2.Radius) < EPSILON)
            {
                // here was an error
                Console.WriteLine($"INTERSECTION Circle1 AND Circle2 AND Circle3: ({ip2.X}, {ip2.Y})");
            }
            else
            {
                Console.WriteLine("INTERSECTION Circle1 AND Circle2 AND Circle3: NONE");
            }

            Console.WriteLine("Scale: " + scale);

            return true;
        }

        private readonly struct Point
        {
            public readonly double X;
            public readonly double Y;

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public static Point operator +(Point p1, Point p2)
            {
                return new Point(p1.X + p2.X, p1.Y + p2.Y);
            }

            public static Point operator -(Point p1, Point p2)
            {
                return new Point(p1.X - p2.X, p1.Y - p2.Y);
            }
        }

        private readonly struct Circle
        {
            public readonly double X;
            public readonly double Y;
            public readonly double Radius;

            public Circle(double x, double y, double radius)
            {
                X = x;
                Y = y;
                Radius = radius;
            }
        }
    }
}
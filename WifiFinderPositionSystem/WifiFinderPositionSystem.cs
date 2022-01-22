using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine(CalculateThreeCircleIntersection(new Circle(1, 1, 2), new Circle(1, 5, 3), new Circle(7, 1, 2)));

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

        private Point? CalculateThreeCircleIntersection(
            Circle circle1Original,
            Circle circle2Original,
            Circle circle3Original,
            double scale=1)
        {
            // TRIN 1: Find den rigtige skalering, så alle cirkler mindst skærer hinanden parvis.
            // TRIN 2: Udregn alle skæringspunkt kombinationerne, der skulle gerne være 6 punkter.
            // TRIN 3: Find 3 tætteste punkter (dem som er tæt på midten).
            // TRIN 4: Find det centrum de 3 punkter udgør.

            double dx, dy, d;

            // scaled radii
            Circle c0, c1, c2;

            c0 = new Circle(circle1Original.X, circle1Original.Y, circle1Original.Radius * scale);
            c1 = new Circle(circle2Original.X, circle2Original.Y, circle2Original.Radius * scale);
            c2 = new Circle(circle3Original.X, circle3Original.Y, circle3Original.Radius * scale);

            // -- TRIN 1
            // Tjek om cirklerne skærer hinanden, hvis ikke, så udvid radius.
            // Derudover, tjek om cirklerne indeholder hinanden, hvis det er sandt har man i virkeligheden kun 2 cirkler
            // og man kan ikke finde et enkelt centrum.

            // - C0 — C1
            // dx and dy are the vertical and horizontal distances between
            // the circle centers.
            dx = c1.X - c0.X;
            dy = c1.Y - c0.Y;

            // Determine the straight-line distance between the centers.
            d = Math.Sqrt((dy * dy) + (dx * dx));

            // Tjek om cirklerne 0 og 1 skærer hinanden.
            if (d > (c0.Radius + c1.Radius))
            {
                return CalculateThreeCircleIntersection(circle1Original, circle2Original, circle3Original, scale + EPSILON);
            }

            // Tjek om cirklerne 0 og 1 er inde i hinanden.
            if (d < Math.Abs(c0.Radius - c1.Radius))
            {
                return null;
            }

            // - C1 — C2
            dx = c2.X - c1.X;
            dy = c2.Y - c1.Y;

            d = Math.Sqrt((dy * dy) + (dx * dx));

            // Tjek om cirklerne 1 og 2 skærer hiannden.
            if (d > (c1.Radius + c2.Radius))
            {
                return CalculateThreeCircleIntersection(circle1Original, circle2Original, circle3Original, scale + EPSILON);
            }

            // Tjek om cirklerne 1 og 2 er inde i hinanden.
            if (d < Math.Abs(c1.Radius - c2.Radius))
            {
                return null;
            }

            // - C0 — C2
            dx = c2.X - c0.X;
            dy = c2.Y - c0.Y;

            d = Math.Sqrt((dy * dy) + (dx * dx));

            // Tjek om cirklerne 0 og 2 skærer hinanden.
            if (d > (c0.Radius + c2.Radius))
            {
                return CalculateThreeCircleIntersection(circle1Original, circle2Original, circle3Original, scale + EPSILON);
            }

            // Tjek om cirklerne 0 og 2 er inde i hinanden.
            if (d < Math.Abs(c0.Radius - c2.Radius))
            {
                return null;
            }

            // -- TRIN 2: find alle skæringspunkter
            List<Point> intersectionPoints = new List<Point>(6);

            intersectionPoints.AddRange(FindRadicalPoints(c0, c1));
            intersectionPoints.AddRange(FindRadicalPoints(c1, c2));
            intersectionPoints.AddRange(FindRadicalPoints(c2, c0));

            // -- TRIN 3 skal finde de tætteste punkter.
            List<(double, Point)> distances = new List<(double, Point)>(intersectionPoints.Count);

            Point[] receivers = new Point[]
            {
                new Point(c0.X, c0.Y),
                new Point(c1.X, c1.Y),
                new Point(c2.X, c2.Y),
            };

            distances.AddRange(from Point item in intersectionPoints
                               select (SumOfDistances(item, receivers), item));

            var sortedDistances = from intersectionPoint in distances
                                  orderby intersectionPoint.Item1 ascending
                                  select intersectionPoint.Item2;

            IEnumerable<Point> centerIntersections = sortedDistances.Take(3);

            // -- TRIN 4: Find det centrum de tre punkter udgør
            Point sum = Point.Zero;
            foreach (Point item in centerIntersections)
            {
                sum += item;
            }

            Console.WriteLine("Scale: " + scale);

            // TODO: Eksperimenter med vinkelhalverings ting.
            return new Point(sum.X / centerIntersections.Count(), sum.Y / centerIntersections.Count());
        }

        private IEnumerable<Point> FindRadicalPoints(Circle c0, Circle c1)
        {
            double dx, dy, d;
            double a, h;

            // C0 — C1
            // dx and dy are the vertical and horizontal distances between
            // the circle centers.
            dx = c1.X - c0.X;
            dy = c1.Y - c0.Y;

            // Determine the straight-line distance between the centers.
            d = Math.Sqrt((dy * dy) + (dx * dx));

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
            yield return p2 + r;
            yield return p2 - r;
        }

        private double SumOfDistances(Point origin, params Point[] otherPoints)
        {
            double sum = 0;

            for (int i = 0; i < otherPoints.Length; i++)
            {
                sum += origin.Distance(otherPoints[i]);
            }

            return sum;
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

            public static Point Zero => new Point(0, 0);

            public static Point operator +(Point p1, Point p2)
            {
                return new Point(p1.X + p2.X, p1.Y + p2.Y);
            }

            public static Point operator -(Point p1, Point p2)
            {
                return new Point(p1.X - p2.X, p1.Y - p2.Y);
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }

            public double Distance(Point p)
            {
                // TODO: Make this a sum of squares to avoid squareroot operation.
                return Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2));
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
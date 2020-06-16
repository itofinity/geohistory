using System;
using System.Collections.Generic;
using System.Text;

namespace Uk.Co.Itofinity.GeoHistory.Render.Kml
{
    public struct Vector
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double MagnitudeSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Magnitude()
        {
            return Math.Sqrt(MagnitudeSquared());
        }

        public Vector ToUnit()
        {
            double m = Magnitude();

            return new Vector(X / m, Y / m, Z / m);
        }

        public Gps ToGps()
        {
            Vector unit = ToUnit();
            // Rounding errors
            double z = unit.Z;
            if (z > 1)
                z = 1;

            double lat = Math.Asin(z);

            double lng = Math.Atan2(unit.Y, unit.X);

            return new Gps(lat * 180 / Math.PI, lng * 180 / Math.PI);
        }

        public static Vector operator *(double m, Vector v)
        {
            return new Vector(m * v.X, m * v.Y, m * v.Z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", X, Y, Z);
        }

        public double Dot(Vector that)
        {
            return X * that.X + Y * that.Y + Z * that.Z;
        }

        public Vector Cross(Vector that)
        {
            return new Vector(Y * that.Z - Z * that.Y, Z * that.X - X * that.Z, X * that.Y - Y * that.X);
        }

        // Pick a random orthogonal vector
        public Vector Orthogonal()
        {
            double minNormal = Math.Abs(X);
            int minIndex = 0;
            if (Math.Abs(Y) < minNormal)
            {
                minNormal = Math.Abs(Y);
                minIndex = 1;
            }
            if (Math.Abs(Z) < minNormal)
            {
                minNormal = Math.Abs(Z);
                minIndex = 2;
            }

            Vector B;
            switch (minIndex)
            {
                case 0:
                    B = new Vector(1, 0, 0);
                    break;
                case 1:
                    B = new Vector(0, 1, 0);
                    break;
                default:
                    B = new Vector(0, 0, 1);
                    break;
            }

            return (B - minNormal * this).ToUnit();
        }
    }
}

namespace RTC_Core {
    using System;

    public class Vector : Tuple {
        public Vector(double x, double y, double z) : base(x, y, z, 0) { }
        internal Vector(Tuple t) : this(t.X, t.Y, t.Z) { }
        public double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

        public static Vector UnitX => new Vector(1, 0, 0);
        public static Vector UnitY => new Vector(0, 1, 0);
        public static Vector UnitZ => new Vector(0, 0, 1);

        public override string ToString() {
            return $"v[{X},{Y},{Z}]";
        }

        public static Vector operator +(Vector a, Vector b) => new Vector((Tuple)a+b);
        public static Vector operator -(Vector a, Vector b) => new Vector((Tuple)a - b);
        public static Vector operator-(Vector v) => new Vector(-(Tuple)v);
        public static Vector operator *(Vector v, double f) => new Vector((Tuple)v*f);
        public static Vector operator /(Vector v, double f) => new Vector((Tuple)v/f);

        public Vector Normalize() {
            var m = Magnitude;
            if (m == 0) {
                return new Vector(0, 0, 0);
            }
            return new Vector(X / m, Y / m, Z / m);
        }
        public double Dot(Tuple v) {
            return X * v.X + Y * v.Y + Z * v.Z + W * v.W;
        }
        public Vector Cross(Vector b) {
            return new Vector(Y * b.Z - Z * b.Y, 
                              Z * b.X - X * b.Z, 
                              X * b.Y - Y * b.X);
        }


    }
}
namespace RTC_Core {
    public class Point : Tuple {
        public Point(double x, double y, double z) : base(x, y, z, 1) {}
        private Point(Tuple t) : this(t.X, t.Y, t.Z) { }

        public static Point operator +(Point p, Vector v) => new Point(p + (Tuple)v);
        public static Vector operator-(Point a, Point b) => new Vector((Tuple)a-b);
        public static Point operator-(Point a, Vector b) => new Point((Tuple)a-b);
        public static Point operator*(Point p, double d)=>new Point((Tuple)p*d);
        public static Point operator/(Point p, double d)=>new Point((Tuple)p/d);

        public override string ToString() {
            return $"p[{X},{Y},{Z}]";
        }

        public static Point Zero() {
            return new Point(0,0,0);
        }
    }
}
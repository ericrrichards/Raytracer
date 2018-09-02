namespace RTC_Core {
    using Microsoft.VisualBasic.CompilerServices;

    public class Color : Tuple {
        

        public Color(double r, double g, double b) : base(r, g, b, 0) { }
        private Color(Tuple t) : this(t.X, t.Y, t.Z) { }
        public double R => X;
        public double G => Y;
        public double B => Z;
        public override string ToString() {
            return $"c[{R},{G},{B}]";
        }
        public static Color operator+(Color a, Color b)=>new Color((Tuple)a+b);
        public static Color operator-(Color a, Color b)=>new Color((Tuple)a-b);
        public static Color operator*(Color a, double d)=>new Color((Tuple)a*d);
        public static Color operator/(Color a, double d)=>new Color((Tuple)a/d);
        public static Color operator*(Color a, Color b) => new Color(a.R*b.R, a.G*b.G, a.B*b.B);
    }
}
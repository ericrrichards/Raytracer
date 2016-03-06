namespace OneWeekendRT.Util {
    using System;
    using System.IO;
    using System.Text;

    public class Bitmap {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private readonly Vector3[] _bitmap;

        public Bitmap(int width, int height) {
            Width = width;
            Height = height;
            _bitmap = new Vector3[width * height];
        }

        public Vector3 this[int x, int y] {
            get {
                if (x >= Width || x < 0) {
                    throw new ArgumentOutOfRangeException("x", x, "Invalid x-coordinate");
                }
                if (y >= Height || y < 0) {
                    throw new ArgumentOutOfRangeException("y", y, "Invalid y-coordinate");
                }
                return _bitmap[y * Width + x];
            }
            set {
                if (x >= Width || x < 0) {
                    throw new ArgumentOutOfRangeException("x", x, "Invalid x-coordinate");
                }
                if (y >= Height || y < 0) {
                    throw new ArgumentOutOfRangeException("y", y, "Invalid y-coordinate");
                }
                _bitmap[y * Width + x] = value;
            }
        }

        public void SavePPM(string filename) {
            var sb = new StringBuilder("P3\n");
            sb.AppendLine(Width + " " + Height);
            sb.AppendLine("255");
            for (var y = Height-1; y >=0; y--) {
                for (var x = 0; x < Width; x++) {
                    var pixel = this[x, y];
                    sb.AppendFormat("{0} {1} {2}\n", (int)(pixel.R * 255.99), (int)(pixel.G * 255.99), (int)(pixel.B * 255.99));
                }
            }

            File.WriteAllText(filename, sb.ToString());
        }
    }
}
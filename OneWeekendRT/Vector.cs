using System.Diagnostics;

namespace OneWeekendRT {
    using OneWeekendRT.Util;

    public class Vector {
        public static void Main(string[] args) {
            var width = 400;
            var height = 200;

            var bitmap = new Bitmap(width, height);

            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var color = new Vector3((float)x / width, (float)y / height, 0.2f);
                    bitmap[x, y] = color;
                }
            }
            bitmap.SavePPM("vector.ppm");
            Process.Start("vector.ppm");
        }
    }
}
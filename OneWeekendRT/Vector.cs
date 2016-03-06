using System.Diagnostics;

namespace OneWeekendRT {
    using OneWeekendRT.Util;

    public class Vector {
        public static void Main(string[] args) {
            var bitmap = new Bitmap(400, 200);

            for (var y = bitmap.Height - 1; y >= 0; y--) {
                for (var x = 0; x < bitmap.Width; x++) {
                    var color = new Vector3((float)x / bitmap.Width, (float)y / bitmap.Height, 0.2f);
                    bitmap[x, y] = color;
                }
            }
            bitmap.SavePPM("vector.ppm");
            Process.Start("vector.ppm");
        }
    }
}
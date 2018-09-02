using OneWeekendRT.Util;
using System.Diagnostics;

namespace OneWeekendRT {
    public class Rays {
        public static void Main(string[] args) {
            var bitmap = new Bitmap(400, 200);

            // setup image plane
            var lowerLeftCorner = new Vector3(-2, -1, -1);
            var horizontal = new Vector3(4, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            // setup camera origin
            var origin = Vector3.Zero;
            
            for (var y = bitmap.Height-1; y >= 0 ; y--) {
                for (var x = 0; x < bitmap.Width; x++) {
                    // calculate image-plane uv coordinates
                    var u = (float)x / bitmap.Width;
                    var v = (float)y / bitmap.Height;
                    // create a ray through the pixel on the image plane
                    var r = new Ray(origin, lowerLeftCorner + horizontal * u + vertical * v);

                    var unit = r.Direction.Normalize();
                    var t = 0.5f * (unit.Y + 1);
                    bitmap[x, y] = Vector3.Lerp(Vector3.One, new Vector3(0.5f, 0.7f, 1.0f), t);
                }
            }
            bitmap.SavePPM("ray.ppm");
            Process.Start("ray.ppm");
        }
    }
}
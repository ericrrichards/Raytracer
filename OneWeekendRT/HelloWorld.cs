using System.Diagnostics;
using System.IO;
using System.Text;

namespace OneWeekendRT {
    public class HelloWorld {

        public static void Main(string[] args) {
            var width = 400;
            var height = 200;

            var sb = new StringBuilder();
            // add PPM header
            sb.AppendLine("P3"); // ASCII PPM
            sb.AppendLine(width + " " + height); // Image dimensions
            sb.AppendLine("255"); // Max color range

            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var r = (float)x / width;
                    var g = (float)y / height;
                    var b = 0.2f;
                    // convert float colors to bytes
                    var ir = (int)(256 * r);
                    var ig = (int)(256 * g);
                    var ib = (int)(256 * b);
                    // Add RGB triplets
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("hello.ppm", sb.ToString());
            Process.Start("hello.ppm");
        }
    }
}
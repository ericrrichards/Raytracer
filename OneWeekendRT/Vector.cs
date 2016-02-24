using System.Diagnostics;
using System.IO;
using System.Text;

namespace OneWeekendRT {
    public class Vector {
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
                    var color = new Vector3((float)x / width, (float)y / height, 0.2f);
                    // convert float colors to bytes
                    var ir = (int)(256 * color.R);
                    var ig = (int)(256 * color.G);
                    var ib = (int)(256 * color.B);
                    // Add RGB triplets
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("vector.ppm", sb.ToString());
            Process.Start("vector.ppm");
        }
    }
}
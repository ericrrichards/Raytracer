using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWeekendRT {
    using System;
    using System.Configuration.Assemblies;
    using System.Diagnostics;
    using System.IO;

    class Program {
        static void Main(string[] args) {
            HelloWorld();
            Vector();
            RayTrace();
            Sphere();
            Sphere2();
        }

        

        private static void HelloWorld() {
            var width = 400;
            var height = 200;

            var sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine(width + " " + height);
            sb.AppendLine("255");
            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var r = (float)x / width;
                    var g = (float)y / height;
                    var b = 0.2f;
                    var ir = (int)(255.99 * r);
                    var ig = (int)(255.99 * g);
                    var ib = (int)(255.99 * b);
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("hello.ppm", sb.ToString());
            Process.Start("hello.ppm");
        }

        private static void Vector() {
            var width = 400;
            var height = 200;

            var sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine(width + " " + height);
            sb.AppendLine("255");
            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var color = new Vector3((float)x / width, (float)y / height, 0.2f);
                    var ir = (int)(255.99 * color.R);
                    var ig = (int)(255.99 * color.G);
                    var ib = (int)(255.99 * color.B);
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("vector.ppm", sb.ToString());
            Process.Start("vector.ppm");
        }

        private static void RayTrace() {
            var width = 400;
            var height = 200;

            var sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine(width + " " + height);
            sb.AppendLine("255");

            var lowerLeftCorner = new Vector3(-2, -1, -1);
            var horizontal = new Vector3(4, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            var origin = Vector3.Zero;

            var c = new Func<Ray, Vector3>(ray => {
                var unit = ray.Direction.Normalize();
                var t = 0.5f * (unit.Y + 1);
                return new Vector3(1,1,1)*(1.0f-t) + new Vector3(0.5f, 0.7f, 1.0f) * t;
            });

            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var u = (float)x / width;
                    var v = (float)y / height;
                    var r = new Ray(origin, lowerLeftCorner + horizontal*u + vertical*v);


                    var color = c(r);
                    var ir = (int)(255.99 * color.R);
                    var ig = (int)(255.99 * color.G);
                    var ib = (int)(255.99 * color.B);
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("ray1.ppm", sb.ToString());
            Process.Start("ray1.ppm");

        }

        private static void Sphere() {
            var width = 400;
            var height = 200;

            var sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine(width + " " + height);
            sb.AppendLine("255");

            var lowerLeftCorner = new Vector3(-2, -1, -1);
            var horizontal = new Vector3(4, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            var origin = Vector3.Zero;

            var hitSphere = new Func<Vector3, float, Ray, bool>((center, r, ray) => {
                var oc = ray.Origin - center;
                var a = ray.Direction.Dot(ray.Direction);
                var b = 2.0f * oc.Dot(ray.Direction);
                var c = oc.Dot(oc) - r * r;
                var discriminant = b * b - 4 * a * c;
                return discriminant > 0;
            });

            var col = new Func<Ray, Vector3>(ray => {
                if (hitSphere(new Vector3(0, 0, -1), 0.5f, ray)) {
                    return new Vector3(1,0,0);
                }
                var unit = ray.Direction.Normalize();
                var t = 0.5f * (unit.Y + 1);
                return new Vector3(1, 1, 1) * (1.0f - t) + new Vector3(0.5f, 0.7f, 1.0f) * t;
            });

            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var u = (float)x / width;
                    var v = (float)y / height;
                    var r = new Ray(origin, lowerLeftCorner + horizontal * u + vertical * v);


                    var color = col(r);
                    var ir = (int)(255.99 * color.R);
                    var ig = (int)(255.99 * color.G);
                    var ib = (int)(255.99 * color.B);
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("sphere.ppm", sb.ToString());
            Process.Start("sphere.ppm");
        }

        private static void Sphere2() {
            var width = 400;
            var height = 200;

            var sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine(width + " " + height);
            sb.AppendLine("255");

            var lowerLeftCorner = new Vector3(-2, -1, -1);
            var horizontal = new Vector3(4, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            var origin = Vector3.Zero;

            var hitSphere = new Func<Vector3, float, Ray, float>((center, r, ray) => {
                var oc = ray.Origin - center;
                var a = ray.Direction.Dot(ray.Direction);
                var b = 2.0f * oc.Dot(ray.Direction);
                var c = oc.Dot(oc) - r * r;
                var discriminant = b * b - 4 * a * c;
                if (discriminant < 0) {
                    return -1;
                }
                return (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
            });

            var col = new Func<Ray, Vector3>(ray => {
                var t = hitSphere(new Vector3(0, 0, -1), 0.5f, ray);
                if (t > 0.0f) {
                    var n = (ray.Evaluate(t) - new Vector3(0, 0, -1)).Normalize();
                    return new Vector3(n.X+1, n.Y+1, n.Z+1)*0.5f;
                }
                var unit = ray.Direction.Normalize();
                t = 0.5f * (unit.Y + 1);
                return new Vector3(1, 1, 1) * (1.0f - t) + new Vector3(0.5f, 0.7f, 1.0f) * t;
            });

            for (var y = height - 1; y >= 0; y--) {
                for (var x = 0; x < width; x++) {
                    var u = (float)x / width;
                    var v = (float)y / height;
                    var r = new Ray(origin, lowerLeftCorner + horizontal * u + vertical * v);


                    var color = col(r);
                    var ir = (int)(255.99 * color.R);
                    var ig = (int)(255.99 * color.G);
                    var ib = (int)(255.99 * color.B);
                    sb.AppendLine(ir + " " + ig + " " + ib);
                }
            }
            File.WriteAllText("sphere2.ppm", sb.ToString());
            Process.Start("sphere2.ppm");
        }
    }
}

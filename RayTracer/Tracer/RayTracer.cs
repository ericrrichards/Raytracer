using System;
using System.Drawing;
using SlimDX;

namespace RayTracer.Tracer {
    public class RayTracer {
        private const float Epsilon = 0.001f;
        public Scene Scene { get; set; }
        public int MaxRecursion { get; set; }

        public RayTracer(Scene scene, int maxRecursion) {
            Scene = scene;
            MaxRecursion = maxRecursion;
        }

        public ColorImage Render() {
            var image = new ColorImage(Scene.Camera.ResolutionX, Scene.Camera.ResolutionY);
            for (int y = 0; y < image.Height; y++) {
                Console.WriteLine("{0:F1}% complete", ((float)y) / image.Height * 100);
                var v = (y + 0.5f) / (image.Height);
                for (int x = 0; x < image.Width; x++) {
                    var u = (x + 0.5f) / image.Width;
                    Color4 c = Color.Black;

                    var r = Scene.Camera.GetRay(u, v);
                    c += ComputeColor(r, 0);
                    c.Alpha = 1;
                    image[x, y] = c;
                }
            }
            return image;
        }

        private Color4 ComputeColor(Ray ray, int recursionDepth) {
            var i = new Intersection();
            if (Scene.Intersect(ray, ref i)) {
                return ComputeIllumination(ray, i, recursionDepth);
            }
            return Color.Black;
        }

        private Color4 ComputeIllumination(Ray ray, Intersection i, int recursionDepth) {
            Color4 cF = Color.Black;
            foreach (var light in Scene.Lights) {
                var cL = light.ComputeLightIntensity(i.Position);
                cL.Alpha = 1;
                var toLight = light.ComputeLightDirection(i.Position);
                var c = i.Material.ComputeDirectLighting(i.Normal, toLight, -ray.Direction);
                c.Alpha = 1;
                cL *= c;
                var v = ComputeShadow(i.Position, light);

                cL *= v;
                if (v < 0.1) {
                    var foo = "bar";
                }


                cF += cL;
            }
            var cT = i.Material.ComputeTextureColor(i);
            cF *= cT;

            Color4 cR = Color.Black;
            if (i.Material.HasReflection(i.Normal, ray.Direction) && recursionDepth < MaxRecursion) {
                var reflectDir = ray.Direction - i.Normal * (2 * Vector3.Dot(ray.Direction, i.Normal));
                reflectDir.Normalize();

                var reflectRay = new Ray(i.Position + reflectDir * Epsilon, reflectDir);
                cR = ComputeColor(reflectRay, recursionDepth + 1) *
                     i.Material.ComputeReflection(i.Normal, ray.Direction);
            }


            return cF + cF * cR;
        }

        private float ComputeShadow(Vector3 p, Light light) {
            var toLight = light.ComputeLightDirection(p);
            var shadow = new Ray(p + toLight * Epsilon, toLight);
            var i = new Intersection();
            var distToLight = light.ComputeShadowDistance(i.Position);
            var v = 0.0f;
            if (!Scene.Intersect(shadow, ref i) || i.Distance > distToLight) {
                v = 1.0f;
            }
            return v;
        }
    }
}
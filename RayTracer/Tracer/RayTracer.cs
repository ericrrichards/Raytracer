using System;
using System.Drawing;
using System.Threading.Tasks;
using SlimDX;

namespace RayTracer.Tracer {
    public class RayTracer {
        private readonly int _numDistributedSamples = 16;
        private const float Epsilon = 0.0001f;
        private Scene Scene { get; set; }
        private int MaxRecursion { get; set; }

        public RayTracer(Scene scene, int maxRecursion, int numSamples = 64) {
            Scene = scene;
            MaxRecursion = maxRecursion;
            _numDistributedSamples = numSamples;
        }

        public ColorImage Render() {
            var image = new ColorImage(Scene.Camera.ResolutionX, Scene.Camera.ResolutionY);

            for (var y = 0; y < image.Height; y++) {
                //Console.WriteLine("{0:F1}% complete", ((float)y) / image.Height * 100);
                var v = (y + 0.5f) / (image.Height);
                int y1 = y;
                Parallel.For(0, image.Height, x => {
                    var u = (x + 0.5f) / image.Width;
                    Color4 c = Color.Black;

                    var r = Scene.Camera.GetRay(u, v);
                    c += ComputeColor(r, 0);
                    c.Alpha = 1;
                    image[x, y1] = c;
                });
            }
            return image;
        }

        private Color4 ComputeColor(Ray ray, int recursionDepth) {
            Intersection i;
            if ((i = Scene.Intersect(ray)) == true) {
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


            var final = cF + cF * cR;
            
            return final;
        }

        private float ComputeShadow(Vector3 p, Light light) {
            if (light is AreaLight && ((AreaLight)light).Radius > 0) {
                var v = 0.0f;
                for (var i = 0; i < _numDistributedSamples; i++) {
                    var toLight = light.ComputeLightDirection(p);
                    var shadow = new Ray(p + toLight * Epsilon, toLight);
                    var distToLight = light.ComputeShadowDistance(p);
                    Intersection intersection;
                    if (!(intersection = Scene.Intersect(shadow)) || intersection.Distance > distToLight) {
                        v += 1.0f;
                    }

                }
                
                v = v/_numDistributedSamples;
                return v;
            } else {
                var toLight = light.ComputeLightDirection(p);
                var shadow = new Ray(p + toLight*Epsilon, toLight);
                Intersection i;
                var distToLight = light.ComputeShadowDistance(p);
                var v = 0.0f;
                if (!(i = Scene.Intersect(shadow)) || i.Distance > distToLight) {
                    v = 1.0f;
                }
                return v;
            }
        }
    }
}
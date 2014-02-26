using System;
using System.Drawing;
using SlimDX;

namespace RayTracer.Tracer {
    public abstract class Material {
        public string TextureID { get; set; }

        public abstract Color4 ComputeDirectLighting(Vector3 normal, Vector3 toLight, Vector3 toSurface);
        public abstract bool HasReflection(Vector3 n, Vector3 i);
        public abstract Color4 ComputeReflection(Vector3 n, Vector3 i);

        public virtual Color4 ComputeTextureColor(Intersection i) {
            var img = TextureStore.GetTexture(i.Material.TextureID);

            var w = img.Width;
            var h = img.Height;

            var u = (int) (i.TexCoord.X*w);
            var v = (int) (i.TexCoord.Y*h);
            var c = img[u, v];
            return c;
        }

    }

    public class Lambert : Material {
        public Color4 Diffuse { get; set; }

        public override Color4 ComputeDirectLighting(Vector3 normal, Vector3 toLight, Vector3 toSurface) {
            var cD = Diffuse;
            cD *= Math.Max(0, Vector3.Dot(normal, toLight));
            return cD;
        }

        public override bool HasReflection(Vector3 n, Vector3 i) {
            return false;
        }

        public override Color4 ComputeReflection(Vector3 n, Vector3 i) {
            return Color.Black;
        }
    }
    public class Phong : Material {
        public Color4 Diffuse { get; set; }
        public Color4 Specular { get; set; }
        public float Exponent { get; set; }

        public override Color4 ComputeDirectLighting(Vector3 normal, Vector3 toLight, Vector3 toSurface) {
            var cD = Diffuse;
            cD *= Math.Max(0, Vector3.Dot(normal, toLight));

            var r = -toLight + normal*(2*Vector3.Dot(normal, toLight));
            var cS = Specular;
            cS *= (float)Math.Pow(Math.Max(0, Vector3.Dot(toSurface, r)), Exponent);
            cD += cS;
            cD.Red = Math.Min(1.0f, cD.Red);
            cD.Green = Math.Min(1.0f, cD.Green);
            cD.Blue = Math.Min(1.0f, cD.Blue);

            return cD;
        }

        public override bool HasReflection(Vector3 n, Vector3 i) {
            return false;
        }

        public override Color4 ComputeReflection(Vector3 n, Vector3 i) {
            return Color.Black;
        }
    }
}
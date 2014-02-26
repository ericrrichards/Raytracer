using System;
using SlimDX;

namespace RayTracer.Tracer {
    public abstract class Light {
        public Color4 Intensity { get; set; }
        public abstract Vector3 ComputeLightDirection(Vector3 surfPoint);
        public abstract Color4 ComputeLightIntensity(Vector3 surfPoint);
        public abstract float ComputeShadowDistance(Vector3 surfPoint);
    }

    public class PointLight : Light {
        public Vector3 Position { get; set; }

        public override Vector3 ComputeLightDirection(Vector3 surfPoint) {
            return Vector3.Normalize(Position - surfPoint);
        }

        public override Color4 ComputeLightIntensity(Vector3 surfPoint) {
            return Intensity;
        }

        public override float ComputeShadowDistance(Vector3 surfPoint) {
            var d = Position - surfPoint;
            return d.Length();
        }
    }

    public class AreaLight : PointLight {
        private static readonly Random Rand = new Random();
        public float Radius { get; set; }

        public override Vector3 ComputeLightDirection(Vector3 surfPoint) {
            var p = SamplePoint(surfPoint);
            return Vector3.Normalize(p - surfPoint);
        }

        private Vector3 SamplePoint(Vector3 surfPoint) {
            var r1 = Rand.NextDouble();
            var r2 = Rand.NextDouble();
            var phi = 2*Math.PI*r1;
            var r = Radius*Math.Sqrt(r2);
            
            var z = Vector3.Normalize(surfPoint - Position);
            var temp = new Vector3(0, 1, 0);
            var x = Vector3.Cross(z, temp);
            var y = Vector3.Cross(z, x);

            var p = x*(float) (r*Math.Cos(phi)) + y*(float) (r*Math.Sin(phi));

            p += Position;
            return p;
        }
    }
}
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
}
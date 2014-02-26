using SlimDX;

namespace RayTracer.Tracer {
    public class Intersection {
        public bool Intersected { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public Material Material { get; set; }
        public float Distance { get; set; }
        public Vector2 TexCoord { get; set; }

        public Intersection(bool intersected) {
            Intersected = intersected;
            Distance = float.MaxValue;
        }

        public static implicit operator bool(Intersection i) { return i.Intersected; }
    }
}
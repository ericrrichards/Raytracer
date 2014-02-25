using SlimDX;

namespace RayTracer.Tracer {
    public class Intersection {
        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public Material Material { get; set; }
        public float Distance { get; set; }
        public Vector2 TexCoord { get; set; }
    }
}
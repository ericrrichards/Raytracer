using System;
using SlimDX;

namespace RayTracer.Tracer {
    public abstract class Surface {
        public abstract Intersection Intersect(Ray r);
        public abstract void Setup();
        public Material Material { get; set; }
    }

    public class Sphere : Surface {
        //public Material Material { get; set; }
        public Vector3 Position { get; set; }
        public float Radius { get; set; }

        public override Intersection Intersect(Ray r) {
            var w = Position - r.Position;
            var tp = Vector3.Dot(w, r.Direction);
            if (tp < 0) {
                return new Intersection(false);
            }
            var d2 = Vector3.Dot(w, w) - tp*tp;
            var r2 = Radius*Radius;
            if (d2 > r2) {
                return new Intersection(false);
            }
            var tr = (float)Math.Sqrt(r2 - d2);
            var t = tp - tr;
            if (t < 0) {
                return new Intersection(false);
            }

            var p = r.Position + r.Direction*t;
            var n = Vector3.Normalize(p - Position);
            var p1 = p - Position;
            var phi = Math.Atan(p1.Y/p1.X);
            var theta = Math.Atan(p1.Z/p1.Length());
            phi = phi/(2*Math.PI) + 0.5f;
            theta = theta/(Math.PI) + 0.5f;

            var uv = new Vector2((float) phi, (float) theta);
            var intersection = new Intersection(true) {
                Position = p, 
                Material = Material, 
                Distance = t, 
                Normal = n, 
                TexCoord = uv
            };

            return intersection;
        }

        public override void Setup() {
            
        }
    }
    public class Triangle : Surface {
        //public Material Material { get; set; }
        public Vector3 V0 { get; set; }
        public Vector3 V1 { get; set; }
        public Vector3 V2 { get; set; }
        public Vector3 Normal { get; set; }
        public Vector2 UV0 { get; set; }
        public Vector2 UV1 { get; set; }
        public Vector2 UV2 { get; set; }

        public override Intersection Intersect(Ray r) {
            float t, u, v;
            
            var hit = Ray.Intersects(r, V0, V1, V2, out t, out u, out v);

            var intersection = new Intersection(false);
            if (hit) {
                intersection.Intersected = true;
                intersection.TexCoord = UV0*(1.0f - u - v) + UV1*u + UV2*v;
                intersection.Distance = t;
                intersection.Material = Material;
                intersection.Normal = Normal;
                intersection.Position = r.Position + r.Direction*t;
            }
            return intersection;
        }

        public override void Setup() {
            Normal = Vector3.Normalize(Vector3.Cross(V1 - V0, V2 - V0));
        }
    }
}
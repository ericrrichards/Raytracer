using System;
using SlimDX;

namespace RayTracer.Tracer {
    public class Camera {
        public Vector3 Origin { get; set; }
        public Vector3 Target { get; set; }
        public Vector3 Right { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 Look { get; set; }

        public float FovX { get; set; }
        public float FovY { get; set; }

        public int ResolutionX { get; set; }
        public int ResolutionY { get; set; }

        public Ray GetRay(float u, float v) {
            var planePoint = GetPlanePoint(u, v);
            var dir = planePoint - Origin;
            dir.Normalize();
            return new Ray(Origin, dir);
        }

        private Vector3 GetPlanePoint(float u, float v) {
            var imagePlaneWidth = (float) (2*Math.Tan(FovX));
            var imagePlaneHeight = (float) (2*Math.Tan(FovY));

            var i = (u - 0.5f)*imagePlaneWidth;
            var j = -(v - 0.5f)*imagePlaneHeight;

            var ret = Origin + Right*i + Up*j + Look;
            return ret;
        }
        public void Setup() {
            Look = Vector3.Normalize(Target - Origin);

            Up = Vector3.Normalize(Up - Look*Vector3.Dot(Up, Look));
            Right = Vector3.Cross( Up, Look);

            FovY = Radians(FovY);
            FovX = FovY*((float) ResolutionX/ResolutionY);
        }

        private static float Radians(float degrees) {
            return (float) ((Math.PI / 180) * degrees);
        }
    }
}
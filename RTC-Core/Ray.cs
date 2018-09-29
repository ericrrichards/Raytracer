namespace RTC_Core {
    using System;

    public class Ray {
        public Point Origin { get; }
        public Vector Direction { get; }

        public Ray(Point origin, Vector direction) {
            Origin = origin;
            Direction = direction;
        }

        public Point Position(double distance) {
            return Origin + Direction * distance;
        }

        public Intersections Intersect(Sphere sphere) {
            var r2 = Transform(sphere.Transform.Inverse());
            var sphereToRay = r2.Origin - new Point(0, 0, 0);
            var a = r2.Direction.Dot(r2.Direction);
            var b = 2 * r2.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0) {
                return new Intersections();
            }
            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            if (t1 > t2) {
                var temp = t1;
                t1 = t2;
                t2 = temp;
            }
            return new Intersections(new Intersection(t1, sphere), new Intersection(t2, sphere));
        }

        public Ray Transform(Matrix m) {
            return new Ray(m*Origin, m*Direction);

        }
    }

    
}
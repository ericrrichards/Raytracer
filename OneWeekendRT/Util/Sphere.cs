namespace OneWeekendRT.Util {
    using System;

    public class Sphere : IHitable {
        public Vector3 Center { get; set; }
        public float Radius { get; set; }

        public Sphere(Vector3 center, float radius) {
            Center = center;
            Radius = radius;
        }

        public virtual bool Hit(Ray r, float tMin, float tMax, out HitRecord record) {
            record = new HitRecord();
            var toOrigin = r.Origin - Center;
            var a = r.Direction.Dot(r.Direction);
            var b = toOrigin.Dot(r.Direction);
            var c = toOrigin.Dot(toOrigin) - Radius*Radius;
            var discriminant = b*b - a*c;
            if (!(discriminant > 0)) {
                return false;
            }
            var temp = (float) ((-b - Math.Sqrt(b*b - a*c))/a);
            if (temp < tMax && temp > tMin) {
                record.Time = temp;
                record.Postition = r.Evaluate(record.Time);
                record.Normal = (record.Postition - Center)/Radius;
                return true;
            }
            temp = (float)((-b + Math.Sqrt(b * b - a * c)) / a);
            if (temp < tMax && temp > tMin) {
                record.Time = temp;
                record.Postition = r.Evaluate(record.Time);
                record.Normal = (record.Postition - Center)/Radius;
                return true;
            }
            return false;
        }
    }
}
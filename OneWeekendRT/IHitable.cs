namespace OneWeekendRT {
    public struct HitRecord {
        public float Time { get; set; }
        public Vector3 Postition { get; set; }
        public Vector3 Normal { get; set; }
    }

    public interface IHitable {
        bool Hit(Ray r, float tMin, float tMax, out HitRecord record);
    }
}

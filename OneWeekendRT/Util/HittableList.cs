namespace OneWeekendRT.Util {
    using System.Collections.Generic;

    public class HittableList : IHitable {
        public List<IHitable> List { get; set; }

        public HittableList() {
            List=new List<IHitable>();
        }
        public void Add(IHitable h) { List.Add(h);}

        public virtual bool Hit(Ray r, float tMin, float tMax, out HitRecord record) {
            var hitAnything = false;
            var closestSoFar = tMax;
            record = new HitRecord();
            foreach (var hitable in List) {
                HitRecord temp;
                if (hitable.Hit(r, tMin, closestSoFar, out temp)) {
                    hitAnything = true;
                    closestSoFar = temp.Time;
                    record = temp;
                }
            }
            return hitAnything;
        }
    }
}
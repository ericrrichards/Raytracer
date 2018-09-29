namespace RTC_Core {
    using System.Collections.Generic;
    using System.Linq;

    public class Intersections {
        private List<Intersection> Hits { get; }
        public int Count => Hits.Count;
        public Intersection this[int i] => Hits[i];

        public Intersections(params Intersection[] hits) {
            Hits = new List<Intersection>(hits);
        }

        public Intersection Hit() {
            return Hits.Where(h => h.Distance >= 0).OrderBy(h=>h.Distance).FirstOrDefault();
        }
    }
    public class Intersection {
        public double Distance { get; }
        public object Object { get; }

        public Intersection(double t, object o) {
            Distance = t;
            Object = o;
        }
    }
}
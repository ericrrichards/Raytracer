namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class IntersectionTests {
        [Test]
        public void IntersectionIsADistanceAndAnObject() {
            var s = new Sphere();
            var i = new Intersection(3.5, s);
            Assert.AreEqual(3.5, i.Distance);
            Assert.AreEqual(s, i.Object);
        }

        [Test]
        public void AggregatingIntersections() {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = new Intersections(i1, i2);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(1, xs[0].Distance);
            Assert.AreEqual(2, xs[1].Distance);
        }

        [Test]
        public void HitWhenAllDistancesPositive() {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = new Intersections(i1, i2);
            var h = xs.Hit();
            Assert.AreEqual(i1, h);
        }

        [Test]
        public void HitWhenSomeDistancesNegative() {
            var s = new Sphere();
            var i1 = new Intersection(-1, s);
            var i2 = new Intersection(1, s);
            var xs = new Intersections(i1,i2);
            var h = xs.Hit();
            Assert.AreEqual(i2, h);
        }

        [Test]
        public void HitWhenAllDistancesNegative() {
            var s = new Sphere();
            var i1 = new Intersection(-2, s);
            var i2 = new Intersection(-1, s);
            var xs = new Intersections(i1,i2);
            var h = xs.Hit();
            Assert.AreEqual(null, h);

        }

        [Test]
        public void HitIsAlwaysLowestNonNegativeIntersection() {
            var s = new Sphere();
            var i1 = new Intersection(5, s);
            var i2 = new Intersection(7, s);
            var i3 = new Intersection(-3, s);
            var i4 = new Intersection(2, s);
            var xs = new Intersections(i1,i2,i3,i4);
            var h = xs.Hit();
            Assert.AreEqual(i4, h);
        }
    }
}
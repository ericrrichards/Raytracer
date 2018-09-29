namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class SphereTests {
        [Test]
        public void RayIntersectsSphereAtTwoPoints() {
            var r = new Ray(new Point(0,0,-5),new Vector(0,0,1) );
            var s = new Sphere();
            var hits = r.Intersect(s);
            Assert.AreEqual(2, hits.Count);
            Assert.AreEqual(4, hits[0].Distance);
            Assert.AreEqual(6, hits[1].Distance);
        }

        [Test]
        public void RayIntersectsSphereAtATangent() {
            var r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var hits = r.Intersect(s);
            Assert.AreEqual(2, hits.Count);
            Assert.AreEqual(5, hits[0].Distance);
            Assert.AreEqual(5, hits[1].Distance);
        }

        [Test]
        public void RayMissesSphere() {
            var r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
            var s = new Sphere();
            var hits = r.Intersect(s);
            Assert.AreEqual(0, hits.Count);
        }

        [Test]
        public void RayOriginInSphere() {
            var r = new Ray(new Point(0,0,0), new Vector(0,0,1) );
            var s = new Sphere();
            var hits = r.Intersect(s);
            Assert.AreEqual(2, hits.Count);
            Assert.AreEqual(-1, hits[0].Distance);
            Assert.AreEqual(1, hits[1].Distance);
        }

        [Test]
        public void RayIsAheadOfSphere() {
            var r = new Ray(new Point(0,0,5),new Vector(0,0,1) );
            var s = new Sphere();
            var hits = r.Intersect(s);
            Assert.AreEqual(2, hits.Count);
            Assert.AreEqual(-6, hits[0].Distance);
            Assert.AreEqual(-4, hits[1].Distance);
        }

        [Test]
        public void IntersectSetsTheObjectOnTheIntersection() {
            var r = new Ray(new Point(0,0,-5), new Vector(0,0,1) );
            var s = new Sphere();
            var xs = r.Intersect(s);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(s, xs[0].Object);
            Assert.AreEqual(s, xs[1].Object);
        }

        [Test]
        public void SphereDefaultTransformIsIdentity() {
            var s = new Sphere();
            Assert.AreEqual(Matrix.Identity, s.Transform);
        }

        [Test]
        public void ChangingASpheresTransform() {
            var s = new Sphere();
            var t = Matrix.Translate(2, 3, 4);
            s.Transform = t;
            Assert.AreEqual(t, s.Transform);
        }

        [Test]
        public void IntersectingAScaledSphereWithARay() {
            var r = new Ray(new Point(0,0,-5),new Vector(0,0,1) );
            var s = new Sphere {
                Transform = Matrix.Scale(2, 2, 2)
            };
            var xs = r.Intersect(s);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(3, xs[0].Distance);
            Assert.AreEqual(7, xs[1].Distance);
        }

        [Test]
        public void IntersectingATranslatedSphereWithARay() {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = new Sphere {
                Transform = Matrix.Translate(5, 0, 0)
            };
            var xs = r.Intersect(s);
            Assert.AreEqual(0, xs.Count);
        }
    }
}
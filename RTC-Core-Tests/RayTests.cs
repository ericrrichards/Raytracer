namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class RayTests {
        [Test]
        public void CreateARay() {
            var origin = new Point(1,2,3);
            var direction = new Vector(4,5,6);

            var r = new Ray(origin, direction);
            Assert.AreEqual(origin, r.Origin);
            Assert.AreEqual(direction, r.Direction);
        }

        [Test]
        public void ComputePositionFromDistance() {
            var r = new Ray(new Point(2, 3, 4), new Vector(1, 0, 0));
            Assert.AreEqual(new Point(2,3,4),r.Position(0) );
            Assert.AreEqual(new Point(3,3,4), r.Position(1) );
            Assert.AreEqual(new Point(1,3,4), r.Position(-1) );
            Assert.AreEqual(new Point(4.5, 3,4), r.Position(2.5) );
        }

        [Test]
        public void TranslateRay() {
            var r = new Ray(new Point(1,2,3), new Vector(0,1,0) );
            var m = Matrix.Translate(3, 4, 5);
            Ray r2 = r.Transform(m);
            Assert.AreEqual(new Point(4,6,8), r2.Origin);
            Assert.AreEqual(new Vector(0,1,0), r2.Direction );
        }

        [Test]
        public void ScaleRay() {
            var r = new Ray(new Point(1,2,3), new Vector(0,1,0) );
            var m = Matrix.Scale(2, 3, 4);
            Ray r2 = r.Transform(m);
            Assert.AreEqual(new Point(2,6,12), r2.Origin);
            Assert.AreEqual(new Vector(0,3,0), r2.Direction );
        }
    }
}

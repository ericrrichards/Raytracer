namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class PointTests {
        [Test]
        public void Subtract_TwoPoints() {
            var p1 = new Point(3, 2, 1);
            var p2 = new Point(5, 6, 7);
            Assert.AreEqual(new Vector(-2, -4, -6), p1 - p2);
        }

        [Test]
        public void Subtract_VectorFromPoint() {
            var p = new Point(3, 2, 1);
            var v = new Vector(5, 6, 7);
            Assert.AreEqual(new Point(-2, -4, -6), p - v);
        }
    }
}
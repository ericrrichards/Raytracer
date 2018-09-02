namespace RTC_Core_Tests {
    using System;

    using NUnit.Framework;

    using RTC_Core;

    public class VectorTests {
        [Test]
        public void Subtract_TwoVectors() {
            var v1 = new Vector(3, 2, 1);
            var v2 = new Vector(5, 6, 7);
            Assert.AreEqual(new Vector(-2, -4, -6), v1 - v2);

        }

        [Test]
        public void SubtractVectorFromZero() {
            var zero = new Vector(0, 0, 0);
            var v = new Vector(1, -2, 3);
            Assert.AreEqual(new Vector(-1, 2, -3), zero - v);
        }

        [Test]
        public void MagnitudeOfUnitXVector() {
            var v = new Vector(1, 0, 0);
            Assert.AreEqual(1, v.Magnitude);
        }

        [Test]
        public void MagnitudeOfUnitYVector() {
            var v = new Vector(0, 1, 0);
            Assert.AreEqual(1, v.Magnitude);
        }

        [Test]
        public void MagnitudeOfUnitZVector() {
            var v = new Vector(0, 0, 1);
            Assert.AreEqual(1, v.Magnitude);
        }

        [Test]
        public void MagnitudeOf_1_2_3() {
            var v = new Vector(1, 2, 3);
            Assert.AreEqual(Math.Sqrt(14), v.Magnitude);
        }

        [Test]
        public void MagnitudeOfNegative_1_2_3() {
            var v = new Vector(-1, -2, -3);
            Assert.AreEqual(Math.Sqrt(14), v.Magnitude);
        }

        [Test]
        public void Normalize_4_0_0_isUnitX() {
            var v = new Vector(4, 0, 0);
            Assert.AreEqual(Vector.UnitX, v.Normalize());
        }

        [Test]
        public void Normalize_1_2_3() {
            var v = new Vector(1, 2, 3);
            Assert.AreEqual(new Vector(0.26726, 0.53452, 0.80178), v.Normalize());
        }

        [Test]
        public void Normalized_HasMagnitudeOf1() {
            var v = new Vector(1, 2, 3);
            Assert.AreEqual(1, v.Normalize().Magnitude);
        }

        [Test]
        public void NormalizedZeroVector_HasMagnitude0() {
            var v = new Vector(0, 0, 0);
            Assert.AreEqual(0, v.Normalize().Magnitude);
        }

        [Test]
        public void DotProduct() {
            var a = new Vector(1, 2, 3);
            var b = new Vector(2, 3, 4);
            Assert.AreEqual(20, a.Dot(b));
        }

        [Test]
        public void CrossProduct() {
            var a = new Vector(1, 2, 3);
            var b = new Vector(2, 3, 4);
            Assert.AreEqual(new Vector(-1, 2, -1), a.Cross(b));
            Assert.AreEqual(new Vector(1, -2, 1), b.Cross(a));
        }
    }
}
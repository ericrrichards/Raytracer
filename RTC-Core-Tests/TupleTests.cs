using RTC_Core;

namespace RTC_Core_Tests {
    using NUnit.Framework;

    using Tuple = RTC_Core.Tuple;

    [TestFixture]
    public class TupleTests {

        [Test]
        public void Tuple_WithWEquals1_IsAPoint() {
            var a = new Tuple(4.3, -4.2, 3.1, 1.0);

            Assert.AreEqual(4.3, a.X);
            Assert.AreEqual(-4.2, a.Y);
            Assert.AreEqual(3.1, a.Z);
            Assert.AreEqual(1.0, a.W);
            Assert.True(a.IsPoint);
            Assert.False(a.IsVector);

        }

        [Test]
        public void Tuple_WithWEquals0_IsAVector() {
            var a = new Tuple(4.3, -4.2, 3.1, 0.0);
            Assert.AreEqual(4.3, a.X);
            Assert.AreEqual(-4.2, a.Y);
            Assert.AreEqual(3.1, a.Z);
            Assert.AreEqual(0, a.W);
            Assert.False(a.IsPoint);
            Assert.True(a.IsVector);
        }

        [Test]
        public void Point_IsATupleWithW1() {
            var p = new Point(4, -4, 3);
            Assert.AreEqual(new Tuple(4, -4, 3, 1), p);
        }

        [Test]
        public void Vector_IsATupleWithW0() {
            var v = new Vector(4, -4, 3);
            Assert.AreEqual(new Tuple(4, -4, 3, 0), v);
        }

        [Test]
        public void Add() {
            var a = new Tuple(3, -2, 5, 1);
            var b = new Tuple(-2, 3, 1, 0);
            Assert.AreEqual(new Tuple(1, 1, 6, 1), a + b);
        }

        [Test]
        public void Negate() {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(new Tuple(-1, 2, -3, 4), -a);
        }

        [Test]
        public void ScalarMultiplication() {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(new Tuple(3.5, -7, 10.5, -14), a * 3.5);
        }

        [Test]
        public void MultiplyTupleByFraction() {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(new Tuple(0.5, -1, 1.5, -2), a * 0.5);
        }

        [Test]
        public void DivideTupleByScalar() {
            var a = new Tuple(1, -2, 3, -4);
            Assert.AreEqual(new Tuple(0.5, -1, 1.5, -2), a / 2);
        }
    }
}

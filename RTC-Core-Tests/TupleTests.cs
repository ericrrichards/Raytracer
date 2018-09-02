using System;
using System.Collections.Generic;
using System.Text;
using RTC_Core;

namespace RTC_Core_Tests {
    using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

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

        [Test]
        public void Colors_Are_RGBTuples() {
            var c = new Color(-0.5, 0.4, 1.7);
            Assert.AreEqual(-0.5, c.R);
            Assert.AreEqual(0.4, c.G);
            Assert.AreEqual(1.7, c.B);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class MatrixTests {
        [Test]
        public void Construct4x4Matrix() {
            var m = new Matrix(new[,] {
                {1.0, 2, 3,4,},
                {5.5, 6.5, 7.5, 8.5},
                {9,10,11,12},
                {13.5,14.5, 15.5, 16.5}
            });
            Assert.AreEqual(1, m[0, 0]);
            Assert.AreEqual(4, m[0, 3]);
            Assert.AreEqual(5.5, m[1, 0]);
            Assert.AreEqual(7.5, m[1, 2]);
            Assert.AreEqual(11, m[2, 2]);
            Assert.AreEqual(13.5, m[3, 0]);
            Assert.AreEqual(15.5, m[3, 2]);
        }

        [Test]
        public void Construct2x2Matrix() {
            var m = new Matrix(new[,] {
                {-3.0, 5},
                {1, -2}
            });
            Assert.AreEqual(-3, m[0, 0]);
            Assert.AreEqual(5, m[0, 1]);
            Assert.AreEqual(1, m[1, 0]);
            Assert.AreEqual(-2, m[1, 1]);
        }

        [Test]
        public void Construct3x3Matrix() {
            var m = new Matrix(new[,] {
                {-3, 5, 0.0},
                {1, -2, -7},
                {0,1,1}
            });
            Assert.AreEqual(-3, m[0, 0]);
            Assert.AreEqual(-2, m[1, 1]);
            Assert.AreEqual(1, m[2, 2]);
        }

        [Test]
        public void MultiplyingMatrices() {
            var a = new Matrix(new double[,] {
                {1,2,3,4},
                {2,3,4,5},
                {3,4,5,6},
                {4,5,6,7}
            });
            var b = new Matrix(new double[,] {
                {0,1,2,4},
                {1,2,4,8},
                {2,4,8, 16},
                {4, 8, 16, 32}
            });
            Assert.AreEqual(a*b, new Matrix(new double[,] {
                {24, 49, 98, 196},
                {31, 64, 128, 256},
                {38, 79, 158, 316},
                {45,94,188,376}
            }));
        }
    }
}

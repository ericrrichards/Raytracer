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
            Assert.AreEqual(a * b, new Matrix(new double[,] {
                {24, 49, 98, 196},
                {31, 64, 128, 256},
                {38, 79, 158, 316},
                {45,94,188,376}
            }));
        }

        [Test]
        public void MultiplyMatrixWithTuple() {
            var a = new Matrix(new double[,] {
                {1,2,3,4},
                {2,4,4,2},
                {8,6,4,1},
                {0,0,0,1}
            });
            var b = new Tuple(1, 2, 3, 1);
            Assert.AreEqual(new Tuple(18, 24, 33, 1), a * b);
        }

        [Test]
        public void MultiplyingByTheIdentityMatrix() {
            var a = new Matrix(
                               new double[,] {
                                   { 0, 1, 2, 4 },
                                   { 1, 2, 4, 8 },
                                   { 2, 4, 8, 16 },
                                   { 4, 8, 16, 32 }
                               });
            Assert.AreEqual(a, a*Matrix.Identity);
        }

        [Test]
        public void MultipleTupleWithIdentityMatrix() {
            var a = new Tuple(1,2,3,4);
            Assert.AreEqual(a, Matrix.Identity*a);
        }

        [Test]
        public void TransposeMatrix() {
            var a = new Matrix(new double[,] {
                {0,9,3,0},
                {9,8,0,8},
                {1,8,5,3},
                {0,0,5,8}
            });
            var b = new Matrix(new double[,] {
                {0,9,1,0},
                {9,8,8,0},
                {3,0,5,5},
                {0,8,3,8}
            });
            Assert.AreEqual(b, a.Transpose);
        }

        [Test]
        public void TransposeIdentityMatrix() {
            Assert.AreEqual(Matrix.Identity, Matrix.Identity.Transpose);
        }

        [Test]
        public void Determinant2x2Matrix() {
            var a = new Matrix(new double[,] {
                {1,5},
                {-3,2}
            });
            Assert.AreEqual(17, a.Determinant);
        }

        [Test]
        public void SubmatrixOf3x3MatrixIs2x2Matrix() {
            var a = new Matrix(new double[,] {
                {1,5,0},
                {-3, 2,7},
                {0, 6, -3}
            });
            var b = new Matrix(new double[,] {
                {-3, 2},
                {0,6}
            });
            Assert.AreEqual(b, a.SubMatrix(0,2));
        }

        [Test]
        public void SubmatrixOf4x4MatrixIs3x3Matrix() {
            var a = new Matrix(new double[,] {
                {-6, 1,1,6},
                {-8,5,8,6},
                {-1,0,8,2},
                {-7,1,-1,1}
            });
            var b = new Matrix(new double[,] {
                {-6,1,6},
                {-8,8,6},
                {-7,-1,1}
            });
            Assert.AreEqual(b, a.SubMatrix(2,1));
        }

        [Test]
        public void MinorOf3x3Matrix() {
            var a = new Matrix(new double[,] {
                {3,5,0},
                {2,-1,-7},
                {6,-1,5}
            });
            var b = a.SubMatrix(1, 0);
            Assert.AreEqual(25, b.Determinant);
            Assert.AreEqual(25, a.Minor(1,0));
        }

        [Test]
        public void CofactorOf3x3Matrix() {
            var a = new Matrix(
                               new double[,] {
                                   { 3, 5, 0 },
                                   { 2, -1, -7 },
                                   { 6, -1, 5 }
                               });
            Assert.AreEqual(-12, a.Minor(0,0));
            Assert.AreEqual(-12, a.CoFactor(0,0));
            Assert.AreEqual(25, a.Minor(1,0));
            Assert.AreEqual(-25, a.CoFactor(1,0));
        }

        [Test]
        public void DeterminantOf3x3Matrix() {
            var a = new Matrix(new double[,] {
                {1,2,6},
                {-5,8,-4},
                {2,6,4}
            });
            Assert.AreEqual(56, a.CoFactor(0,0));
            Assert.AreEqual(12, a.CoFactor(0,1));
            Assert.AreEqual(-46, a.CoFactor(0, 2));
            Assert.AreEqual(-196, a.Determinant);
        }

        [Test]
        public void DeterminantOf4x4Matrix() {
            var a = new Matrix(new double[,] {
                {-2, -8, 3, 5},
                {-3,1,7,3},
                {1,2,-9,6},
                {-6,7,7,-9}
            });
            Assert.AreEqual(690, a.CoFactor(0,0));
            Assert.AreEqual(447, a.CoFactor(0,1));
            Assert.AreEqual(210, a.CoFactor(0,2));
            Assert.AreEqual(51, a.CoFactor(0,3));
            Assert.AreEqual(-4071, a.Determinant);
        }

        [Test]
        public void Invertible() {
            var a = new Matrix(new double[,] {
                {6,4,4,4},
                {5,5,7,6},
                {4,-9,3,-7},
                {9,1,7,-6}
            });
            Assert.AreEqual(-2120, a.Determinant);
            Assert.True(a.Invertible);
        }

        [Test]
        public void NotInvertible() {
            var a = new Matrix(
                               new double[,] {
                                   { -4, 2, -2, -3 },
                                   { 9, 6, 2, 6 },
                                   { 0, -5, 1, -5 },
                                   { 0, 0, 0, 0 }
                               });
            Assert.AreEqual(0, a.Determinant);
            Assert.False(a.Invertible);
        }

        [Test]
        public void Inverse() {
            var a = new Matrix(new double[,] {
                {-5,2,6,-8},
                {1,-5,1,8},
                {7,7,-6,-7},
                {1,-3,7,4}
            });
            var b = a.Inverse();
            Assert.AreEqual(532, a.Determinant);
            Assert.AreEqual(-160, a.CoFactor(2,3));
            Assert.AreEqual(-160.0/532, b[3,2]);
            Assert.AreEqual(105, a.CoFactor(3,2));
            Assert.AreEqual(105.0/532, b[2,3]);
            var expected = new Matrix(new double[,] {
                {0.21805, 0.45113, 0.24060, -0.04511},
                {-0.80827, -1.45677, -0.44361, 0.52068},
                {-0.07895, -0.22368, -0.05263, 0.19737},
                {-0.52256, -0.81391, -0.30075, 0.30639}
            });
            Assert.AreEqual(expected, b);
        }

        [Test]
        public void Inverse2() {
            var a = new Matrix(new double[,] {
                {8, -5, 9, 2},
                {7,5,6,1},
                {-6,0,9,6},
                {-3,0,-9,-4}
            });
            var b = new Matrix(new double[,] {
                {-0.15385, -0.15385, -0.28205, -0.53846},
                {-0.07692, 0.12308, 0.02564, 0.03077},
                {0.35897, 0.35897, 0.43590, 0.92308},
                {-0.69231, -0.69231, -0.76923, -1.92308}
            });
            Assert.AreEqual(b, a.Inverse());
        }
        [Test]
        public void Inverse3() {
            var a = new Matrix(new double[,] {
                {9, 3, 0, 9},
                {-5,-2,-6,-3},
                {-4,9,6,4},
                {-7,6,6,2}
            });
            var b = new Matrix(new double[,] {
                {-0.04074, -0.07778, 0.14444, -0.22222},
                {-0.07778, 0.03333, 0.36667, -0.33333},
                {-0.02901, -0.14630, -0.10926, 0.12963},
                {0.17778, 0.06667, -0.26667, 0.33333}
            });
            Assert.AreEqual(b, a.Inverse());
        }

        [Test]
        public void MultipleProductByInverse() {
            var a = new Matrix(
                               new double[,] {
                                   { 3, -9, 7, 3 },
                                   { 3, -8, 2, -9 },
                                   { -4, 4, 4, 1 },
                                   { -6, 5, -1, 1 }
                               });
            var b = new Matrix(new double[,] {
                {8,2,2,2},
                {3,-1,7,0},
                {7,0,5,4},
                {6, -2, 0, 5}
            });
            var c = a * b;
            Assert.AreEqual(a, c*b.Inverse());
        }

        [Test]
        public void InverseOfIdentityIsIdentity() {
            Assert.AreEqual(Matrix.Identity, Matrix.Identity.Inverse());
        }

        [Test]
        public void MultipleMatrixWithInverseIsIdentity() {
            var a = new Matrix(
                               new double[,] {
                                   { 3, -9, 7, 3 },
                                   { 3, -8, 2, -9 },
                                   { -4, 4, 4, 1 },
                                   { -6, 5, -1, 1 }
                               });
            Assert.AreEqual(Matrix.Identity, a*a.Inverse());
        }

        [Test]
        public void InverseOfTransposeIsTransposeOfInverse() {
            var a = new Matrix(
                               new double[,] {
                                   { 3, -9, 7, 3 },
                                   { 3, -8, 2, -9 },
                                   { -4, 4, 4, 1 },
                                   { -6, 5, -1, 1 }
                               });
            var b = a.Transpose.Inverse();
            var c = a.Inverse().Transpose;
            Assert.AreEqual(b, c);
        }

        [Test]
        public void MultiplyTupleWithModifiedIdentity() {
            var a = Matrix.Identity;
            var b = new Tuple(1,2,3,4);
            a[0, 1] = 2;
            Assert.AreEqual(new Tuple(5,2,3,4), a*b);
        }
    }
}

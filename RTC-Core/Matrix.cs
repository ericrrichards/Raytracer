using System;
using System.Collections.Generic;
using System.Text;

namespace RTC_Core {
    public class Matrix : IEquatable<Matrix> {
        private readonly double[,] _values;

        public static Matrix Identity => new Matrix(new double[,] {
            {1,0,0,0},
            {0,1,0,0},
            {0,0,1,0},
            {0,0,0,1}
        });


        public Matrix(double[,] v) {
            _values = v;
        }

        private Matrix(int rows, int cols) {
            _values = new double[rows, cols];
        }

        public double this[int row, int column] { get => _values[row, column]; set => _values[row, column] = value; }
        public int Rows => _values.GetLength(0);
        public int Columns => _values.GetLength(1);
        public static Matrix operator *(Matrix a, Matrix b) {
            if (a.Rows != b.Rows || a.Columns != b.Columns) {
                throw new InvalidOperationException("Matrix dimensions do not match");
            }
            var ret = new Matrix(a.Rows, a.Columns);
            for (var row = 0; row < ret.Rows; row++) {
                for (var col = 0; col < ret.Columns; col++) {
                    ret[row, col] = a[row, 0] * b[0, col] +
                                    a[row, 1] * b[1, col] +
                                    a[row, 2] * b[2, col] +
                                    a[row, 3] * b[3, col];
                }
            }
            return ret;
        }

        public static Tuple operator *(Matrix a, Tuple b) {
            if (a.Rows != 4 || a.Columns != 4) {
                throw new InvalidOperationException("Not 4x4 matrix");
            }
            return new Tuple(
                             a[0, 0] * b.X + a[0, 1] * b.Y + a[0, 2] * b.Z + a[0, 3] * b.W,
                             a[1, 0] * b.X + a[1, 1] * b.Y + a[1, 2] * b.Z + a[1, 3] * b.W,
                             a[2, 0] * b.X + a[2, 1] * b.Y + a[2, 2] * b.Z + a[2, 3] * b.W,
                             a[3, 0] * b.X + a[3, 1] * b.Y + a[3, 2] * b.Z + a[3, 3] * b.W
                             );
        }

        public Matrix Transpose {
            get {
                var ret = new Matrix(Rows, Columns);
                for (var row = 0; row < Rows; row++) {
                    for (var col = 0; col < Columns; col++) {
                        ret[col, row] = _values[row, col];
                    }
                }
                return ret;
            }
        }
        public double Determinant {
            get {
                if (Rows == 2 && Columns == 2) {
                    return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
                }
                if (Rows == 3 && Columns == 3) {
                    return this[0, 0] * CoFactor(0, 0) + this[0, 1] * CoFactor(0, 1) + this[0, 2] * CoFactor(0, 2);
                }
                if (Rows == 4 && Columns == 4) {
                    return this[0, 0] * CoFactor(0, 0) + this[0, 1] * CoFactor(0, 1) + this[0, 2] * CoFactor(0, 2) + this[0, 3] * CoFactor(0, 3);
                }
                throw new NotImplementedException();
            }
        }
        public bool Invertible => Determinant != 0;

        public Matrix Inverse() {
            var ret = new Matrix(Rows, Columns);
            for (var row = 0; row < Rows; row++) {
                for (var col = 0; col < Columns; col++) {
                    ret[row, col] = CoFactor(row, col);
                }
            }
            ret = ret.Transpose;
            var determinant = Determinant;
            for (var row = 0; row < Rows; row++) {
                for (var col = 0; col < Columns; col++) {

                    ret[row, col] = ret[row, col] / determinant;
                }
            }
            return ret;
        }

        public Matrix SubMatrix(int row, int col) {
            var ret = new Matrix(Rows - 1, Columns - 1);

            var r1 = 0;
            for (var r = 0; r < Rows; r++) {
                var c1 = 0;
                if (r == row) {
                    continue;
                }
                for (var c = 0; c < Columns; c++) {
                    if (c == col) {
                        continue;
                    }
                    ret[r1, c1] = _values[r, c];
                    c1++;
                }
                r1++;
            }
            return ret;
        }

        public double Minor(int row, int col) => SubMatrix(row, col).Determinant;
        public double CoFactor(int row, int col) {
            if ((row + col) % 2 == 0) {
                return Minor(row, col);
            }
            return -Minor(row, col);
        }
        #region Transformations

        public static Matrix Translate(double x, double y, double z) {
            var ret = Identity;
            ret[0, 3] = x;
            ret[1, 3] = y;
            ret[2, 3] = z;

            return ret;
        }

        public static Matrix Scale(double x, double y, double z) {
            var ret = Identity;
            ret[0, 0] = x;
            ret[1, 1] = y;
            ret[2, 2] = z;
            return ret;
        }

        public static Matrix RotationX(double radians) {
            var ret = Identity;
            ret[1, 1] = Math.Cos(radians);
            ret[1, 2] = -Math.Sin(radians);
            ret[2, 1] = Math.Sin(radians);
            ret[2, 2] = Math.Cos(radians);
            return ret;
        }

        public static Matrix RotationY(double radians) {
            var ret = Identity;
            ret[0, 0] = Math.Cos(radians);
            ret[0, 2] = Math.Sin(radians);
            ret[2, 0] = -Math.Sin(radians);
            ret[2, 2] = Math.Cos(radians);
            return ret;
        }

        public static Matrix RotationZ(double radians) {
            var ret = Identity;
            ret[0, 0] = Math.Cos(radians);
            ret[0, 1] = -Math.Sin(radians);
            ret[1, 0] = Math.Sin(radians);
            ret[1, 1] = Math.Cos(radians);

            return ret;
        }

        public static Matrix Shear(double xy, double xz, double yx, double yz, double zx, double zy) {
            return new Matrix(new[,] {
                {1, xy, xz, 0},
                {yx,1,yz, 0},
                {zx,zy,1,0},
                {0,0,0,1}
            });
        }


        #endregion



        #region Equality stuff
        public bool Equals(Matrix other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            if (Rows != other.Rows || Columns != other.Columns) {
                return false;
            }
            for (var row = 0; row < Rows; row++) {
                for (var col = 0; col < Columns; col++) {
                    if (!NumericExtensions.Equals(this[row, col], other[row, col], 0.00001)) {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            return Equals((Matrix)obj);
        }

        public override int GetHashCode() {
            return (_values != null ? _values.GetHashCode() : 0);
        }

        public static bool operator ==(Matrix left, Matrix right) {
            return Equals(left, right);
        }

        public static bool operator !=(Matrix left, Matrix right) {
            return !Equals(left, right);
        }
        #endregion

        public override string ToString() {
            var sb = new StringBuilder();
            for (var row = 0; row < Rows; row++) {
                for (var col = 0; col < Columns; col++) {
                    sb.Append(this[row, col] + ",");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RTC_Core {
    public class Matrix : IEquatable<Matrix> {
        private readonly double[,] _values;

        public Matrix(double[,] v) {
            _values = v;
        }

        public double this[int row, int column] { get => _values[row, column]; set => _values[row, column] = value; }
        public int Rows => _values.GetLength(0);
        public int Columns => _values.GetLength(1);
        public static Matrix operator *(Matrix a, Matrix b) {
            if (a.Rows != b.Rows || a.Columns != b.Columns) {
                throw new InvalidOperationException("Matrix dimensions do not match");
            }
            var ret = new Matrix(new double[a.Rows,a.Columns]);
            for (int row = 0; row < ret.Rows; row++) {
                for (int col = 0; col < ret.Columns; col++) {
                    ret[row, col] = a[row, 0] * b[0, col] +
                                    a[row, 1] * b[1, col] + 
                                    a[row, 2] * b[2, col] + 
                                    a[row, 3] * b[3, col];
                }
            }
            return ret;
        }

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
            for (int row = 0; row < Rows; row++) {
                for (int col = 0; col < Columns; col++) {
                    if (this[row, col] != other[row, col]) {
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
    }
}

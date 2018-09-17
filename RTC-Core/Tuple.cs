using System;
using System.Collections.Generic;
using System.Text;

namespace RTC_Core {
    using Microsoft.VisualBasic.CompilerServices;

    public class Tuple : IEquatable<Tuple> {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }

        public Tuple(double x, double y, double z, double w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool IsPoint => W == 1.0;
        public bool IsVector => W == 0;

        public bool Equals(Tuple other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return NumericExtensions.Equals(X, other.X, 0.00001) &&
                   NumericExtensions.Equals(Y, other.Y, 0.00001) &&
                   NumericExtensions.Equals(Z, other.Z, 0.00001) &&
                   NumericExtensions.Equals(W, other.W, 0.00001);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is Tuple other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Tuple left, Tuple right) {
            return Equals(left, right);
        }

        public static bool operator !=(Tuple left, Tuple right) {
            return !Equals(left, right);
        }

        public static Tuple operator +(Tuple a, Tuple b) {
            return new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Tuple operator -(Tuple a, Tuple b) {
            return new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Tuple operator -(Tuple a) {
            return new Tuple(-a.X, -a.Y, -a.Z, -a.W);
        }

        public static Tuple operator *(Tuple a, double d) {
            return new Tuple(a.X * d, a.Y * d, a.Z * d, a.W * d);
        }

        public static Tuple operator /(Tuple a, double d) {
            return new Tuple(a.X / d, a.Y / d, a.Z / d, a.W / d);
        }

        public override string ToString() {
            return $"[{X},{Y},{Z},{W}]";
        }
    }
}

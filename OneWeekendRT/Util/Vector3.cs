namespace OneWeekendRT.Util {
    using System;

    public struct Vector3 {
        // XYZ fields
        public float X;
        public float Y;
        public float Z;

        // RGB aliases
        public float R { get { return X; } set { X = value; } }
        public float G { get { return Y; } set { Y = value; } }
        public float B { get { return Z; } set { Z = value; } }

        // Indexer for treating the vector as an array
        public float this[int i] {
            get {
                switch (i) {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        throw new IndexOutOfRangeException("Vector 3 only allows indices 0, 1, and 2");
                }
            }
            set {
                switch (i) {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Vector 3 only allows indices 0, 1, and 2");
                }
            }
        }

        // Simple constructor
        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        // Factory properties for creating common vectors
        public static Vector3 Zero { get { return new Vector3(0, 0, 0); } }
        public static Vector3 One { get { return new Vector3(1, 1, 1); } }
        public static Vector3 UnitX { get { return new Vector3(1, 0, 0); } }
        public static Vector3 UnitY { get { return new Vector3(0, 1, 0); } }
        public static Vector3 UnitZ { get { return new Vector3(0, 0, 1); } }

        // unary negation
        public static Vector3 operator -(Vector3 v) {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        // Binary addition and subtraction operators
        public static Vector3 operator +(Vector3 a, Vector3 b) {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b) {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        // Scaling operators
        public static Vector3 operator *(Vector3 a, float f) {
            return new Vector3(a.X * f, a.Y * f, a.Z * f);
        }
        public static Vector3 operator *(float f, Vector3 a) {
            return a * f;
        }
        public static Vector3 operator /(Vector3 a, float f) {
            return new Vector3(a.X / f, a.Y / f, a.Z / f);
        }
        public static Vector3 operator /(float f, Vector3 a) {
            return a * f;
        }

        // Lengths of the vector
        public float Length { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); } }
        public float LengthSquared { get { return X * X + Y * Y + Z * Z; } }


        // Normalize to a unit-vector
        public Vector3 Normalize() {
            var length = Length;
            return new Vector3(X / length, Y / length, Z / length);
        }

        // Dot product
        public float Dot(Vector3 v) { return X * v.X + Y * v.Y + Z * v.Z; }

        // Cross product
        public Vector3 Cross(Vector3 v) {
            return new Vector3(Y * v.Z - Z * v.Y, -(X * v.Z - Z * v.X), X * v.Y - Y * v.X);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
            return a*(1.0f - t) + b*t;
        }
    }
}
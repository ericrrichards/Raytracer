namespace OneWeekendRT {
    public struct Ray {
        public Vector3 A, B;

        public Vector3 Origin {get { return A; }}
        public Vector3 Direction { get { return B; } }

        public Ray(Vector3 origin, Vector3 direction) {
            A = origin;
            B = direction;
        }

        public Vector3 Evaluate(float t) { return A + B * t; }
    }
}
namespace OneWeekendRT.Util {
    public struct Ray {

        public Vector3 Origin { get; private set; }
        public Vector3 Direction { get; private set; }

        public Ray(Vector3 origin, Vector3 direction) : this() {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Evaluate(float t) { return Origin + Direction * t; }
    }
}
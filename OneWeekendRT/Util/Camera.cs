namespace OneWeekendRT.Util {
    public class Camera {
        public Vector3 Origin { get; set; }
        public Vector3 LowerLeft { get; set; }
        public Vector3 Horizontal { get; set; }
        public Vector3 Vertical { get; set; }

        public Camera() {
            LowerLeft= new Vector3(-2, -1, -1);
            Horizontal = new Vector3(4,0,0);
            Vertical = new Vector3(0, 2, 0);
            Origin = Vector3.Zero;
        }

        public Ray GetRay(float u, float v) {
            return new Ray(Origin, LowerLeft + Horizontal*u + Vertical*v - Origin);
        }
    }
}
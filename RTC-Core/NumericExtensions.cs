namespace RTC_Core {
    using System;

    public static class NumericExtensions {
        public static bool Equals(this double a, double b, double epsilon) {
            return Math.Abs(a - b) < epsilon;
        }
    }
}
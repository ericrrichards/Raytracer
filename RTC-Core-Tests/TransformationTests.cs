using System;
using System.Collections.Generic;
using System.Text;

namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class TransformationTests {
        [Test]
        public void TransformPoint() {
            var t = Matrix.Translation(5, -3, 2);
            var p = new Point(-3,4,5);
            Assert.AreEqual(new Point(2,1,7), t*p);
        }

        [Test]
        public void InverseTranslatePoint() {
            Matrix t = Matrix.Translation(5, -3, 2);
            var inv = t.Inverse();
            var p = new Point(-3, 4,5);
            Assert.AreEqual(new Point(-8, 7, 3), inv*p );
        }

        [Test]
        public void TranslationDoesntAffectVectors() {
            var t = Matrix.Translation(5, -3, 2);
            var v = new Vector(-3,4,5);
            Assert.AreEqual(v, t*v);
        }
    }
}

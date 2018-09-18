using System;
using System.Collections.Generic;
using System.Text;

namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class TransformationTests {
        [Test]
        public void TransformPoint() {
            var t = Matrix.Translate(5, -3, 2);
            var p = new Point(-3,4,5);
            Assert.AreEqual(new Point(2,1,7), t*p);
        }

        [Test]
        public void InverseTranslatePoint() {
            Matrix t = Matrix.Translate(5, -3, 2);
            var inv = t.Inverse();
            var p = new Point(-3, 4,5);
            Assert.AreEqual(new Point(-8, 7, 3), inv*p );
        }

        [Test]
        public void TranslationDoesntAffectVectors() {
            var t = Matrix.Translate(5, -3, 2);
            var v = new Vector(-3,4,5);
            Assert.AreEqual(v, t*v);
        }

        [Test]
        public void ScalePoint() {
            var t = Matrix.Scale(2, 3, 4);
            var p = new Point(-4, 6, 8);
            Assert.AreEqual(new Point(-8, 18, 32), t*p );
        }

        [Test]
        public void ScaleVector() {
            var t = Matrix.Scale(2, 3, 4);
            var v = new Vector(-4,6,8);
            Assert.AreEqual(new Vector(-8, 18, 32),t*v );
        }

        [Test]
        public void InverseScale() {
            Matrix t = Matrix.Scale(2, 3, 4);
            var inv = t.Inverse();
            var v = new Vector(-4, 6,8);
            Assert.AreEqual(new Vector(-2,2,2),inv*v );
        }

        [Test]
        public void Reflect() {
            var t = Matrix.Scale(-1, 1, 1);
            var  p =new Point(2,3,4);
            Assert.AreEqual(new Point(-2,3,4), t*p);
        }

        [Test]
        public void RotationX() {
            var p = new Point(0,1,0);
            var halfQ = Matrix.RotationX(Math.PI / 4);
            var fullQ = Matrix.RotationX(Math.PI / 2);
            Assert.AreEqual(new Point(0, Math.Sqrt(2)/2, Math.Sqrt(2)/2), halfQ * p );
            Assert.AreEqual(new Point(0,0,1), fullQ*p);
        }
        [Test]
        public void InverseRotationX() {
            var p = new Point(0,1,0);
            Matrix halfQ = Matrix.RotationX(Math.PI / 4);
            var inv = halfQ.Inverse();
            Assert.AreEqual(new Point(0,Math.Sqrt(2)/2, -Math.Sqrt(2)/2), inv*p);
        }

        [Test]
        public void RotationY() {
            var p = new Point(0, 0, 1);
            var halfQ = Matrix.RotationY(Math.PI / 4);
            var fullQ = Matrix.RotationY(Math.PI / 2);
            Assert.AreEqual(new Point(Math.Sqrt(2)/2, 0, Math.Sqrt(2)/2), halfQ*p );
            Assert.AreEqual(new Point(1,0,0), fullQ*p);
        }
        [Test]
        public void RotationZ() {
            var p = new Point(0, 1, 0);
            var halfQ = Matrix.RotationZ(Math.PI / 4);
            var fullQ = Matrix.RotationZ(Math.PI / 2);
            Assert.AreEqual(new Point(-Math.Sqrt(2)/2, Math.Sqrt(2)/2,0), halfQ*p );
            Assert.AreEqual(new Point(-1,0,0), fullQ*p);
        }

        [Test]
        public void ShearXInProportionToY() {
            var t = Matrix.Shear(1, 0, 0, 0, 0, 0);
            var p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(5,3,4), t*p);
        }
        [Test]
        public void ShearXInProportionToZ() {
            var t = Matrix.Shear(0, 1, 0, 0, 0, 0);
            var p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(6,3,4), t*p);
        }
        [Test]
        public void ShearYInProportionToX() {
            var t = Matrix.Shear(0, 0, 1, 0, 0, 0);
            var p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2,5,4), t*p);
        }
        [Test]
        public void ShearYInProportionToZ() {
            var t = Matrix.Shear(0, 0, 0, 1, 0, 0);
            var p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2,7,4), t*p);
        }
        [Test]
        public void ShearZInProportionToX() {
            var t = Matrix.Shear(0, 0, 0, 0, 1, 0);
            var p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2,3,6), t*p);
        }
        [Test]
        public void ShearZInProportionToY() {
            var t = Matrix.Shear(0, 0, 0, 0, 0, 1);
            var p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(2,3,7), t*p);
        }

        [Test]
        public void ApplyTransformsInOrder() {
            var p = new Point(1, 0, 1);
            var a = Matrix.RotationX(Math.PI / 2);
            var b = Matrix.Scale(5, 5, 5);
            var c = Matrix.Translate(10, 5, 7);

            var p2 = a * p;
            Assert.AreEqual(new Point(1,-1,0), p2 );

            var p3 = b * p2;
            Assert.AreEqual(new Point(5,-5, 0), p3);

            var p4 = c * p3;
            Assert.AreEqual(new Point(15,0,7), p4 );
        }

        [Test]
        public void ChainedTransformsMustBeAppliedInOrder() {
            var p = new Point(1, 0, 1);
            var a = Matrix.RotationX(Math.PI / 2);
            var b = Matrix.Scale(5, 5, 5);
            var c = Matrix.Translate(10, 5, 7);
            var t = c * b * a;
            Assert.AreEqual(new Point(15,0,7), t*p);
        }
    }
}

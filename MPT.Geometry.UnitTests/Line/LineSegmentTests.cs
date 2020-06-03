using System;
using NUnit.Framework;

using MPT.Geometry.Line;
using MPT.Math.Coordinates;
using MPT.Math.Vectors;
using MPT.Math;

namespace MPT.Geometry.UnitTests.Line
{
    [TestFixture]
    public static class LineSegmentTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization_without_Coordinates_Results_in_Empty_Object()
        {
            LineSegment lineSegment = new LineSegment();

            Assert.AreEqual(GeometryLibrary.ZeroTolerance, lineSegment.Tolerance);
            Assert.AreEqual(0, lineSegment.I.X);
            Assert.AreEqual(0, lineSegment.I.Y);
            Assert.AreEqual(0, lineSegment.J.X);
            Assert.AreEqual(0, lineSegment.J.Y);
        }

        [Test]
        public static void Initialization_with_Coordinates_Results_in_Object_with_Immutable_Coordinates_Properties_List()
        {
            LineSegment lineSegment = new LineSegment(new CartesianCoordinate(3, 4), new CartesianCoordinate(5, 6));

            Assert.AreEqual(GeometryLibrary.ZeroTolerance, lineSegment.Tolerance);
            Assert.AreEqual(3, lineSegment.I.X);
            Assert.AreEqual(4, lineSegment.I.Y);
            Assert.AreEqual(5, lineSegment.J.X);
            Assert.AreEqual(6, lineSegment.J.Y);
        }
        #endregion

        #region Methods: Override (IPathSegment)
        #endregion

        #region Methods: Public

        //public bool IsHorizontal()
        //{
        //    return IsHorizontal(I, J, Tolerance);
        //}


        //public bool IsVertical()
        //{
        //    return IsVertical(I, J, Tolerance);
        //}


        //public double Slope()
        //{
        //    return Slope(I, J, Tolerance);
        //}


        //public double InterceptX()
        //{
        //    return InterceptX(I, J, Tolerance);
        //}


        //public double InterceptY()
        //{
        //    return InterceptY(I, J, Tolerance);
        //}


        //public bool IsParallel(LineSegment otherLine)
        //{
        //    return (Slope() - otherLine.Slope()).IsZeroSign(Tolerance);
        //}


        //public bool IsPerpendicular(LineSegment otherLine)
        //{
        //    return (Slope() * otherLine.Slope()).IsEqualTo(-1, Tolerance);
        //}


        //public bool IsProjectionIntersecting(CartesianCoordinate point)
        //{
        //    return (point.Y.IsEqualTo(InterceptY() + Slope() * point.X));
        //}


        //public bool IsProjectionIntersecting(LineSegment otherLine)
        //{
        //    return !IsParallel(otherLine);
        //}

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 3, 4)]
        [TestCase(2, 3, 0, 0)]
        [TestCase(-2, -3, 0, 0)]
        [TestCase(2, 3, 2, 3)]
        [TestCase(-2, -3, 2, 3)]
        [TestCase(2, 3, -2, 3)]
        [TestCase(-2, -3, 2, -3)]
        [TestCase(-2.1, -3.4, 2.8, -3.2)]
        public static void ToVector_Returns_Segment_as_Vector(double x1, double y1, double x2, double y2)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Vector vector = segment.ToVector();

            double magnitudeX = x2 - x1;
            double magnitudeY = y2 - y1;

            Assert.AreEqual(x1, vector.Location.X);
            Assert.AreEqual(y1, vector.Location.Y);
            Assert.AreEqual(magnitudeX, vector.Xcomponent);
            Assert.AreEqual(magnitudeY, vector.Ycomponent);
            Assert.AreEqual(Numbers.ZeroTolerance, vector.Tolerance);
        }
        #endregion

        #region Methods: Static

        #region Alignment
        // TODO: Tests with tolerance

        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(5.6789d, 5.6789d, ExpectedResult = true)]
        [TestCase(5.6789d, -5.6789d, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, ExpectedResult = true)]
        public static bool IsParallel(double slope1, double slope2)
        {
            return LineSegment.IsParallel(slope1, slope2);
        }

        [TestCase(1, -1, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = false)]
        [TestCase(2, -1 / 2d, ExpectedResult = true)]
        [TestCase(5.6789d, -1 / 5.6789d, ExpectedResult = true)]
        [TestCase(5.6789d, 5.6789d, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, 0, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, 0, ExpectedResult = true)]
        [TestCase(0, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(0, double.PositiveInfinity, ExpectedResult = true)]
        public static bool IsPerpendicular(double slope1, double slope2)
        {
            return LineSegment.IsPerpendicular(slope1, slope2);
        }
        #endregion

        #region Slope
        [TestCase(1, 2, ExpectedResult = 1 / 2d)]
        [TestCase(-1, 2, ExpectedResult = -1 / 2d)]
        [TestCase(-1, -2, ExpectedResult = 1 / 2d)]
        [TestCase(1, -2, ExpectedResult = -1 / 2d)]
        [TestCase(0, -2, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = double.PositiveInfinity)]
        [TestCase(-1, 0, ExpectedResult = double.NegativeInfinity)]
        public static double Slope(double rise, double run)
        {
            return LineSegment.Slope(rise, run);
        }

        [Test]
        public static void Slope_of_Point_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => LineSegment.Slope(rise: 0, run: 0));
        }

        [TestCase(2, 1, 4, 2, ExpectedResult = 1 / 2d)]
        [TestCase(2, 2, 4, 1, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 1, ExpectedResult = 1 / 2d)]
        [TestCase(4, 1, 2, 2, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 2, ExpectedResult = 0)]
        [TestCase(4, 1, 4, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(4, 2, 4, 1, ExpectedResult = double.NegativeInfinity)]
        public static double Slope_of_Coordinate_Input(double x1, double y1, double x2, double y2)
        {
            return LineSegment.Slope(x1, y1, x2, y2);
        }

        [TestCase(2, 1, 4, 2, ExpectedResult = 1 / 2d)]
        [TestCase(2, 2, 4, 1, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 1, ExpectedResult = 1 / 2d)]
        [TestCase(4, 1, 2, 2, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 2, ExpectedResult = 0)]
        [TestCase(4, 1, 4, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(4, 2, 4, 1, ExpectedResult = double.NegativeInfinity)]
        public static double Slope_of_Point_Input(double x1, double y1, double x2, double y2)
        {
            CartesianCoordinate point1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate point2 = new CartesianCoordinate(x2, y2);
            return LineSegment.Slope(point1, point2);
        }
        #endregion

        #region Intercept
        #endregion

        #region Intersect

        #endregion

        #endregion
    }
}

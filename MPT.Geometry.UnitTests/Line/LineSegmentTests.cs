using System;
using NUnit.Framework;

using MPT.Geometry.Segments;
using MPT.Math.Coordinates;
using MPT.Math.Vectors;

namespace MPT.Geometry.UnitTests.Line
{
    [TestFixture]
    public static class LineSegmentTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization_with_Coordinates_Results_in_Object_with_Immutable_Coordinates_Properties_List()
        {
            LineSegment lineSegment = new LineSegment(new CartesianCoordinate(3, 4), new CartesianCoordinate(5, 6));

            Assert.AreEqual(3, lineSegment.I.X);
            Assert.AreEqual(4, lineSegment.I.Y);
            Assert.AreEqual(5, lineSegment.J.X);
            Assert.AreEqual(6, lineSegment.J.Y);
            Assert.AreEqual(3, lineSegment.Curve.ControlPointI.X);
            Assert.AreEqual(4, lineSegment.Curve.ControlPointI.Y);
            Assert.AreEqual(5, lineSegment.Curve.ControlPointJ.X);
            Assert.AreEqual(6, lineSegment.Curve.ControlPointJ.Y);
        }
        #endregion

        #region Methods: Override (IPathSegment)
        [TestCase(1, 1, 2, 1, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.828427)] // + slope
        [TestCase(-1, -2, -3, -4, 2.828427)] // - slope
        [TestCase(1, 2, -3, -4, 7.2111103)] // - slope
        public static void Length(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, segment.Length(), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1.5)] // Horizontal
        [TestCase(1, 1, 1, 2, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2)] // + slope
        [TestCase(-1, -2, -3, -4, -2)] // - slope
        [TestCase(1, 2, -3, -4, -1)] // - slope
        public static void Xo(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, segment.Xo(), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1.5)] // Vertical
        [TestCase(1, 2, 3, 4, 3)] // + slope
        [TestCase(-1, -2, -3, -4, -3)] // - slope
        [TestCase(1, 2, -3, -4, -1)] // - slope
        public static void Yo(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, segment.Yo(), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1, double.PositiveInfinity)] // Horizontal, on line
        [TestCase(1, 1, 2, 1, 2, double.PositiveInfinity)] // Horizontal, off line
        [TestCase(1, 1, 1, 2, 1.25, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.5, 1.5)] // + slope
        [TestCase(-1, -2, -3, -4, -2.5, -1.5)] // - slope
        [TestCase(1, 2, -3, -4, 0.2, -0.2)] // - slope
        public static void X(double x1, double y1, double x2, double y2, double y, double expectedResult)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, segment.X(y), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1.25, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1, double.PositiveInfinity)] // Vertical, on line
        [TestCase(1, 1, 1, 2, 2, double.PositiveInfinity)] // Vertical, off line
        [TestCase(1, 2, 3, 4, 1.5, 2.5)] // + slope
        [TestCase(-1, -2, -3, -4, -1.5, -2.5)] // - slope
        [TestCase(1, 2, -3, -4, -0.2, 0.2)] // - slope
        public static void Y(double x1, double y1, double x2, double y2, double x, double expectedResult)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, segment.Y(x), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 0.25, 1.25, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 0.25, 1, 1.25)] // Vertical
        [TestCase(1, 2, 3, 4, 0.25, 1.5, 2.5)] // + slope
        [TestCase(-1, -2, -3, -4, 0.25, -1.5, -2.5)] // - slope
        [TestCase(1, 2, -3, -4, 0.3, -0.2, 0.2)] // - slope
        public static void PointByPathPosition(double x1, double y1, double x2, double y2, double sRelative, double expectedResultX, double expectedResultY)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            CartesianCoordinate coordinate = segment.PointByPathPosition(sRelative);

            Assert.AreEqual(expectedResultX, coordinate.X, Tolerance);
            Assert.AreEqual(expectedResultY, coordinate.Y, Tolerance);
        }

        [TestCase(-1)]
        [TestCase(2)]
        public static void PointByPathPosition_Throws_ArgumentOutOfRangeException_if_sRelative_is_Out_of_Range(double sRelative)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4));

            Assert.Throws<ArgumentOutOfRangeException>(() => { segment.PointByPathPosition(sRelative); });
        }

        [TestCase(1, 1, 2, 1, 0.25, 0, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 0.25, -1, 0)] // Vertical
        [TestCase(1, 2, 3, 4, 0.25, -0.707107, 0.707107)] // + slope
        [TestCase(3, 4, 1, 2, 0.25, 0.707107, -0.707107)] // - slope by reversing + slope coordinates
        [TestCase(-1, -2, -3, -4, 0.25, 0.707107, -0.707107)] // - slope
        [TestCase(1, 2, -3, -4, 0.3, 0.832050, -0.554700)] // - slope
        [TestCase(1, 2, -3, -4, -1, 0.832050, -0.554700)] // sRelative less than 0
        [TestCase(1, 2, -3, -4, 2, 0.832050, -0.554700)] // sRelative greater than 1
        public static void NormalVector(double x1, double y1, double x2, double y2, double sRelative, double expectedXMagnitude, double expectedYMagnitude)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            Vector vector = segment.NormalVector(sRelative);

            Assert.AreEqual(expectedXMagnitude, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedYMagnitude, vector.Ycomponent, Tolerance);
        }

        [TestCase(1, 1, 2, 1, 0.25, 1, 0)] // Horizontal
        [TestCase(1, 1, 1, 2, 0.25, 0, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 0.25, 0.707107, 0.707107)] // + slope
        [TestCase(3, 4, 1, 2, 0.25, -0.707107, -0.707107)] // - slope by reversing + slope coordinates
        [TestCase(-1, -2, -3, -4, 0.25, -0.707107, -0.707107)] // - slope
        [TestCase(1, 2, -3, -4, 0.3, -0.554700, -0.832050)] // - slope
        [TestCase(1, 2, -3, -4, -1, -0.554700, -0.832050)] // sRelative less than 0
        [TestCase(1, 2, -3, -4, 2, -0.554700, -0.832050)] // sRelative greater than 1
        public static void TangentVector(double x1, double y1, double x2, double y2, double sRelative, double expectedXMagnitude, double expectedYMagnitude)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            Vector vector = segment.TangentVector(sRelative);

            Assert.AreEqual(expectedXMagnitude, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedYMagnitude, vector.Ycomponent, Tolerance);
        }
        #endregion

        #region Methods: Override (IPathSegmentCollision)
        [TestCase(1, 2, 3, 4, -6, 6, false)]  // Not on line
        [TestCase(1, 1, 3, 1, 2, 1, true)]  // Horizontal, within points
        [TestCase(1, 2, 1, 4, 1, 3, true)]  // Vertical, within points
        [TestCase(1, 2, 3, 4, 2, 3, true)]  // Sloped, within points
        [TestCase(1.2, 3.4, 6.7, 9.1, 3.95, 6.25, true)]  // Sloped, within points
        [TestCase(1, 1, 3, 1, 1, 1, true)]  // Horizontal, on i
        [TestCase(1, 2, 1, 4, 1, 2, true)]  // Vertical, on i
        [TestCase(1, 2, 3, 4, 1, 2, true)]  // Sloped, on i
        [TestCase(1.2, 3.4, 6.7, 9.1, 1.2, 3.4, true)]  // on i
        [TestCase(1, 1, 3, 1, 3, 1, true)]  // Horizontal, on j
        [TestCase(1, 2, 1, 4, 1, 4, true)]  // Vertical, on j
        [TestCase(1, 2, 3, 4, 3, 4, true)]  // Sloped, on j
        [TestCase(1.2, 3.4, 6.7, 9.1, 6.7, 9.1, true)]  // Sloped, on j
        [TestCase(1, 1, 3, 1, -1, 1, false)]  // Horizontal, before i
        [TestCase(1, 2, 1, 4, 1, 1, false)]  // Vertical, before i
        [TestCase(1, 2, 3, 4, -2, -1, false)]  // Sloped, before i
        [TestCase(1.2, 3.4, 6.7, 9.1, 0, 2.15636, false)]  // Sloped, before i
        [TestCase(1, 1, 3, 1, 4, 1, false)]  // Horizontal, after j
        [TestCase(1, 2, 1, 4, 1, 5, false)]  // Vertical, after j
        [TestCase(1, 2, 3, 4, 4, 5, false)]  // Sloped, after j
        [TestCase(1.2, 3.4, 6.7, 9.1, 9.45, 11.95, false)]  // Sloped, after j
        public static void IncludesCoordinate(
            double x1, double y1, double x2, double y2,
            double pointX, double pointY, bool expectedResult)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            CartesianCoordinate coordinate = new CartesianCoordinate(pointX, pointY);

            Assert.AreEqual(expectedResult, segment.IncludesCoordinate(coordinate));
        }

        [TestCase(-5, 6, -3, -2, 1, 5, -7, 3, true)] // sloped perpendicular, within points
        [TestCase(-5, 2, -5, 8, -5, 6, 5, 6, true)] // aligned perpendicular + vertical, within points
        [TestCase(-5, 8, -5, 2, -5, 6, 5, 6, true)] // aligned perpendicular - vertical, within points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, true)] // sloped perpendicular, on i-i points
        [TestCase(-5, 6, -3, -2, -1, 7, -5, 6, true)] // sloped perpendicular, on i-j points
        [TestCase(-5, 6, -3, -2, -3, -2, 1, -1, true)] // sloped perpendicular, on j-i points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, true)] // sloped perpendicular, on j-j points
        [TestCase(-5, 6, -3, -2, 5, 6, 1, 5, false)] // sloped perpendicular, before segment1 i
        [TestCase(-5, 8, -5, 11, -5, 6, 5, 6, false)] // aligned perpendicular + vertical, before segment1 i
        [TestCase(-5, 11, -5, 8, -5, 6, 5, 6, false)] // aligned perpendicular - vertical, before segment1 i
        [TestCase(-5, 6, -3, -2, -7, 3, -11, 2, false)] // sloped perpendicular, after segment1 j
        [TestCase(-5, -2, -5, 2, -5, 6, 5, 6, false)] // aligned perpendicular + vertical, after segment1 j
        [TestCase(-5, 2, -5, -2, -5, 6, 5, 6, false)] // aligned perpendicular - vertical, after segment1 j
        [TestCase(1, 2, 3, 4, 2, 3, 4, 5, false)] // + slope parallel
        [TestCase(-5, 6, -3, -1, 5, 6, 7, -1, false)] // - slope parallel
        [TestCase(-1, 2, 1, 2, -2, 3, 2, 3, false)] // horizontal parallel
        [TestCase(-5, -2, -5, 2, 5, 6, 5, 8, false)] // + vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 8, 5, 6, false)] // - vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 6, 5, 8, false)] // +/- vertical parallel
        public static void IsIntersecting(
            double xi1, double yi1, double xj1, double yj1,
            double xi2, double yi2, double xj2, double yj2, bool expectedResult)
        {
            LineSegment segment1 = new LineSegment(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LineSegment segment2 = new LineSegment(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            Assert.AreEqual(expectedResult, segment1.IsIntersecting(segment2));
        }

        [TestCase(-5, 6, -3, -2, 1, 5, -7, 3, -4.411765, 3.647059)] // sloped perpendicular, within points
        [TestCase(-5, 2, -5, 8, -5, 6, 5, 6, -5, 6)] // aligned perpendicular + vertical, within points
        [TestCase(-5, 8, -5, 2, -5, 6, 5, 6, -5, 6)] // aligned perpendicular - vertical, within points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, -5, 6)] // sloped perpendicular, on i-i points
        [TestCase(-5, 6, -3, -2, -1, 7, -5, 6, -5, 6)] // sloped perpendicular, on i-j points
        [TestCase(-5, 6, -3, -2, -3, -2, 1, -1, -3, -2)] // sloped perpendicular, on j-i points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, -5, 6)] // sloped perpendicular, on j-j points
        [TestCase(-5, 2, -5, 8, -5, 2, 1, 2, -5, 2)] // aligned perpendicular + vertical, on i-i points
        [TestCase(-5, 2, -5, 8, -5, 2, 1, 2, -5, 2)] // aligned perpendicular + vertical, on j-j points
        public static void IntersectionCoordinate(
            double xi1, double yi1, double xj1, double yj1,
            double xi2, double yi2, double xj2, double yj2, double expectedX, double expectedY)
        {
            LineSegment segment1 = new LineSegment(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LineSegment segment2 = new LineSegment(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            CartesianCoordinate coordinateResult = segment1.IntersectionCoordinate(segment2);

            Assert.AreEqual(expectedX, coordinateResult.X, Tolerance);
            Assert.AreEqual(expectedY, coordinateResult.Y, Tolerance);
        }


        [TestCase(-5, 6, -3, -2, 1, 5, 5, 6)] //, -4.411765, 3.647059)] // sloped perpendicular, outside points
        [TestCase(-5, 6, -3, -2, 5.2, 6.1, 7.7, -1.3)] // slope, within points
        [TestCase(1, 2, 3, 4, 2, 3, 4, 5)] // + slope parallel
        [TestCase(-5, 6, -3, -1, 5, 6, 7, -1)] // - slope parallel
        [TestCase(-1, 2, 1, 2, -2, 3, 2, 3)] // horizontal parallel
        [TestCase(-5, -2, -5, 2, 5, 6, 5, 8)] // + vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 8, 5, 6)] // - vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 6, 5, 8)] // +/- vertical parallel
        public static void IntersectionCoordinate_Throws_ArgumentOutOfRangeException_if_Segments_Do_Not_Intersect(
            double xi1, double yi1, double xj1, double yj1,
            double xi2, double yi2, double xj2, double yj2)
        {
            LineSegment segment1 = new LineSegment(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LineSegment segment2 = new LineSegment(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            Assert.Throws<ArgumentOutOfRangeException>(() => { segment1.IntersectionCoordinate(segment2); });
        }
        #endregion

        #region Methods (IPathDivision)
        [TestCase(-3, 4, 5, 6, 0, -3, 4)]
        [TestCase(-3, 4, 5, 6, 0.5, 1, 5)]
        [TestCase(-3, 4, 5, 6, 1, 5, 6)]
        public static void PointDivision(
            double x1, double y1, double x2, double y2, 
            double fraction, double expectedX, double expectedY)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            CartesianCoordinate coordinateResult = segment.PointDivision(fraction);

            Assert.AreEqual(expectedX, coordinateResult.X, Tolerance);
            Assert.AreEqual(expectedY, coordinateResult.Y, Tolerance);
        }

        [Test]
        public static void PointDivision_Throws_ArgumentOutOfRangeException_for_Ratio_Less_Than_0()
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4));

            Assert.Throws<ArgumentOutOfRangeException>(() => { segment.PointDivision(-0.1); });
        }

        [Test]
        public static void PointDivision_Throws_ArgumentOutOfRangeException_for_Ratio_Greater_Than_1()
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4));

            Assert.Throws<ArgumentOutOfRangeException>(() => { segment.PointDivision(1.1); });
        }

        [TestCase(-3, 4, 5, 6, -1.5, -15, 1)]
        [TestCase(-3, 4, 5, 6, -0.5, -7, 3)]
        [TestCase(-3, 4, 5, 6, 0, 5, 6)]
        [TestCase(-3, 4, 5, 6, 0.5, 9, 7)]
        [TestCase(-3, 4, 5, 6, 1, 13, 8)]
        [TestCase(-3, 4, 5, 6, 1.5, 17, 9)]
        public static void PointExtension(
            double x1, double y1, double x2, double y2,
            double ratio, double expectedX, double expectedY)
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            CartesianCoordinate coordinateResult = segment.PointExtension(ratio);

            Assert.AreEqual(expectedX, coordinateResult.X, Tolerance);
            Assert.AreEqual(expectedY, coordinateResult.Y, Tolerance);
        }
        #endregion

        #region Methods: Public
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
        }
        #endregion
    }
}

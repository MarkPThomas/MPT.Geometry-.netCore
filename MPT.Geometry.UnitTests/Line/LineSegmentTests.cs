using System;
using NUnit.Framework;

using MPT.Geometry.Line;
using MPT.Math.Coordinates;

namespace MPT.Geometry.UnitTests.Line
{
    [TestFixture]
    public class LineSegmentTests
    {
        [TestCase(1, 2, ExpectedResult = 1 / 2d)]
        [TestCase(-1, 2, ExpectedResult = -1 / 2d)]
        [TestCase(-1, -2, ExpectedResult = 1 / 2d)]
        [TestCase(1, -2, ExpectedResult = -1 / 2d)]
        [TestCase(0, -2, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = double.PositiveInfinity)]
        [TestCase(-1, 0, ExpectedResult = double.NegativeInfinity)]
        public double Slope(double rise, double run)
        {
            return LineSegment.Slope(rise, run);
        }

        [Test]
        public void Slope_of_Point_Throws_Argument_Exception()
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
        public double Slope_of_Coordinate_Input(double x1, double y1, double x2, double y2)
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
        public double Slope_of_Point_Input(double x1, double y1, double x2, double y2)
        {
            CartesianCoordinate point1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate point2 = new CartesianCoordinate(x2, y2);
            return LineSegment.Slope(point1, point2);
        }

        // TODO: Tests with tolerance

        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(5.6789d, 5.6789d, ExpectedResult = true)]
        [TestCase(5.6789d, -5.6789d, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, ExpectedResult = true)]
        public bool IsParallel(double slope1, double slope2)
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
        public bool IsPerpendicular(double slope1, double slope2)
        {
            return LineSegment.IsPerpendicular(slope1, slope2);
        }
    }
}

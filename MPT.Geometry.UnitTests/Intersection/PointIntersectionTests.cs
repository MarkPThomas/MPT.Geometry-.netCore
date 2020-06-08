using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPT.Geometry.Intersections;
using MPT.Math;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Intersection
{
    [TestFixture]
    public class PointIntersectionTests
    {

        [TestCase(0.9, 2, 1, 2, ExpectedResult = false)] // Left
        [TestCase(1.1, 2, 1, 2, ExpectedResult = false)] // Right
        [TestCase(1, 2.1, 1, 2, ExpectedResult = false)] // Above
        [TestCase(1, 1.9, 1, 2, ExpectedResult = false)] // Below
        [TestCase(1, 2, 1, 2, ExpectedResult = true)] // On
        [TestCase(-0.9, -2, -1, -2, ExpectedResult = false)] // Left
        [TestCase(-1.1, -2, -1, -2, ExpectedResult = false)] // Right
        [TestCase(-1, -2.1, -1, -2, ExpectedResult = false)] // Above
        [TestCase(-1, -1.9, -1, -2, ExpectedResult = false)] // Below
        [TestCase(-1, -2, -1, -2, ExpectedResult = true)] // On
        [TestCase(-0.9, 2, -1, 2, ExpectedResult = false)] // Left
        [TestCase(-1.1, 2, -1, 2, ExpectedResult = false)] // Right
        [TestCase(-1, 2.1, -1, 2, ExpectedResult = false)] // Above
        [TestCase(-1, 1.9, -1, 2, ExpectedResult = false)] // Below
        [TestCase(-1, 2, -1, 2, ExpectedResult = true)] // On
        [TestCase(0.9, -2, 1, -2, ExpectedResult = false)] // Left
        [TestCase(1.1, -2, 1, -2, ExpectedResult = false)] // Right
        [TestCase(1, -2.1, 1, -2, ExpectedResult = false)] // Above
        [TestCase(1, -1.9, 1, -2, ExpectedResult = false)] // Below
        [TestCase(1, -2, 1, -2, ExpectedResult = true)] // On
        public bool PointsOverlap(double x, double y, double x1, double y1)
        {
            CartesianCoordinate point = new CartesianCoordinate(x1, y1);
            return PointIntersection.IsOnPoint(point, new CartesianCoordinate(x, y));
        }

        private List<CartesianCoordinate> polyline = new List<CartesianCoordinate>()
                                        {
                                            new CartesianCoordinate(-5, 5),
                                            new CartesianCoordinate(4, 5),
                                            new CartesianCoordinate(6, -5),
                                            new CartesianCoordinate(-5, -5),
                                        };

        private List<CartesianCoordinate> square = new List<CartesianCoordinate>()
                                        {
                                            new CartesianCoordinate(-5, 5),
                                            new CartesianCoordinate(4, 5),
                                            new CartesianCoordinate(6, -5),
                                            new CartesianCoordinate(-5, -5),
                                            new CartesianCoordinate(-5, 5),
                                        };

        private List<CartesianCoordinate> comb = new List<CartesianCoordinate>()
                                        {
                                            new CartesianCoordinate(-5, 5),
                                            new CartesianCoordinate(5, 5),
                                            new CartesianCoordinate(5, -5),
                                            new CartesianCoordinate(2, -5),
                                            new CartesianCoordinate(2, 2),
                                            new CartesianCoordinate(-2, 2),
                                            new CartesianCoordinate(-2, -5),
                                            new CartesianCoordinate(-5, -5),
                                            new CartesianCoordinate(-5, 5),
                                        };

        [Test]
        public void IsWithinShape_Of_Polyline_Throws_Argument_Exception()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(1, 1);
            Assert.That(() => PointIntersection.IsWithinShape(coordinate, polyline.ToArray()),
                                Throws.Exception
                                    .TypeOf<ArgumentException>());
        }


        //[TestCase(-6, ExpectedResult = false)]
        //[TestCase(-5, ExpectedResult = true)]
        //[TestCase(0, ExpectedResult = true)]
        //[TestCase(4.8, ExpectedResult = true)] // Intercept with right sloping line segment
        //[TestCase(6, ExpectedResult = false)]
        //public bool IsWithinShape_Between_Top_And_Bottom_of_Square(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 1);
        //    return PointIntersection.IsWithinShape(coordinate, square.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = false)]
        //[TestCase(-5, ExpectedResult = true)]
        //[TestCase(0, ExpectedResult = true)]
        //[TestCase(4, ExpectedResult = true)]
        //[TestCase(6, ExpectedResult = false)]
        //public bool IsWithinShape_Aligned_With_Top_of_Square(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 5);
        //    return PointIntersection.IsWithinShape(coordinate, square.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = false)]
        //[TestCase(-5, ExpectedResult = false)]
        //[TestCase(0, ExpectedResult = false)]
        //[TestCase(3.8, ExpectedResult = false)] // Intercept with right sloping line
        //[TestCase(6, ExpectedResult = false)]
        //public bool IsWithinShape_Above_Square(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 6);
        //    return PointIntersection.IsWithinShape(coordinate, square.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = false)]
        //[TestCase(-5, ExpectedResult = true)]
        //[TestCase(-4, ExpectedResult = true)]
        //[TestCase(-2, ExpectedResult = true)]
        //[TestCase(0, ExpectedResult = false)]
        //[TestCase(2, ExpectedResult = true)]
        //[TestCase(4, ExpectedResult = true)]
        //[TestCase(5, ExpectedResult = true)]
        //[TestCase(6, ExpectedResult = false)]
        //public bool IsWithinShape_Intersection_Multiple_Solid_Void(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 1);
        //    return PointIntersection.IsWithinShape(coordinate, comb.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = false)]
        //[TestCase(-5, ExpectedResult = true)]
        //[TestCase(-4, ExpectedResult = true)]
        //[TestCase(0, ExpectedResult = false)]
        //[TestCase(4, ExpectedResult = true)]
        //[TestCase(5, ExpectedResult = true)]
        //[TestCase(6, ExpectedResult = false)]
        //public bool IsWithinShape_Intersection_Multiple_Solid_Void_On_Tooth_Segment(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, -5);
        //    return PointIntersection.IsWithinShape(coordinate, comb.ToArray());
        //}
    }
}

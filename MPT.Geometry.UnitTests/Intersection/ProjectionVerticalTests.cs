using System;
using System.Collections.Generic;
using MPT.Geometry.Intersection;
using MPT.Math;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Intersection
{
    [TestFixture]
    public class ProjectionVerticalTests
    {
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
                                            new CartesianCoordinate(-5, -5),
                                            new CartesianCoordinate(-5, -2),
                                            new CartesianCoordinate(-2, -2),
                                            new CartesianCoordinate(-2, 2),
                                            new CartesianCoordinate(-5, 2),
                                            new CartesianCoordinate(-5, 5),
                                        };

        [Test]
        public void NumberOfIntersections_Of_Polyline_Throws_Argument_Exception()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(1, 1);
            Assert.That(() => ProjectionVertical.NumberOfIntersections(coordinate, polyline.ToArray()),
                                Throws.Exception
                                    .TypeOf<ArgumentException>());
        }

        // TODO: Fix these failing tests
        //[TestCase(-6, ExpectedResult = 2)]
        //[TestCase(-5, ExpectedResult = 1)] // On bottom edge
        //[TestCase(0, ExpectedResult = 1)]
        //[TestCase(4.8, ExpectedResult = 1)] // On top edge
        //[TestCase(6, ExpectedResult = 0)]
        //public int NumberOfIntersections_Between_Left_And_Right_of_Square(double y)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(1, y);
        //    return ProjectionVertical.NumberOfIntersections(coordinate, square.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 2)]
        //[TestCase(-5, ExpectedResult = 1)]  // On bottom vertex
        //[TestCase(0, ExpectedResult = 1)]   // On left segment
        //[TestCase(4, ExpectedResult = 1)]   // On top vertex
        //[TestCase(6, ExpectedResult = 0)]
        //public int NumberOfIntersections_Aligned_With_Left_of_Square(double y)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(5, y);
        //    return ProjectionVertical.NumberOfIntersections(coordinate, square.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 0)]
        //[TestCase(-5, ExpectedResult = 0)]
        //[TestCase(0, ExpectedResult = 0)]
        //[TestCase(3.8, ExpectedResult = 0)]
        //[TestCase(6, ExpectedResult = 0)]
        //public int NumberOfIntersections_Left_Of_Square(double y)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(6, y);
        //    return ProjectionVertical.NumberOfIntersections(coordinate, square.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 4)]
        //[TestCase(-5, ExpectedResult = 1)]  // On bottom vertical segment
        //[TestCase(-4, ExpectedResult = 3)]
        //[TestCase(-2, ExpectedResult = 1)]  // On bottom vertical segment of center gap
        //[TestCase(0, ExpectedResult = 2)]   // In center gap
        //[TestCase(2, ExpectedResult = 1)]   // On top vertical segment of center gap
        //[TestCase(4, ExpectedResult = 1)]
        //[TestCase(5, ExpectedResult = 1)]   // On top vertical segment
        //[TestCase(6, ExpectedResult = 0)]
        //public int NumberOfIntersections_Intersection_Multiple_Solid_Void(double y)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(1, y);
        //    return ProjectionVertical.NumberOfIntersections(coordinate, comb.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 4)]
        //[TestCase(-5, ExpectedResult = 1)]  // On bottom vertex
        //[TestCase(-4, ExpectedResult = 1)]  // On bottom left segment
        //[TestCase(0, ExpectedResult = 2)]   // In center gap
        //[TestCase(4, ExpectedResult = 1)]   // On top right segment
        //[TestCase(5, ExpectedResult = 1)]   // On top vertex
        //[TestCase(6, ExpectedResult = 0)]
        //public int NumberOfIntersections_Intersection_Multiple_Solid_Void_On_Tooth_Segment(double y)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(-5, y);
        //    return ProjectionVertical.NumberOfIntersections(coordinate, comb.ToArray());
        //}

        [TestCase(9.9, 10, ExpectedResult = false)]
        [TestCase(10, 10, ExpectedResult = true)]
        [TestCase(10.1, 10, ExpectedResult = false)]
        [TestCase(-0.1, 0, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(0.1, 0, ExpectedResult = false)]
        [TestCase(-9.9, -10, ExpectedResult = false)]
        [TestCase(-10, -10, ExpectedResult = true)]
        [TestCase(-10.1, -10, ExpectedResult = false)]
        public bool PointIsWithinSegmentWidth_Vertical(double xPtN, double xLeftEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(xLeftEnd, 1);
            CartesianCoordinate ptJ = new CartesianCoordinate(xLeftEnd, 15);

            return ProjectionVertical.PointIsWithinSegmentWidth(xPtN, ptI, ptJ);
        }

        [TestCase(9.9, 10, 10.2, ExpectedResult = false)]
        [TestCase(10, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.1, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.2, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.3, 10, 10.2, ExpectedResult = false)]
        [TestCase(-9.9, -10, -10.2, ExpectedResult = false)]
        [TestCase(-10, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.1, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.2, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.3, -10, -10.2, ExpectedResult = false)]
        public bool PointIsWithinSegmentWidth_Horizontal(double xPtN, double xLeftEnd, double xRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(xLeftEnd, 1);
            CartesianCoordinate ptJ = new CartesianCoordinate(xRightEnd, 1);

            return ProjectionVertical.PointIsWithinSegmentWidth(xPtN, ptI, ptJ);
        }

        [TestCase(9.9, 10, 10.2, ExpectedResult = false)]
        [TestCase(10, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.1, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.2, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.3, 10, 10.2, ExpectedResult = false)]
        [TestCase(-9.9, -10, -10.2, ExpectedResult = false)]
        [TestCase(-10, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.1, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.2, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.3, -10, -10.2, ExpectedResult = false)]
        public bool PointIsWithinSegmentWidth_Sloped(double xPtN, double xLeftEnd, double xRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(xLeftEnd, 1);
            CartesianCoordinate ptJ = new CartesianCoordinate(xRightEnd, 15);

            return ProjectionVertical.PointIsWithinSegmentWidth(xPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, 15, ExpectedResult = true)]
        [TestCase(-2, 0, 15, ExpectedResult = true)]
        [TestCase(-2, 1, 15, ExpectedResult = true)]
        [TestCase(0, 1, 15, ExpectedResult = true)]
        [TestCase(0.9, 1, 15, ExpectedResult = true)]
        [TestCase(1, 1, 15, ExpectedResult = true)]
        [TestCase(1.1, 1, 15, ExpectedResult = true)]
        [TestCase(14.9, 1, 15, ExpectedResult = true)]
        [TestCase(15, 1, 15, ExpectedResult = true)]
        [TestCase(15.1, 1, 15, ExpectedResult = false)]
        [TestCase(-2, 15, -1, ExpectedResult = true)]
        [TestCase(-2, 15, 0, ExpectedResult = true)]
        [TestCase(-2, 15, 1, ExpectedResult = true)]
        [TestCase(0, 15, 1, ExpectedResult = true)]
        [TestCase(0.9, 15, 1, ExpectedResult = true)]
        [TestCase(1, 15, 1, ExpectedResult = true)]
        [TestCase(1.1, 15, 1, ExpectedResult = true)]
        [TestCase(14.9, 15, 1, ExpectedResult = true)]
        [TestCase(15, 15, 1, ExpectedResult = true)]
        [TestCase(15.1, 15, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentBottom_Vertical_Segment(double yPtN, double yBottomEnd, double yTopEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(10, yBottomEnd);
            CartesianCoordinate ptJ = new CartesianCoordinate(10, yTopEnd);

            return ProjectionVertical.PointIsBelowSegmentBottom(yPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(-0.9, -1, ExpectedResult = false)]
        [TestCase(-2, 0, ExpectedResult = true)]
        [TestCase(-2, 1, ExpectedResult = true)]
        [TestCase(0, 1, ExpectedResult = true)]
        [TestCase(0.9, 1, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(1.1, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentBottom_Horizontal_Segment(double yPtN, double yBottomEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(10, yBottomEnd);
            CartesianCoordinate ptJ = new CartesianCoordinate(20, yBottomEnd);

            return ProjectionVertical.PointIsBelowSegmentBottom(yPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, 15, ExpectedResult = true)]
        [TestCase(-2, 0, 15, ExpectedResult = true)]
        [TestCase(-2, 1, 15, ExpectedResult = true)]
        [TestCase(0, 1, 15, ExpectedResult = true)]
        [TestCase(0.9, 1, 15, ExpectedResult = true)]
        [TestCase(1, 1, 15, ExpectedResult = true)]
        [TestCase(1.1, 1, 15, ExpectedResult = true)]
        [TestCase(14.9, 1, 15, ExpectedResult = true)]
        [TestCase(15, 1, 15, ExpectedResult = true)]
        [TestCase(15.1, 1, 15, ExpectedResult = false)]
        [TestCase(-2, 15, -1, ExpectedResult = true)]
        [TestCase(-2, 15, 0, ExpectedResult = true)]
        [TestCase(-2, 15, 1, ExpectedResult = true)]
        [TestCase(0, 15, 1, ExpectedResult = true)]
        [TestCase(0.9, 15, 1, ExpectedResult = true)]
        [TestCase(1, 15, 1, ExpectedResult = true)]
        [TestCase(1.1, 15, 1, ExpectedResult = true)]
        [TestCase(14.9, 15, 1, ExpectedResult = true)]
        [TestCase(15, 15, 1, ExpectedResult = true)]
        [TestCase(15.1, 15, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentBottom_Sloped_Segment(double yPtN, double yBottomEnd, double yTopEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(10, yBottomEnd);
            CartesianCoordinate ptJ = new CartesianCoordinate(20, yTopEnd);

            return ProjectionVertical.PointIsBelowSegmentBottom(yPtN, ptI, ptJ);
        }

        // Using f(y) = 1 + 0.5 * y
        [TestCase(1.5, ExpectedResult = 1)] // Bottom of segment
        [TestCase(2, ExpectedResult = 2)] // On segment end
        [TestCase(2.5, ExpectedResult = 3)] // Between segment end
        [TestCase(3, ExpectedResult = 4)] // On segment end
        [TestCase(3.5, ExpectedResult = 5)] // Top of segment
        public double IntersectionPointY_Sloped(double xPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(2, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(3, 4);

            return ProjectionVertical.IntersectionPointY(xPtN, ptI, ptJ);
        }

        [Test]
        public void IntersectionPointY_Vertical_Throws_Argument_Exception()
        {
            CartesianCoordinate ptI = new CartesianCoordinate(2, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(2, 4);

            Assert.That(() => ProjectionVertical.IntersectionPointY(2, ptI, ptJ),
                Throws.Exception
                    .TypeOf<ArgumentException>());
        }

        [TestCase(1.9, ExpectedResult = 2)] // Bottom of segment
        [TestCase(2, ExpectedResult = 2)] // On segment
        [TestCase(2.1, ExpectedResult = 2)] // Top of segment
        public double IntersectionPointY_Horizontal(double xPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(2, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(3, 2);

            return ProjectionVertical.IntersectionPointY(xPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(-2, 1, ExpectedResult = true)]
        [TestCase(-2, 0, ExpectedResult = true)]
        [TestCase(0, 2, ExpectedResult = true)]
        [TestCase(1, 2, ExpectedResult = true)]
        [TestCase(2, 2, ExpectedResult = false)]    // Pt is on segment intersection
        [TestCase(3, 2, ExpectedResult = false)]
        [TestCase(-1, -2, ExpectedResult = false)]
        [TestCase(1, -2, ExpectedResult = false)]
        [TestCase(0, -2, ExpectedResult = false)]
        [TestCase(2, 0, ExpectedResult = false)]
        [TestCase(2, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentIntersection_Within_Segment(double yPtN, double yIntersection)
        {
            CartesianCoordinate vertexI = new CartesianCoordinate(1, -100);
            CartesianCoordinate vertexJ = new CartesianCoordinate(2, 100);
            return ProjectionVertical.PointIsBelowSegmentIntersection(yPtN, yIntersection, vertexI, vertexJ);
        }

        [TestCase(-2, -1.1, ExpectedResult = false)]
        [TestCase(-2, 1.1, ExpectedResult = false)]
        [TestCase(0, 1.1, ExpectedResult = false)]
        [TestCase(1, 1.1, ExpectedResult = false)]
        public bool PointIsBelowSegmentIntersection_Outside_Segment(double yPtN, double yIntersection)
        {
            CartesianCoordinate vertexI = new CartesianCoordinate(1, -1);
            CartesianCoordinate vertexJ = new CartesianCoordinate(2, 1);
            return ProjectionVertical.PointIsBelowSegmentIntersection(yPtN, yIntersection, vertexI, vertexJ);
        }
    }
}

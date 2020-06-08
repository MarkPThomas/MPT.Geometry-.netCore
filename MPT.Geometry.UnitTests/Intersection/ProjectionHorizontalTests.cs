using System;
using System.Collections.Generic;
using MPT.Geometry.Intersections;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Intersection
{
    [TestFixture]
    public static class ProjectionHorizontalTests
    {
        private static List<CartesianCoordinate> polyline = new List<CartesianCoordinate>()
                                        {
                                            new CartesianCoordinate(-5, -5),
                                            new CartesianCoordinate(6, -5),
                                            new CartesianCoordinate(4, 5),
                                            new CartesianCoordinate(-5, 5),
                                        };

        private static List<CartesianCoordinate> trapezoid = new List<CartesianCoordinate>()
                                        {
                                            new CartesianCoordinate(-5, 5),
                                            new CartesianCoordinate(-5, -5),
                                            new CartesianCoordinate(6, -5),
                                            new CartesianCoordinate(4, 5),
                                            new CartesianCoordinate(-5, 5),
                                        };

        private static List<CartesianCoordinate> polygon = new List<CartesianCoordinate>()
                                        {
                                            new CartesianCoordinate(-5, 5),
                                            new CartesianCoordinate(-5, -5),
                                            new CartesianCoordinate(-2, -5),
                                            new CartesianCoordinate(-2, 2),
                                            new CartesianCoordinate(2, 2),
                                            new CartesianCoordinate(2, -5),
                                            new CartesianCoordinate(5, -5),
                                            new CartesianCoordinate(5, 5),
                                            new CartesianCoordinate(-5, 5),
                                        };

        [Test]
        public static void NumberOfIntersections_Of_Polyline_Throws_Argument_Exception_for_Path()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(1, 1);
            Assert.That(() => ProjectionHorizontal.NumberOfIntersections(coordinate, polyline.ToArray()),
                                Throws.Exception
                                    .TypeOf<ArgumentException>());
        }


        //[TestCase(-6, ExpectedResult = 2)]
        //[TestCase(-5, ExpectedResult = 1)] // On left edge
        //[TestCase(0, ExpectedResult = 1)]
        //[TestCase(4.8, ExpectedResult = 1)] // On right edge
        //[TestCase(6, ExpectedResult = 0)]
        //public static int NumberOfIntersections_Between_Top_And_Bottom_of_Trapezoid(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 1);
        //    return ProjectionHorizontal.NumberOfIntersections(coordinate, trapezoid.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 2)]
        //[TestCase(-5, ExpectedResult = 1)]  // On left vertex
        //[TestCase(0, ExpectedResult = 1)]   // On top segment
        //[TestCase(4, ExpectedResult = 1)]   // On right vertex
        //[TestCase(6, ExpectedResult = 0)]
        //public static int NumberOfIntersections_Aligned_With_Top_of_Trapezoid(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 5);
        //    return ProjectionHorizontal.NumberOfIntersections(coordinate, trapezoid.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 0)]
        //[TestCase(-5, ExpectedResult = 0)]
        //[TestCase(0, ExpectedResult = 0)]
        //[TestCase(3.8, ExpectedResult = 0)]
        //[TestCase(6, ExpectedResult = 0)]
        //public static int NumberOfIntersections_Above_Trapezoid(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 6);
        //    return ProjectionHorizontal.NumberOfIntersections(coordinate, trapezoid.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 4)]
        //[TestCase(-5, ExpectedResult = 1)]  // On left vertical segment
        //[TestCase(-4, ExpectedResult = 3)]
        //[TestCase(-2, ExpectedResult = 1)]  // On left vertical segment of center gap
        //[TestCase(0, ExpectedResult = 2)]   // In center gap
        //[TestCase(2, ExpectedResult = 1)]   // On right vertical segment of center gap
        //[TestCase(4, ExpectedResult = 1)]
        //[TestCase(5, ExpectedResult = 1)]   // On right vertical segment
        //[TestCase(6, ExpectedResult = 0)]
        //public static int NumberOfIntersections_Intersection_Multiple_Solid_Void(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, 1);
        //    return ProjectionHorizontal.NumberOfIntersections(coordinate, polygon.ToArray());
        //}

        //[TestCase(-6, ExpectedResult = 4)]
        //[TestCase(-5, ExpectedResult = 1)]  // On left vertex
        //[TestCase(-4, ExpectedResult = 1)]  // On bottom left segment
        //[TestCase(0, ExpectedResult = 2)]   // In center gap
        //[TestCase(4, ExpectedResult = 1)]   // On bottom right segment
        //[TestCase(5, ExpectedResult = 1)]   // On right vertex
        //[TestCase(6, ExpectedResult = 0)]
        //public static int NumberOfIntersections_Intersection_Multiple_Solid_Void_On_Tooth_Segment(double x)
        //{
        //    CartesianCoordinate coordinate = new CartesianCoordinate(x, -5);
        //    return ProjectionHorizontal.NumberOfIntersections(coordinate, polygon.ToArray());
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
        public static bool PointIsWithinSegmentHeight_Horizontal(double yPtN, double yLeftEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, yLeftEnd);
            CartesianCoordinate ptJ = new CartesianCoordinate(15, yLeftEnd);

            return ProjectionHorizontal.PointIsWithinSegmentHeight(yPtN, ptI, ptJ);
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
        public static bool PointIsWithinSegmentHeight_Vertical(double yPtN, double yLeftEnd, double yRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, yLeftEnd);
            CartesianCoordinate ptJ = new CartesianCoordinate(1, yRightEnd);

            return ProjectionHorizontal.PointIsWithinSegmentHeight(yPtN, ptI, ptJ);
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
        public static bool PointIsWithinSegmentHeight_Sloped(double yPtN, double yLeftEnd, double yRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, yLeftEnd);
            CartesianCoordinate ptJ = new CartesianCoordinate(15, yRightEnd);

            return ProjectionHorizontal.PointIsWithinSegmentHeight(yPtN, ptI, ptJ);
        }

        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = false)]
        public static bool PointIsWithinSegmentHeight_On_Ends_with_Ends_Included(double yPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(15, 5);

            return ProjectionHorizontal.PointIsWithinSegmentHeight(yPtN, ptI, ptJ);
        }

        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(6, ExpectedResult = false)]
        public static bool PointIsWithinSegmentHeight_On_Ends_with_Ends_Excluded(double yPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(15, 5);

            return ProjectionHorizontal.PointIsWithinSegmentHeight(yPtN, ptI, ptJ, includeEnds: false);
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
        public static bool PointIsLeftOfSegmentEnd_Horizontal_Segment(double xPtN, double xLeftEnd, double xRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(xLeftEnd, 10);
            CartesianCoordinate ptJ = new CartesianCoordinate(xRightEnd, 10);

            return ProjectionHorizontal.PointIsLeftOfSegmentEnd(xPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(-0.9, -1, ExpectedResult = false)]
        [TestCase(-2, 0, ExpectedResult = true)]
        [TestCase(-2, 1, ExpectedResult = true)]
        [TestCase(0, 1, ExpectedResult = true)]
        [TestCase(0.9, 1,ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(1.1, 1, ExpectedResult = false)]
        public static bool PointIsLeftOfSegmentEnd_Vertical_Segment(double xPtN, double xRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(xRightEnd, 10);
            CartesianCoordinate ptJ = new CartesianCoordinate(xRightEnd, 20);

            return ProjectionHorizontal.PointIsLeftOfSegmentEnd(xPtN, ptI, ptJ);
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
        public static bool PointIsLeftOfSegmentEnd_Sloped_Segment(double xPtN, double xLeftEnd, double xRightEnd)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(xLeftEnd, 10);
            CartesianCoordinate ptJ = new CartesianCoordinate(xRightEnd, 20);

            return ProjectionHorizontal.PointIsLeftOfSegmentEnd(xPtN, ptI, ptJ);
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(15, ExpectedResult = true)]
        [TestCase(16, ExpectedResult = false)]
        public static bool PointIsLeftOfSegmentEnd_Sloped_Segment_with_Ends_Included(double xPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(15, 5);

            return ProjectionHorizontal.PointIsLeftOfSegmentEnd(xPtN, ptI, ptJ);
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(15, ExpectedResult = false)]
        [TestCase(16, ExpectedResult = false)]
        public static bool PointIsLeftOfSegmentEnd_Sloped_Segment_with_Ends_Excluded(double xPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(1, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(15, 5);

            return ProjectionHorizontal.PointIsLeftOfSegmentEnd(xPtN, ptI, ptJ, includeEnds: false);
        }



        // Using f(x) = 1 + 0.5 * x
        [TestCase(1.5, ExpectedResult = 1)] // Left of segment
        [TestCase(2, ExpectedResult = 2)] // On segment end
        [TestCase(2.5, ExpectedResult = 3)] // Between segment end
        [TestCase(3, ExpectedResult = 4)] // On segment end
        [TestCase(3.5, ExpectedResult = 5)] // Right of segment
        public static double IntersectionPointX_Sloped(double yPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(2, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(4, 3);

            return ProjectionHorizontal.IntersectionPointX(yPtN, ptI, ptJ);
        }

        [Test]
        public static void IntersectionPointX_Horizontal_Throws_Argument_Exception()
        {
            CartesianCoordinate ptI = new CartesianCoordinate(2, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(4, 2);

            Assert.That(() => ProjectionHorizontal.IntersectionPointX(2, ptI, ptJ),
                Throws.Exception
                    .TypeOf<ArgumentException>());
        }

        [TestCase(1.9, ExpectedResult = 2)] // Left of segment
        [TestCase(2, ExpectedResult = 2)] // On segment
        [TestCase(2.1, ExpectedResult = 2)] // Right of segment
        public static double IntersectionPointX_Vertical(double yPtN)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(2, 2);
            CartesianCoordinate ptJ = new CartesianCoordinate(2, 3);

            return ProjectionHorizontal.IntersectionPointX(yPtN, ptI, ptJ);
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
        public static bool PointIsLeftOfSegmentIntersection_Within_Segment(double xPtN, double xIntersection)
        {
            CartesianCoordinate vertexI = new CartesianCoordinate(-100, 1);
            CartesianCoordinate vertexJ = new CartesianCoordinate(100, 2);
            return ProjectionHorizontal.PointIsLeftOfSegmentIntersection(xPtN, xIntersection, vertexI, vertexJ);
        }

        [TestCase(-2, -1.1, ExpectedResult = false)]
        [TestCase(-2, 1.1, ExpectedResult = false)]
        [TestCase(0, 1.1, ExpectedResult = false)]
        [TestCase(1, 1.1, ExpectedResult = false)]
        public static bool PointIsLeftOfSegmentIntersection_Outside_Segment(double xPtN, double xIntersection)
        {
            CartesianCoordinate vertexI = new CartesianCoordinate(-1, 1);
            CartesianCoordinate vertexJ = new CartesianCoordinate(1, 2);
            return ProjectionHorizontal.PointIsLeftOfSegmentIntersection(xPtN, xIntersection, vertexI, vertexJ);
        }
    }
}

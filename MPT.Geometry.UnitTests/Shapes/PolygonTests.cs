using System;
using System.Collections.Generic;
using MPT.Geometry.Segments;
using MPT.Geometry.Shapes;
using MPT.Geometry.Tools;
using MPT.Math;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Shapes
{
    [TestFixture]
    public static class PolygonTests
    {
        public static double Tolerance = 0.00001;

        private static List<CartesianCoordinate> openRectangle = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-3, 2),
                new CartesianCoordinate(-3, -1),
                new CartesianCoordinate(2, -1),
                new CartesianCoordinate(2, 2),
            };

        private static List<CartesianCoordinate> bowTieNonCrossingSegments = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(0, 3),
                new CartesianCoordinate(0, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(2, 1),
                new CartesianCoordinate(2, 3),
                new CartesianCoordinate(1, 2),
            };

        private static List<CartesianCoordinate> house = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(0, 0),
                new CartesianCoordinate(2, 0),
                new CartesianCoordinate(2, 0.9),
                new CartesianCoordinate(1, 1.9),
                new CartesianCoordinate(0, 0.9),
                new CartesianCoordinate(0, 0),
            };

        private static List<CartesianCoordinate> bowTieNonCrossingOrIntersectingSegments = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(1, 2.1),
                new CartesianCoordinate(0, 3.1),
                new CartesianCoordinate(0, 0.9),
                new CartesianCoordinate(1, 1.9),
                new CartesianCoordinate(2, 0.9),
                new CartesianCoordinate(2, 3.1),
                new CartesianCoordinate(1, 2.1),
            };

        #region Initialization     
        [Test]
        public static void Initialization_Empty_Creates_Empty_Object()
        {
            Polygon polygon = new Polygon();

            Assert.AreEqual(0, polygon.Angles.Count);
            Assert.AreEqual(0, polygon.Points.Count);
            Assert.AreEqual(0, polygon.Sides.Count);
            Assert.AreEqual(GeometryLibrary.ZeroTolerance, polygon.Tolerance);
            Assert.IsNull(polygon.Name);
            Assert.IsFalse(polygon.IsHole);
            Assert.AreEqual(0, polygon.Centroid.X);
            Assert.AreEqual(0, polygon.Centroid.Y);
            Assert.AreEqual(0, polygon.Area());
            Assert.AreEqual(0, polygon.Xo());
            Assert.AreEqual(0, polygon.Yo());
        }

        [Test]
        public static void Initialization_with_Open_Shape_Creates_Closed_Shape()
        {
            Polygon polygon = new Polygon(openRectangle);

            Assert.AreEqual(4, polygon.Points.Count);
            Assert.AreEqual(4, polygon.Angles.Count);
            Assert.AreEqual(4, polygon.Sides.Count);
            Assert.AreEqual(GeometryLibrary.ZeroTolerance, polygon.Tolerance);
            Assert.AreEqual(-0.5, polygon.Centroid.X);
            Assert.AreEqual(0.5, polygon.Centroid.Y);
        }

        [Test]
        public static void Initialization_with_Coordinates_Creates_Shape()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);

            Assert.AreEqual(6, polygon.Points.Count); 
            Assert.AreEqual(6, polygon.Angles.Count);
            Assert.AreEqual(6, polygon.Sides.Count);
            Assert.AreEqual(GeometryLibrary.ZeroTolerance, polygon.Tolerance);
            Assert.AreEqual(1, polygon.Centroid.X);
            Assert.AreEqual(2, polygon.Centroid.Y);
        }
        #endregion

        #region Validation       
        [Test]
        public static void CheckValidShape_Throws_ArgumentExeception_for_NonClosed_Polyline()
        {
            PolyLine polyline = new PolyLine(new CartesianCoordinate(1, 2), new CartesianCoordinate(3, 4));

            Assert.Throws<ArgumentException>(() => Shape.CheckValidShape(polyline));
        }

        [Test]
        public static void CheckValidShape_Throws_ArgumentExeception_for_Insufficient_Number_of_Coordinates()
        {
            List<CartesianCoordinate> insufficientCoordinates = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4),
            };

            Polygon polygon = new Polygon(insufficientCoordinates);
            Assert.Throws<ArgumentException>(() => polygon.CheckValidShape());
        }

        [Test]
        public static void CheckValidShape_Throws_ArgumentException_for_Closed_Polyline_of_Crossing_Segments()
        {
            List<CartesianCoordinate> bowTieCrossingSegments = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-1, 1),
                new CartesianCoordinate(-1, -1),
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, -1),
                new CartesianCoordinate(-1, 1),
            };

            Polygon polygon = new Polygon(bowTieCrossingSegments);
            Assert.Throws<ArgumentException>(() => polygon.CheckValidShape());
        }

        [Test]
        public static void CheckValidShape_Throws_ArgumentException_for_Closed_Polyline_of_Point_on_Segment()
        {

            List<CartesianCoordinate> bowTiePointOnSegment = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(0, 0),
                new CartesianCoordinate(-1, 1),
                new CartesianCoordinate(-1, -1),
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, -1),
                new CartesianCoordinate(0, 0),
            };

            Polygon polygon = new Polygon(bowTiePointOnSegment);
            Assert.Throws<ArgumentException>(() => polygon.CheckValidShape());
        }

        [Test]
        public static void CheckValidShape_Throws_ArgumentException_for_Points_Changing_CCW_CW_Directions()
        {
            List<CartesianCoordinate> bowTieInverseAreaSigns = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(0, 3),
                new CartesianCoordinate(0, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(2, 3),
                new CartesianCoordinate(2, 1),
                new CartesianCoordinate(1, 2),
            };

            Polygon polygon = new Polygon(bowTieInverseAreaSigns);
            Assert.Throws<ArgumentException>(() => polygon.CheckValidShape());
        }

        [Test]
        public static void CheckValidShape_Returns_True_for_Valid_Shape()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingOrIntersectingSegments);
            Assert.IsTrue(polygon.CheckValidShape());
        }
        #endregion

        #region Methods
        [Test]
        public static void ToString_Returns_Overridden_ToString_Result()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual("MPT.Geometry.Shapes.Polygon", polygon.ToString());
        }

        [Test]
        public static void GetPerimeterFromPolyline()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(4 * (1 + 2.Sqrt()), polygon.GetPerimeterFromPolyline());
        }

        [Test]
        public static void PolyLine()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            PolyLine polyline = polygon.PolyLine();

            Assert.AreEqual(bowTieNonCrossingSegments[2], polyline[2].I);
        }

        [Test]
        public static void PointBoundary()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            PointBoundary pointBoundary = polygon.PointBoundary();
            Assert.AreEqual(bowTieNonCrossingSegments[2], pointBoundary[2]);
        }

        [Test]
        public static void Extents()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            PointExtents pointExtents = polygon.Extents();
            Assert.AreEqual(0, pointExtents.MinX);
            Assert.AreEqual(2, pointExtents.MaxX);
            Assert.AreEqual(1, pointExtents.MinY);
            Assert.AreEqual(3, pointExtents.MaxY);
        }
        #endregion

        #region Query
        [Test]
        public static void PointAt()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(bowTieNonCrossingSegments[1], polygon.PointAt(1));
        }

        [Test]
        public static void PointAt_Throws_IndexOutOfRangeException_for_Negative_Index()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.Throws<IndexOutOfRangeException>(() => polygon.PointAt(-1));
        }

        [Test]
        public static void PointAt_Throws_IndexOutOfRangeException_for_Index_Beyond_Points()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.Throws<IndexOutOfRangeException>(() => polygon.PointAt(7));
        }

        [Test]
        public static void SideAt()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(new LineSegment(bowTieNonCrossingSegments[1], bowTieNonCrossingSegments[2]), polygon.SideAt(1));
        }

        [Test]
        public static void SideAt_Throws_IndexOutOfRangeException_for_Negative_Index()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.Throws<IndexOutOfRangeException>(() => polygon.SideAt(-1));
        }

        [Test]
        public static void SideAt_Throws_IndexOutOfRangeException_for_Index_Beyond_Points()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.Throws<IndexOutOfRangeException>(() => polygon.SideAt(7));
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        public static void NormalsRotateCounterClockwise_for_bowTie_shape(int pointIndex, bool expectedCCW)
        {
            Polygon polygon = new Polygon(bowTieNonCrossingOrIntersectingSegments);
            Assert.AreEqual(expectedCCW, polygon.NormalsRotateCounterClockwiseAt(pointIndex));
        }

        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        public static void NormalsRotateCounterClockwise_for_house_shape(int pointIndex, bool expectedCCW)
        {
            Polygon polygon = new Polygon(house);
            Assert.AreEqual(expectedCCW, polygon.NormalsRotateCounterClockwiseAt(pointIndex));
        }

        [TestCase(0, 270)]
        [TestCase(1, 45)]
        [TestCase(2, 45)]
        [TestCase(3, 270)]
        [TestCase(4, 45)]
        [TestCase(5, 45)]
        public static void AngleInteriorAt_for_bowTie_shape(int angleIndex, double expectedAngleDegrees)
        {
            Polygon polygon = new Polygon(bowTieNonCrossingOrIntersectingSegments);
            Assert.AreEqual(expectedAngleDegrees, polygon.AngleInteriorAt(angleIndex).DegreesRaw, Tolerance);
        }

        [TestCase(0, 90)]
        [TestCase(1, 90)]
        [TestCase(2, 135)]
        [TestCase(3, 90)]
        [TestCase(4, 135)]
        public static void AngleInteriorAt_for_house_shape(int angleIndex, double expectedAngleDegrees)
        {
            Polygon polygon = new Polygon(house);
            Assert.AreEqual(expectedAngleDegrees, polygon.AngleInteriorAt(angleIndex).DegreesRaw, Tolerance);
        }

        [Test]
        public static void HasReentrantCorners_Returns_True_for_bowTie_shape()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingOrIntersectingSegments);
            Assert.IsTrue(polygon.HasReentrantCorners());
        }

        [Test]
        public static void HasReentrantCorners_Returns_False_for_house_shape()
        {
            Polygon polygon = new Polygon(house);
            Assert.IsFalse(polygon.HasReentrantCorners());
        }

        [Test]
        public static void AngleInteriorAt_Throws_IndexOutOfRangeException_for_Negative_Index()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.Throws<IndexOutOfRangeException>(() => polygon.AngleInteriorAt(-1));
        }

        [Test]
        public static void AngleInteriorAt_Throws_IndexOutOfRangeException_for_Index_Beyond_Points()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.Throws<IndexOutOfRangeException>(() => polygon.AngleInteriorAt(7));
        }
        #endregion

        #region Methods: IShapeProperties
        [Test]
        public static void Area()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(2, polygon.Area());
        }

        [Test]
        public static void Perimeter()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(4 * (1 + 2.Sqrt()), polygon.Perimeter());
        }

        [Test]
        public static void Xo()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(1, polygon.Centroid.X);
        }

        [Test]
        public static void Yo()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Assert.AreEqual(2, polygon.Centroid.Y);
        }
        #endregion

        #region ITransform
        [Test]
        public static void Translate_Translates_Shape()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            CartesianOffset translation = new CartesianOffset(1, -3);

            Polygon polygonTranslated = polygon.Translate(translation) as Polygon;
            PointBoundary pointBoundary = polygonTranslated.PointBoundary();
            Assert.AreEqual(1, pointBoundary[1].X);
            Assert.AreEqual(0, pointBoundary[1].Y);
        }

        [TestCase(0, 2, 1)]
        [TestCase(1, 4, 3.5)]
        [TestCase(0.5, 3, 2.25)]
        [TestCase(1.5, 5, 4.75)]
        [TestCase(-1, 0, -1.5)]
        public static void ScaleFromPoint_Scales_Shape(double scale,
            double expectedCentroid_x, double expectedCentroid_y)
        {
            List<CartesianCoordinate> bowTieNonCrossingSegmentsScaled = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(4, 3.5),
                new CartesianCoordinate(3, 5),
                new CartesianCoordinate(3, 2),
                new CartesianCoordinate(4, 3.5),
                new CartesianCoordinate(5, 2),
                new CartesianCoordinate(5, 5),
                new CartesianCoordinate(4, 3.5),
            };

            Polygon polygon = new Polygon(bowTieNonCrossingSegmentsScaled);

            // Check shape position
            CartesianCoordinate originalCentroid = polygon.Centroid;
            Assert.AreEqual(4, originalCentroid.X);
            Assert.AreEqual(3.5, originalCentroid.Y);

            CartesianCoordinate referencePoint = new CartesianCoordinate(2, 1);
            Polygon polygonScaled = polygon.ScaleFromPoint(scale, referencePoint) as Polygon;

            // Check shape scale
            PointExtents extents = polygonScaled.Extents();
            Assert.AreEqual(2 * scale.Abs(), extents.Width);
            Assert.AreEqual(3 * scale.Abs(), extents.Height);

            // Check scaled centroid position
            CartesianCoordinate scaledCentroid = polygonScaled.Centroid;
            Assert.AreEqual(expectedCentroid_x, scaledCentroid.X);
            Assert.AreEqual(expectedCentroid_y, scaledCentroid.Y);
        }

        [Test]
        public static void RotateAboutPoint_Rotates_Shape()
        {
            Polygon polygon = new Polygon(bowTieNonCrossingSegments);
            Angle rotation = new Angle(-Numbers.PiOver2);
            CartesianCoordinate referencePoint = new CartesianCoordinate(3, -1);

            Polygon polygonRotated = polygon.RotateAboutPoint(rotation, referencePoint) as Polygon;
            PointBoundary pointBoundary = polygonRotated.PointBoundary();
            Assert.AreEqual(6, pointBoundary[0].X, Tolerance);
            Assert.AreEqual(1, pointBoundary[0].Y, Tolerance);
            Assert.AreEqual(7, pointBoundary[1].X, Tolerance);
            Assert.AreEqual(2, pointBoundary[1].Y, Tolerance);
            Assert.AreEqual(5, pointBoundary[2].X, Tolerance);
            Assert.AreEqual(2, pointBoundary[2].Y, Tolerance);
            Assert.AreEqual(5, pointBoundary[4].X, Tolerance);
            Assert.AreEqual(0, pointBoundary[4].Y, Tolerance);
            Assert.AreEqual(7, pointBoundary[5].X, Tolerance);
            Assert.AreEqual(0, pointBoundary[5].Y, Tolerance);
        }
        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MPT.Geometry.Segments;
using MPT.Geometry.Tools;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Segments
{
    [TestFixture]
    public static class PolylineTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void PolyLine_Initializes_with_Coordinate_Pair()
        {
            PolyLine polyLine = new PolyLine(new CartesianCoordinate(-5, -5), new CartesianCoordinate(6, -5));

            Assert.IsTrue(polyLine.IsReadOnly);

            // Coordinates properties
            Assert.AreEqual(2, polyLine.CountPoints);
            Assert.AreEqual(-5, polyLine.Coordinates[0].X);
            Assert.AreEqual(-5, polyLine.Coordinates[0].Y);
            Assert.AreEqual(6, polyLine.Coordinates[1].X);
            Assert.AreEqual(-5, polyLine.Coordinates[1].Y);

            // Segments properties
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(-5, polyLine[0].I.X);
            Assert.AreEqual(-5, polyLine[0].I.Y);
            Assert.AreEqual(6, polyLine[0].J.X);
            Assert.AreEqual(-5, polyLine[0].J.Y);
        }

        [Test]
        public static void PolyLine_Initializes_with_PathSegment()
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5));

            PolyLine polyLine = new PolyLine(segment);

            Assert.IsTrue(polyLine.IsReadOnly);

            // Coordinates properties
            Assert.AreEqual(2, polyLine.CountPoints);
            Assert.AreEqual(-5, polyLine.Coordinates[0].X);
            Assert.AreEqual(-5, polyLine.Coordinates[0].Y);
            Assert.AreEqual(6, polyLine.Coordinates[1].X);
            Assert.AreEqual(-5, polyLine.Coordinates[1].Y);

            // Segments properties
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(-5, polyLine[0].I.X);
            Assert.AreEqual(-5, polyLine[0].I.Y);
            Assert.AreEqual(6, polyLine[0].J.X);
            Assert.AreEqual(-5, polyLine[0].J.Y);
        }

        [Test]
        public static void PolyLine_Initializes_with_Coordinates_List()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>() 
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(points);

            Assert.IsTrue(polyLine.IsReadOnly);

            // Coordinates properties
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(-5, polyLine.Coordinates[0].X);
            Assert.AreEqual(-5, polyLine.Coordinates[0].Y);
            Assert.AreEqual(4, polyLine.Coordinates[2].X);
            Assert.AreEqual(5, polyLine.Coordinates[2].Y);
            Assert.AreEqual(-5, polyLine.Coordinates[4].X);
            Assert.AreEqual(2, polyLine.Coordinates[4].Y);

            // Segments properties
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(-5, polyLine[0].I.X);
            Assert.AreEqual(-5, polyLine[0].I.Y);
            Assert.AreEqual(6, polyLine[0].J.X);
            Assert.AreEqual(-5, polyLine[0].J.Y);
            Assert.AreEqual(-5, polyLine[3].I.X);
            Assert.AreEqual(5, polyLine[3].I.Y);
            Assert.AreEqual(-5, polyLine[3].J.X);
            Assert.AreEqual(2, polyLine[3].J.Y);
        }

        [Test]
        public static void PolyLine_Initializes_with_SegmentsBoundary()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            List<IPathSegment> segments = new List<IPathSegment>()
            {
                new LineSegment(points[0], points[1]),
                new LineSegment(points[1], points[2]),
                new LineSegment(points[2], points[3]),
                new LineSegment(points[3], points[4]),
            };

            SegmentsBoundary segmentsBoundary = new SegmentsBoundary(segments);

            PolyLine polyLine = new PolyLine(segmentsBoundary);

            Assert.IsTrue(polyLine.IsReadOnly);

            // Coordinates properties
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(-5, polyLine.Coordinates[0].X);
            Assert.AreEqual(-5, polyLine.Coordinates[0].Y);
            Assert.AreEqual(4, polyLine.Coordinates[2].X);
            Assert.AreEqual(5, polyLine.Coordinates[2].Y);
            Assert.AreEqual(-5, polyLine.Coordinates[4].X);
            Assert.AreEqual(2, polyLine.Coordinates[4].Y);

            // Segments properties
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(-5, polyLine[0].I.X);
            Assert.AreEqual(-5, polyLine[0].I.Y);
            Assert.AreEqual(6, polyLine[0].J.X);
            Assert.AreEqual(-5, polyLine[0].J.Y);
            Assert.AreEqual(-5, polyLine[3].I.X);
            Assert.AreEqual(5, polyLine[3].I.Y);
            Assert.AreEqual(-5, polyLine[3].J.X);
            Assert.AreEqual(2, polyLine[3].J.Y);
        }
        #endregion

        #region Methods: List
        [Test]
        public static void Polyline_Returns_Segment_by_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyline = new PolyLine(coordinates);

            IPathSegment segment = polyline[1];
            Assert.AreEqual(segments[1].I.X, segment.I.X);
            Assert.AreEqual(segments[1].I.Y, segment.I.Y);
        }

        [Test]
        public static void Polyline_Throws_ReadOnlyException_if_Changing_Segments_by_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyline = new PolyLine(coordinates);

            Assert.Throws<ReadOnlyException>(
                () => polyline[2] = new LineSegment(
                    new CartesianCoordinate(7, 8),
                    new CartesianCoordinate(9, 10)));
        }

        [Test]
        public static void Polyline_Throws_IndexOutOfRangeException_when_Accessing_by_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            Assert.Throws<IndexOutOfRangeException>(() => { IPathSegment segment = polyline[3]; });
        }
        #endregion

        #region Methods: Query
        [Test]
        public static void PointBoundary_Returns_PointBoundary_of_Boundary()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyLine = new PolyLine(points);

            Assert.AreEqual(GeometryLibrary.ZeroTolerance, polyLine.Tolerance);
            Assert.IsTrue(polyLine.IsReadOnly);
            Assert.AreEqual(4, polyLine.CountPoints);
            Assert.AreEqual(3, polyLine.CountSegments);

            PointBoundary pointBoundary = polyLine.PointBoundary() as PointBoundary;
            Assert.IsTrue(pointBoundary.IsReadOnly);
            Assert.AreEqual(4, pointBoundary.Count);
            Assert.AreEqual(3, pointBoundary[2].X);
            Assert.AreEqual(4, pointBoundary[2].Y);
        }

        [Test]
        public static void Extents_Returns_Extents_of_Polyline()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -6),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 7)
            };

            PolyLine polyLine = new PolyLine(points);

            PointExtents extents = polyLine.Extents() as PointExtents;
            Assert.AreEqual(-5, extents.MinX);
            Assert.AreEqual(-6, extents.MinY);
            Assert.AreEqual(6, extents.MaxX);
            Assert.AreEqual(7, extents.MaxY);
        }
        #endregion

        #region Points
        [Test]
        public static void Coordinate_Returns_Coordinate_by_Index()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(points);
            CartesianCoordinate coordinate = polyLine.Coordinate(1);
            Assert.AreEqual(6, coordinate.X);
            Assert.AreEqual(-5, coordinate.Y);
        }

        [Test]
        public static void Coordinate_Throws_IndexOutOfRangeException()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(points);
            Assert.Throws<IndexOutOfRangeException>(() => polyLine.Coordinate(5));
        }

        [Test]
        public static void FirstPoint_Returns_First_Point_of_Polyline()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(points);

            CartesianCoordinate point = polyLine.FirstPoint();

            Assert.AreEqual(-5, point.X);
            Assert.AreEqual(-5, point.Y);
        }

        [Test]
        public static void LastPoint_Returns_Last_Point_of_Polyline()
        {
            List<CartesianCoordinate> points = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(points);

            CartesianCoordinate point = polyLine.LastPoint();

            Assert.AreEqual(-5, point.X);
            Assert.AreEqual(2, point.Y);
        }

        [Test]
        public static void ContainsPoint_Returns_True_or_False_Indicating_Presence_of_Coordinate()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1,1),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyLine = new PolyLine(coordinates);

            Assert.IsTrue(polyLine.ContainsPoint(new CartesianCoordinate(3, 4)));
            Assert.IsFalse(polyLine.ContainsPoint(new CartesianCoordinate(7, 8)));
        }

        [Test]
        public static void AddFirstPoint_Adds_Point_to_Beginning_of_Polyline()
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(-5, -6),
                new CartesianCoordinate(6, -7));

            PolyLine polyLine = new PolyLine(segment);
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(2, polyLine.CountPoints);
            Assert.AreEqual(-5, polyLine.FirstPoint().X);
            Assert.AreEqual(-6, polyLine.FirstPoint().Y);

            CartesianCoordinate point = new CartesianCoordinate(1, 2);

            Assert.IsTrue(polyLine.AddFirstPoint(point));
            Assert.AreEqual(2, polyLine.CountSegments);
            Assert.AreEqual(3, polyLine.CountPoints);
            Assert.AreEqual(1, polyLine.FirstPoint().X);
            Assert.AreEqual(2, polyLine.FirstPoint().Y);
        }

        [Test]
        public static void AddLastPoint_Adds_Point_to_End_of_Polyline()
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(-5, -6),
                new CartesianCoordinate(6, -7));

            PolyLine polyLine = new PolyLine(segment);
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(2, polyLine.CountPoints);
            Assert.AreEqual(-5, polyLine.FirstPoint().X);
            Assert.AreEqual(-6, polyLine.FirstPoint().Y);

            CartesianCoordinate point = new CartesianCoordinate(1, 2);

            Assert.IsTrue(polyLine.AddLastPoint(point));
            Assert.AreEqual(2, polyLine.CountSegments);
            Assert.AreEqual(3, polyLine.CountPoints);
            Assert.AreEqual(1, polyLine.LastPoint().X);
            Assert.AreEqual(2, polyLine.LastPoint().Y);
        }

        [Test]
        public static void RemoveFirstPoint_Removes_First_Point()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            Assert.IsTrue(polyLine.RemoveFirstPoint());
            Assert.AreEqual(2, polyLine.CountSegments);
            Assert.AreEqual(segments[1], polyLine[0]);
            Assert.AreEqual(segments[2], polyLine[1]);

            Assert.IsTrue(polyLine.RemoveFirstPoint());
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(segments[2], polyLine[0]);

            Assert.IsTrue(polyLine.RemoveFirstPoint());
            Assert.AreEqual(0, polyLine.CountSegments);

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveFirstPoint());
        }

        [Test]
        public static void RemoveFirstPoint_Throws_Index_Out_of_Range_Exception_if_Polyline_Empty_of_Segments()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyLine = new PolyLine(coordinates);
            polyLine.RemoveFirstPoint();
            polyLine.RemoveFirstPoint();
            polyLine.RemoveFirstPoint();

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveFirstPoint());
        }

        [Test]
        public static void RemoveLastPoint_Removes_Last_Point()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            Assert.IsTrue(polyLine.RemoveLastPoint());
            Assert.AreEqual(2, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);

            Assert.IsTrue(polyLine.RemoveLastPoint());
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);

            Assert.IsTrue(polyLine.RemoveLastPoint());
            Assert.AreEqual(0, polyLine.CountSegments);

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveLastPoint());
        }

        [Test]
        public static void RemoveLastPoint_Throws_Index_Out_of_Range_Exception_if_Polyline_Empty_of_Segments()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyLine = new PolyLine(coordinates);
            polyLine.RemoveLastPoint();
            polyLine.RemoveLastPoint();
            polyLine.RemoveLastPoint();

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveLastPoint());
        }

        [Test]
        public static void RemovePoint_Removes_All_Instances_of_Point_from_Path()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4),
                new CartesianCoordinate(5, 6),
                new CartesianCoordinate(7, 8)};

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);

            // Remove existing middle point
            Assert.IsTrue(polyLine.RemovePoint(coordinates[1]));
            Assert.AreEqual(4, polyLine.CountPoints);
            Assert.AreEqual(3, polyLine.CountSegments);

            // TODO: Remove point between two different segment types

            // Remove point not present
            CartesianCoordinate nonPresentCoordinate = new CartesianCoordinate(9, 10);

            Assert.IsFalse(polyLine.RemovePoint(nonPresentCoordinate));
            Assert.AreEqual(4, polyLine.CountPoints);
            Assert.AreEqual(3, polyLine.CountSegments);

            // Remove leading point
            Assert.IsTrue(polyLine.RemovePoint(coordinates[0]));
            Assert.AreEqual(3, polyLine.CountPoints);
            Assert.AreEqual(2, polyLine.CountSegments);

            // Remove ending point
            Assert.IsTrue(polyLine.RemovePoint(coordinates[4]));
            Assert.AreEqual(2, polyLine.CountPoints);
            Assert.AreEqual(1, polyLine.CountSegments);

            // Remove last point that removes a segment
            Assert.IsFalse(polyLine.RemovePoint(coordinates[4]));
            Assert.AreEqual(2, polyLine.CountPoints);
            Assert.AreEqual(1, polyLine.CountSegments);
        }
        
        [Test]
        public static void MovePoint_Moves_Point_if_Present()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4),
                new CartesianCoordinate(5, 6),
                new CartesianCoordinate(7, 8)};

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);

            // Move present point
            CartesianCoordinate newPoint = new CartesianCoordinate(9, 10);
            Assert.IsTrue(polyLine.MovePoint(coordinates[1], newPoint));
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(newPoint.X, polyLine.Coordinate(1).X);
            Assert.AreEqual(newPoint.Y, polyLine.Coordinate(1).Y);
        }

        [Test]
        public static void MovePoint_Moves_First_Point_if_Specified()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4),
                new CartesianCoordinate(5, 6),
                new CartesianCoordinate(7, 8)};

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);

            // Move first point
            CartesianCoordinate newPoint = new CartesianCoordinate(9, 10);
            Assert.IsTrue(polyLine.MovePoint(coordinates[0], newPoint));
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(newPoint.X, polyLine.Coordinate(0).X);
            Assert.AreEqual(newPoint.Y, polyLine.Coordinate(0).Y);
        }

        [Test]
        public static void MovePoint_Moves_Last_Point_if_Specified()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4),
                new CartesianCoordinate(5, 6),
                new CartesianCoordinate(7, 8)};

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);

            // Move last point
            CartesianCoordinate newPoint = new CartesianCoordinate(9, 10);
            Assert.IsTrue(polyLine.MovePoint(coordinates[4], newPoint));
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(newPoint.X, polyLine.Coordinate(4).X);
            Assert.AreEqual(newPoint.Y, polyLine.Coordinate(4).Y);
        }

        [Test]
        public static void MovePoint_Does_Not_Move_Point_if_Not_Present()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1, 1),
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4),
                new CartesianCoordinate(5, 6),
                new CartesianCoordinate(7, 8)};

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);

            // Move non-present point
            CartesianCoordinate newNonPresentPoint = new CartesianCoordinate(11, 12);
            CartesianCoordinate otherNewPoint = new CartesianCoordinate(13, 14);
            Assert.IsFalse(polyLine.MovePoint(newNonPresentPoint, otherNewPoint));
            Assert.AreEqual(5, polyLine.CountPoints);
            Assert.AreEqual(4, polyLine.CountSegments);
        }
        #endregion

        #region Segments
        [Test]
        public static void Segment_Returns_Segment_by_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            IPathSegment segment = polyLine.Segment(1);
            Assert.AreEqual(segments[1].I.X, segment.I.X);
            Assert.AreEqual(segments[1].I.Y, segment.I.Y);
        }

        [Test]
        public static void Segment_Throws_IndexOutOfRangeException()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.Throws<IndexOutOfRangeException>(() => polyLine.Segment(5));
        }

        [Test]
        public static void Contains_Returns_True_or_False_Indicating_Presence_of_Segment()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1,1),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)
            };

            PolyLine polyLine = new PolyLine(coordinates);

            Assert.IsTrue(polyLine.ContainsSegment(new LineSegment(coordinates[1], coordinates[2])));
            Assert.IsFalse(polyLine.ContainsSegment(
                new LineSegment(
                    new CartesianCoordinate(5, 6),
                    new CartesianCoordinate(7, 8))));
        }

        [Test]
        public static void AddFirstSegment_Adds_Segment_at_Beginning_if_Compatible()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            LineSegment newSegment = new LineSegment(new CartesianCoordinate(7, 8), coordinates[0]);

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            Assert.IsTrue(polyLine.AddFirstSegment(newSegment));
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(newSegment, polyLine[0]);
            Assert.AreEqual(segments[0], polyLine[1]);
        }

        [Test]
        public static void AddFirstSegment_Throws_ArgumentException_if_Not_Compatible()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            LineSegment incompatibleSegment = new LineSegment(new CartesianCoordinate(7, 8), new CartesianCoordinate(9, 10));

            PolyLine polyLine = new PolyLine(coordinates);

            Assert.Throws<ArgumentException>(() => polyLine.AddFirstSegment(incompatibleSegment));
        }

        [Test]
        public static void AddLastSegment_Adds_Segment_at_End_if_Compatible()
        { 
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            LineSegment newSegment = new LineSegment(coordinates[3], new CartesianCoordinate(7, 8));

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            Assert.IsTrue(polyLine.AddLastSegment(newSegment));
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(newSegment, polyLine[3]);
            Assert.AreEqual(segments[2], polyLine[2]);
        }

        [Test]
        public static void AddLastSegment_Throws_ArgumentException_if_Not_Compatible()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            LineSegment incompatibleSegment = new LineSegment(new CartesianCoordinate(7, 8), new CartesianCoordinate(9, 10));

            PolyLine polyLine = new PolyLine(coordinates);

            Assert.Throws<ArgumentException>(() => polyLine.AddLastSegment(incompatibleSegment));
        }

        [Test]
        public static void SplitSegment_Splits_Specified_Segment_if_Segment_is_Present()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            CartesianCoordinate expectedSplitCoordinate = new CartesianCoordinate(2, 3);
            LineSegment expectedNewFirstSegment = new LineSegment(coordinates[1], expectedSplitCoordinate);
            LineSegment expectedNewSecondSegment = new LineSegment(expectedSplitCoordinate, coordinates[2]);

            Assert.IsTrue(polyLine.SplitSegment(segments[1], 0.5));
            Assert.AreEqual(4, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(expectedNewFirstSegment, polyLine[1]);
            Assert.AreEqual(expectedNewSecondSegment, polyLine[2]);
            Assert.AreEqual(segments[2], polyLine[3]);
        }

        [Test]
        public static void SplitSegment_Returns_False_if_Segment_is_not_Present()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            LineSegment nonExistingSegment = new LineSegment(new CartesianCoordinate(7, 8), new CartesianCoordinate(9, 10));

            Assert.IsFalse(polyLine.SplitSegment(nonExistingSegment, 0.5));
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);
        }

        [Test]
        public static void SplitSegment_Returns_False_if_Relative_Position_is_Negative()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            LineSegment nonExistingSegment = new LineSegment(new CartesianCoordinate(7, 8), new CartesianCoordinate(9, 10));

            Assert.IsFalse(polyLine.SplitSegment(nonExistingSegment, -0.5));
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);
        }

        [Test]
        public static void SplitSegment_Returns_False_if_Relative_Position_is_Greater_than_One()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            LineSegment nonExistingSegment = new LineSegment(new CartesianCoordinate(7, 8), new CartesianCoordinate(9, 10));

            Assert.IsFalse(polyLine.SplitSegment(nonExistingSegment, 1.5));
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);
        }

        [Test]
        public static void RemoveFirstSegment_Removes_First_Segment()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            Assert.IsTrue(polyLine.RemoveFirstSegment());
            Assert.AreEqual(2, polyLine.CountSegments);
            Assert.AreEqual(segments[1], polyLine[0]);
            Assert.AreEqual(segments[2], polyLine[1]);

            Assert.IsTrue(polyLine.RemoveFirstSegment());
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(segments[2], polyLine[0]);

            Assert.IsTrue(polyLine.RemoveFirstSegment());
            Assert.AreEqual(0, polyLine.CountSegments);

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveFirstSegment());
        }

        [Test]
        public static void RemoveFirstSegment_Throws_Index_Out_of_Range_Exception_if_Polyline_Empty()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyLine = new PolyLine(coordinates);
            polyLine.RemoveFirstSegment();
            polyLine.RemoveFirstSegment();
            polyLine.RemoveFirstSegment();

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveFirstSegment());
        }

        [Test]
        public static void RemoveLastSegment_Removes_Last_Segment()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyLine = new PolyLine(coordinates);
            Assert.AreEqual(3, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);
            Assert.AreEqual(segments[2], polyLine[2]);

            Assert.IsTrue(polyLine.RemoveLastSegment());
            Assert.AreEqual(2, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);
            Assert.AreEqual(segments[1], polyLine[1]);

            Assert.IsTrue(polyLine.RemoveLastSegment());
            Assert.AreEqual(1, polyLine.CountSegments);
            Assert.AreEqual(segments[0], polyLine[0]);

            Assert.IsTrue(polyLine.RemoveLastSegment());
            Assert.AreEqual(0, polyLine.CountSegments);

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveLastSegment());
        }

        [Test]
        public static void RemoveLastSegment_Throws_Index_Out_of_Range_Exception_if_Polyline_Empty()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyLine = new PolyLine(coordinates);
            polyLine.RemoveLastSegment();
            polyLine.RemoveLastSegment();
            polyLine.RemoveLastSegment();

            Assert.Throws<IndexOutOfRangeException>(() => polyLine.RemoveLastSegment());
        }
        #endregion

        #region Methods: IPathTransform        
        

        public static void Translate()
        {

        }


        public static void ScaleFromPoint()
        {

        }


        public static void RotateAboutPoint()
        {

        }

        // TODO: Implement Skew for this & Segments
        // TODO: Implement Mirror for this & Segments
        #endregion

        #region Methods: Enumerator
        [Test]
        public static void Enumerator_Enumerates_Over_Segment()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyline = new PolyLine(coordinates);

            int index = 0;
            foreach (IPathSegment segment in polyline)
            {
                Assert.AreEqual(segments[index], segment); // Iterating through null rather than ending
                index++;
            }
        }

        [Test]
        public static void GetEnumerator_Allows_Enumeration_Over_Coordinates()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyline = new PolyLine(coordinates); 

            IEnumerator enumerator = polyline.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(segments[0], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(segments[1], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(segments[2], enumerator.Current);

            enumerator.Reset();
            enumerator.MoveNext();
            Assert.AreEqual(segments[0], enumerator.Current);
        }

        [Test]
        public static void GetEnumerator_Current_Throws_Invalid_Operation_Exception_if_Not_Initialized()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() => { var item = enumerator.Current; });
        }

        [Test]
        public static void GetEnumerator_Current_Throws_Invalid_Operation_Exception_if_Moved_Beyond_Max_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.Throws<InvalidOperationException>(() => { var item = enumerator.Current; }); 
        }

        [Test]
        public static void SegmentEnumerator_Allows_Enumeration_Over_Segments()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            List<LineSegment> segments = new List<LineSegment>()
            {
                new LineSegment(coordinates[0], coordinates[1]),
                new LineSegment(coordinates[1], coordinates[2]),
                new LineSegment(coordinates[2], coordinates[3])
            };

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetSegmentEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(segments[0], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(segments[1], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(segments[2], enumerator.Current);

            enumerator.Reset();
            enumerator.MoveNext();
            Assert.AreEqual(segments[0], enumerator.Current);
        }

        [Test]
        public static void SegmentEnumerator_Current_Throws_Invalid_Operation_Exception_if_Not_Initialized()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetSegmentEnumerator();
            Assert.Throws<InvalidOperationException>(() => { var item = enumerator.Current; });
        }

        [Test]
        public static void SegmentEnumerator_Current_Throws_Invalid_Operation_Exception_if_Moved_Beyond_Max_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetSegmentEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.Throws<InvalidOperationException>(() => { var item = enumerator.Current; });
        }

        [Test]
        public static void CoordinateEnumerator_Allows_Enumeration_Over_Coordinates()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetCoordinateEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(coordinates[0], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(coordinates[1], enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(coordinates[2], enumerator.Current);

            enumerator.Reset();
            enumerator.MoveNext();
            Assert.AreEqual(coordinates[0], enumerator.Current);
        }

        [Test]
        public static void CoordinateEnumerator_Current_Throws_Invalid_Operation_Exception_if_Not_Initialized()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetCoordinateEnumerator();
            Assert.Throws<InvalidOperationException>(() => { var item = enumerator.Current; });
        }

        [Test]
        public static void CoordinateEnumerator_Current_Throws_Invalid_Operation_Exception_if_Moved_Beyond_Max_Index()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PolyLine polyline = new PolyLine(coordinates);

            IEnumerator enumerator = polyline.GetCoordinateEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.Throws<InvalidOperationException>(() => { var item = enumerator.Current; });
        }
        #endregion

        #region ICloneable
        [Test]
        public static void CloneLine_Clones_Polyline()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(coordinates);
            PolyLine polyLineClone = polyLine.CloneLine();

            Assert.AreEqual(polyLine[0].I.X, polyLineClone[0].I.X);
            Assert.AreEqual(polyLine[0].I.Y, polyLineClone[0].I.Y);
            Assert.AreEqual(polyLine[1].I.X, polyLineClone[1].I.X);
            Assert.AreEqual(polyLine[1].I.Y, polyLineClone[1].I.Y);
            Assert.AreEqual(polyLine[2].I.X, polyLineClone[2].I.X);
            Assert.AreEqual(polyLine[2].I.Y, polyLineClone[2].I.Y);
            Assert.AreEqual(polyLine[3].I.X, polyLineClone[3].I.X);
            Assert.AreEqual(polyLine[3].I.Y, polyLineClone[3].I.Y);
            Assert.AreEqual(polyLine[3].J.X, polyLineClone[3].J.X);
            Assert.AreEqual(polyLine[3].J.Y, polyLineClone[3].J.Y);
        }

        [Test]
        public static void Clone_Clones_Polyline_as_Object()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5),
                new CartesianCoordinate(4, 5),
                new CartesianCoordinate(-5, 5),
                new CartesianCoordinate(-5, 2)
            };

            PolyLine polyLine = new PolyLine(coordinates);
            PolyLine polyLineClone = polyLine.Clone() as PolyLine;

            Assert.AreEqual(polyLine[0].I.X, polyLineClone[0].I.X);
            Assert.AreEqual(polyLine[0].I.Y, polyLineClone[0].I.Y);
            Assert.AreEqual(polyLine[1].I.X, polyLineClone[1].I.X);
            Assert.AreEqual(polyLine[1].I.Y, polyLineClone[1].I.Y);
            Assert.AreEqual(polyLine[2].I.X, polyLineClone[2].I.X);
            Assert.AreEqual(polyLine[2].I.Y, polyLineClone[2].I.Y);
            Assert.AreEqual(polyLine[3].I.X, polyLineClone[3].I.X);
            Assert.AreEqual(polyLine[3].I.Y, polyLineClone[3].I.Y);
            Assert.AreEqual(polyLine[3].J.X, polyLineClone[3].J.X);
            Assert.AreEqual(polyLine[3].J.Y, polyLineClone[3].J.Y);
        }
        #endregion
    }
}

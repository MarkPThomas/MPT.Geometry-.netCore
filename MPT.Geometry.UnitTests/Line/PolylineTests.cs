using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MPT.Geometry.Intersections;
using MPT.Geometry.Segments;
using MPT.Geometry.Tools;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Line
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

        #region Methods: Public
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


        public static void InsertPoint_Insert_Point_at_Specified_Index()
        {

        }


        public static void RemovePoint_Removes_All_Instances_of_Point_from_Path()
        {
            LineSegment segment = new LineSegment(
                new CartesianCoordinate(-5, -5),
                new CartesianCoordinate(6, -5));

            PolyLine polyLine = new PolyLine(segment);
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
    }
}

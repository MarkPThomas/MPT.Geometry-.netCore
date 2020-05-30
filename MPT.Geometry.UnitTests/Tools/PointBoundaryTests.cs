using System.Collections.Generic;
using MPT.Geometry.Tools;
using MPT.Math.Coordinates;
using NUnit.Framework;


namespace MPT.Geometry.UnitTests.Tools
{
    [TestFixture]
    public static class PointBoundaryTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization_without_Coordinates_Results_in_Empty_Object()
        {
            PointBoundary boundary = new PointBoundary();

            Assert.AreEqual(GeometryLibrary.ZeroTolerance, boundary.Tolerance);
            Assert.AreEqual(0, boundary.Coordinates.Count);
        }

        [Test]
        public static void Initialization_with_Coordinates_Results_in_Object_with_Immutable_Coordinates_Properties_List()
        {
            int index = 1;
            double xOld = 1;
            double yOld = 2;

            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(xOld,yOld),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            double xNew = 7;
            double yNew = 8;

            PointBoundary boundary = new PointBoundary(coordinates);

            Assert.AreEqual(GeometryLibrary.ZeroTolerance, boundary.Tolerance);
            Assert.AreEqual(4, boundary.Coordinates.Count);
            Assert.AreEqual(xOld, boundary.Coordinates[index].X);
            Assert.AreEqual(yOld, boundary.Coordinates[index].Y);

            // Alter existing coordinates to passed in reference
            coordinates[1] = new CartesianCoordinate(xNew, yNew);
            Assert.AreEqual(4, boundary.Coordinates.Count);
            Assert.AreEqual(xOld, boundary.Coordinates[index].X);
            Assert.AreEqual(yOld, boundary.Coordinates[index].Y);
            Assert.AreNotEqual(xNew, boundary.Coordinates[index].X);
            Assert.AreNotEqual(yNew, boundary.Coordinates[index].Y);

            // Add new coordinate to passed in reference
            coordinates.Add(new CartesianCoordinate(xNew, yNew));
            Assert.AreEqual(4, boundary.Coordinates.Count);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void Clear_Clears_Coordinates_from_Boundary()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PointBoundary boundary = new PointBoundary(coordinates);
            Assert.AreEqual(4, boundary.Coordinates.Count);

            boundary.Clear();
            Assert.AreEqual(0, boundary.Coordinates.Count);

        }

        [Test]
        public static void AddRange_Adds_Coordinates_to_Boundary()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PointBoundary boundary = new PointBoundary(coordinates);
            Assert.AreEqual(4, boundary.Coordinates.Count);

            List<CartesianCoordinate> coordinatesAdded = new List<CartesianCoordinate>(){
                new CartesianCoordinate(7,8),
                new CartesianCoordinate(9,10)};

            boundary.AddRange(coordinatesAdded);
            Assert.AreEqual(6, boundary.Coordinates.Count);
            Assert.AreEqual(7, boundary.Coordinates[4].X);
            Assert.AreEqual(8, boundary.Coordinates[4].Y);
            Assert.AreEqual(9, boundary.Coordinates[5].X);
            Assert.AreEqual(10, boundary.Coordinates[5].Y);
        }

        [Test]
        public static void Reset_Resets_Coordinates_in_Boundary()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PointBoundary boundary = new PointBoundary(coordinates);
            Assert.AreEqual(4, boundary.Coordinates.Count);
            Assert.AreEqual(1, boundary.Coordinates[1].X);
            Assert.AreEqual(2, boundary.Coordinates[1].Y);

            List<CartesianCoordinate> coordinatesReset = new List<CartesianCoordinate>(){
                new CartesianCoordinate(7,8),
                new CartesianCoordinate(9,10)};

            boundary.Reset(coordinatesReset);
            Assert.AreEqual(2, boundary.Coordinates.Count);
            Assert.AreEqual(9, boundary.Coordinates[1].X);
            Assert.AreEqual(10, boundary.Coordinates[1].Y);
        }

        [Test]
        public static void Remove_Removes_Coordinates_from_Boundary()
        {
            List<CartesianCoordinate> coordinates = new List<CartesianCoordinate>(){
                new CartesianCoordinate(0,0),
                new CartesianCoordinate(1,2),
                new CartesianCoordinate(3,4),
                new CartesianCoordinate(5,6)};

            PointBoundary boundary = new PointBoundary(coordinates);
            Assert.AreEqual(4, boundary.Coordinates.Count);
            Assert.AreEqual(1, boundary.Coordinates[1].X);
            Assert.AreEqual(2, boundary.Coordinates[1].Y);

            List<CartesianCoordinate> coordinatesRemove = new List<CartesianCoordinate>(){
                new CartesianCoordinate(1,2),  // Existing coordinate
                new CartesianCoordinate(7,8)}; // Non-existing coordinate

            boundary.RemoveRange(coordinatesRemove);
            Assert.AreEqual(3, boundary.Coordinates.Count);
            Assert.AreEqual(3, boundary.Coordinates[1].X);
            Assert.AreEqual(4, boundary.Coordinates[1].Y);
        }
        #endregion
    }
}

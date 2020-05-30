using System.Collections.Generic;
using NMath = System.Math;

using MPT.Math.Coordinates;
using System;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Class PointExtents.
    /// Implements the <see cref="MPT.Geometry.Tools.Extents{CartesianCoordinate}" />
    /// </summary>
    /// <seealso cref="MPT.Geometry.Tools.Extents{CartesianCoordinate}" />
    public class PointExtents : Extents<CartesianCoordinate>
    {
        #region Initialization
        // TODO: Consider if PointExtents should be able to have limits applied?

        /// <summary>
        /// Initializes a new instance of the <see cref="PointExtents"/> class.
        /// </summary>
        public PointExtents()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointExtents"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public PointExtents(IEnumerable<CartesianCoordinate> coordinates) : base(coordinates)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointExtents"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        protected PointExtents(Extents<CartesianCoordinate> extents) : base(extents)
        { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Determines whether the coordinate lies within the extents.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the extents; otherwise, <c>false</c>.</returns>
        public override bool IsWithinExtents(CartesianCoordinate coordinate)
        {
            return ((MinX <= coordinate.X && coordinate.X <= MaxX) &&
                    (MinY <= coordinate.Y && coordinate.Y <= MaxY));
        }

        /// <summary>
        /// Returns a rectangle boundary of this instance.
        /// </summary>
        /// <returns>NRectangle.</returns>
        public override IList<CartesianCoordinate> Boundary()
        {
            return new List<CartesianCoordinate>()
            {
                new CartesianCoordinate(MinX, MaxY),
                new CartesianCoordinate(MaxX, MaxY),
                new CartesianCoordinate(MaxX, MinY),
                new CartesianCoordinate(MinX, MinY),
            };
        }

        /// <summary>
        /// Gets the geometric center.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        /// <value>The geometric center.</value>
        public override CartesianCoordinate GeometricCenter()
        {
            return new CartesianCoordinate(MinX + Width/2, MinY + Height/2);
        }

        /// <summary>
        /// Translates points that define the extents.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>Extents&lt;T&gt;.</returns>
        public override Extents<CartesianCoordinate> Translate(double x, double y)
        {
            CartesianCoordinate coordinateTranslation = new CartesianCoordinate(x, y);
            IList<CartesianCoordinate> coordinates = Boundary();
            for (int i = 0; i < coordinates.Count; i++)
            {
                coordinates[i] += coordinateTranslation;
            }

            return new PointExtents(coordinates);
        }

        /// <summary>
        /// Rotates points that define the extents.
        /// </summary>
        /// <param name="angleRadians">The angle [radians].</param>
        /// <returns>Extents&lt;T&gt;.</returns>
        public override Extents<CartesianCoordinate> Rotate(double angleRadians)
        {
            CartesianCoordinate geometricCenter = GeometricCenter();
            IList<CartesianCoordinate> boundary = Boundary();
            for (int i = 0; i < boundary.Count; i++)
            {
                boundary[i] = CartesianCoordinate.RotateAboutPoint(boundary[i], geometricCenter, angleRadians);
            }

            return new PointExtents(boundary);
        }
        #endregion

        #region Methods: Protected
        /// <summary>
        /// Adds the coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        protected override void addCoordinate(CartesianCoordinate coordinate)
        {
            if (coordinate.Y > MaxY)
            {
                MaxY = NMath.Min(coordinate.Y, _maxYLimit);
            }
            if (coordinate.Y < MinY)
            {
                MinY = NMath.Max(coordinate.Y, _minYLimit);
            }

            if (coordinate.X > MaxX)
            {
                MaxX = NMath.Min(coordinate.X, _maxXLimit);
            }
            if (coordinate.X < MinX)
            {
                MinX = NMath.Max(coordinate.X, _minXLimit);
            }
        }
        #endregion

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public PointExtents Clone()
        {
            return new PointExtents(this);
        }
    }
}

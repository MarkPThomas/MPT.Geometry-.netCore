using System.Collections.Generic;
using NMath = System.Math;

using MPT.Math;
using MPT.Math.Coordinates;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Class PointExtents.
    /// Implements the <see cref="MPT.Geometry.Tools.Extents{CartesianCoordinate}" />
    /// </summary>
    /// <seealso cref="MPT.Geometry.Tools.Extents{CartesianCoordinate}" />
    public class PointExtents : Extents<CartesianCoordinate>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PointExtents"/> class.
        /// </summary>
        public PointExtents()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointExtents"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public PointExtents(IEnumerable<CartesianCoordinate> coordinates) : base (coordinates)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointExtents"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public PointExtents(Extents<CartesianCoordinate> extents) : base (extents)
        { }

        /// <summary>
        /// Updates the extents to include the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public override void Add(CartesianCoordinate coordinate)
        {
            if (coordinate.Y > MaxY)
            {
                MaxY = NMath.Min(coordinate.Y, _minYLimit);
            }
            if (coordinate.Y < MinY)
            {
                MinY = NMath.Max(coordinate.Y, _maxYLimit);
            }

            if (coordinate.X > MaxX)
            {
                MaxX = NMath.Min(coordinate.X, _minXLimit);
            }
            if (coordinate.X < MinX)
            {
                MinX = NMath.Max(coordinate.X, _maxXLimit);
            }
        }

        ///// <summary>
        ///// Updates the extents to include the specified coordinates.
        ///// </summary>
        ///// <param name="coordinates">The coordinates.</param>
        //public void Add(IEnumerable<cartCoords> coordinates)
        //{
        //    foreach (cartCoords coordinate in coordinates)
        //    {
        //        Add(coordinate);
        //    }
        //}

        ///// <summary>
        ///// Updates the extents to include the specified extents.
        ///// </summary>
        ///// <param name="extents">The extents.</param>
        //public void Add(Extents<cartCoords> extents)
        //{
        //    if (extents.MaxY > MaxY)
        //    {
        //        MaxY = extents.MaxY;
        //    }
        //    if (extents.MinY < MinY)
        //    {
        //        MinY = extents.MinY;
        //    }
        //    if (extents.MaxX > MaxX)
        //    {
        //        MaxX = extents.MaxX;
        //    }
        //    if (extents.MinX < MinX)
        //    {
        //        MinX = extents.MinX;
        //    }
        //}

       
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
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public PointExtents Clone()
        {
            return new PointExtents(this);
        }
    }
}

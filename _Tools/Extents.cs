// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Extents.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

using MPT.Math;
using NMath = System.Math;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Represents the coordinate bounds of a shape or line, or cluster of points.
    /// </summary>
    public class Extents
    {
        /// <summary>
        /// The maximum allowed Y-coordinate
        /// </summary>
        protected double _maxYLimit = double.NegativeInfinity;
        /// <summary>
        /// The minimum allowed Y-coordinate
        /// </summary>
        protected double _minYLimit = double.PositiveInfinity;
        /// <summary>
        /// The maximum allowed X-coordinate
        /// </summary>
        protected double _maxXLimit = double.NegativeInfinity;
        /// <summary>
        /// The minimum allowed X-coordinate
        /// </summary>
        protected double _minXLimit = double.PositiveInfinity;

        /// <summary>
        /// Gets the maximum Y-coordinate.
        /// </summary>
        /// <value>The maximum Y-coordinate.</value>
        public double MaxY { get; private set; }
        /// <summary>
        /// Gets the minimum Y-coordinate.
        /// </summary>
        /// <value>The minimum Y-coordinate.</value>
        public double MinY { get; private set; }
        /// <summary>
        /// Gets the maximum X-coordinate.
        /// </summary>
        /// <value>The maximum X-coordinate.</value>
        public double MaxX { get; private set; }
        /// <summary>
        /// Gets the minimum X-coordinate.
        /// </summary>
        /// <value>The minimum X-coordinate.</value>
        public double MinX { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        public Extents()
        {
            initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public Extents(IEnumerable<Point> coordinates)
        {
            initialize();
            Add(coordinates);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public Extents(Extents extents)
        {
            initialize();
            Add(new Point(extents.MaxX, extents.MaxY));
            Add(new Point(extents.MaxX, extents.MinY));
            Add(new Point(extents.MinX, extents.MinY));
            Add(new Point(extents.MinX, extents.MaxY));
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initialize()
        {
            MaxY = _maxYLimit;
            MinY = _minYLimit;
            MaxX = _maxXLimit;
            MinX = _minXLimit;
        }

        /// <summary>
        /// Updates the extents to include the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void Add(Point coordinate)
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

        /// <summary>
        /// Updates the extents to include the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void Add(IEnumerable<Point> coordinates)
        {
            foreach (Point coordinate in coordinates)
            {
                Add(coordinate);
            }
        }

        /// <summary>
        /// Updates the extents to include the specified extents.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public void Add(Extents extents)
        {
            if (extents.MaxY > MaxY)
            {
                MaxY = extents.MaxY;
            }
            if (extents.MinY < MinY)
            {
                MinY = extents.MinY;
            }
            if (extents.MaxX > MaxX)
            {
                MaxX = extents.MaxX;
            }
            if (extents.MinX < MinX)
            {
                MinX = extents.MinX;
            }
        }


        /// <summary>
        /// Determines whether the coordinate lies within the extents.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the extents; otherwise, <c>false</c>.</returns>
        public bool IsWithinExtents(Point coordinate)
        {
            return ((MinX <= coordinate.X && coordinate.X <= MaxX) &&
                    (MinY <= coordinate.Y && coordinate.Y <= MaxY));
        }

        /// <summary>
        /// Returns a rectangle boundary of this instance.
        /// </summary>
        /// <returns>NRectangle.</returns>
        public IList<Point> Boundary()
        {
            return new List<Point>()
            {
                new Point(MinX, MaxY),
                new Point(MaxX, MaxY),
                new Point(MaxX, MinY),
                new Point(MinX, MinY),
            };
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public Extents Clone()
        {
            return new Extents(this);
        }
    }
}

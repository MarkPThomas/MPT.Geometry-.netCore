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

using MPT.Math.Coordinates;


namespace MPT.Geometry.Tools
{
    /// <summary>
    /// CRepresents the coordinate bounds of a shape or line, or cluster of points.
    /// </summary>
    /// <typeparam name="T">The type of coordinate.</typeparam>
    public abstract class Extents<T> where T : ICoordinate
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
        public double MaxY { get; protected set; }
        /// <summary>
        /// Gets the minimum Y-coordinate.
        /// </summary>
        /// <value>The minimum Y-coordinate.</value>
        public double MinY { get; protected set; }
        /// <summary>
        /// Gets the maximum X-coordinate.
        /// </summary>
        /// <value>The maximum X-coordinate.</value>
        public double MaxX { get; protected set; }
        /// <summary>
        /// Gets the minimum X-coordinate.
        /// </summary>
        /// <value>The minimum X-coordinate.</value>
        public double MinX { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}"/> class.
        /// </summary>
        protected Extents()
        {
            initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}"/> class.
        /// </summary>
        /// <param name="maxYLimit">The maximum y limit.</param>
        /// <param name="minYLimit">The minimum y limit.</param>
        /// <param name="maxXLimit">The maximum x limit.</param>
        /// <param name="minXLimit">The minimum x limit.</param>
        protected Extents(
            double maxYLimit,
            double minYLimit,
            double maxXLimit,
            double minXLimit)
        {
            initializeLimits(
                maxYLimit, minYLimit,
                maxXLimit, minXLimit);
            initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="maxYLimit">The maximum y limit.</param>
        /// <param name="minYLimit">The minimum y limit.</param>
        /// <param name="maxXLimit">The maximum x limit.</param>
        /// <param name="minXLimit">The minimum x limit.</param>
        protected Extents(IEnumerable<T> coordinates,
            double maxYLimit = double.NegativeInfinity,
            double minYLimit = double.PositiveInfinity,
            double maxXLimit = double.NegativeInfinity,
            double minXLimit = double.PositiveInfinity)
        {
            initializeLimits(
                maxYLimit, minYLimit,
                maxXLimit, minXLimit);
            initialize();
            Add(coordinates);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        /// <param name="maxYLimit">The maximum y limit.</param>
        /// <param name="minYLimit">The minimum y limit.</param>
        /// <param name="maxXLimit">The maximum x limit.</param>
        /// <param name="minXLimit">The minimum x limit.</param>
        protected Extents(Extents<T> extents,
            double maxYLimit = double.NegativeInfinity,
            double minYLimit = double.PositiveInfinity,
            double maxXLimit = double.NegativeInfinity,
            double minXLimit = double.PositiveInfinity)
        {
            initializeLimits(
                maxYLimit, minYLimit,
                maxXLimit,minXLimit);
            initialize();
            Add(extents.Boundary());
        }

        /// <summary>
        /// Initializes the limits.
        /// </summary>
        /// <param name="maxYLimit">The maximum y limit.</param>
        /// <param name="minYLimit">The minimum y limit.</param>
        /// <param name="maxXLimit">The maximum x limit.</param>
        /// <param name="minXLimit">The minimum x limit.</param>
        protected void initializeLimits(
            double maxYLimit = double.NegativeInfinity,
            double minYLimit = double.PositiveInfinity,
            double maxXLimit = double.NegativeInfinity,
            double minXLimit = double.PositiveInfinity)
        {
            _maxYLimit = maxYLimit;
            _minYLimit = minYLimit;
            _maxXLimit = maxXLimit;
            _minXLimit = minXLimit;
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
        public abstract void Add(T coordinate);

        /// <summary>
        /// Updates the extents to include the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void Add(IEnumerable<T> coordinates)
        {
            foreach (T coordinate in coordinates)
            {
                Add(coordinate);
            }
        }

        /// <summary>
        /// Updates the extents to include the specified extents.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public void Add(Extents<T> extents)
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
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            initialize();
        }

        /// <summary>
        /// Resets the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void Reset(IEnumerable<T> coordinates)
        {
            Clear();
            Add(coordinates);
        }

        /// <summary>
        /// Determines whether the coordinate lies within the extents.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the extents; otherwise, <c>false</c>.</returns>
        public abstract bool IsWithinExtents(T coordinate);

        /// <summary>
        /// Returns a rectangle boundary of this instance.
        /// </summary>
        /// <returns>NRectangle.</returns>
        public abstract IList<T> Boundary();
    }
}

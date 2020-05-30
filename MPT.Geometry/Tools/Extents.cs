// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-29-2020
// ***********************************************************************
// <copyright file="Extents.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.Math.Coordinates;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Represents the coordinate bounds of a shape or line, or cluster of points.
    /// </summary>
    /// <typeparam name="T">The type of coordinate.</typeparam>
    public abstract class Extents<T> where T : ICoordinate
    {
        #region Properties        
        /// <summary>
        /// The original coordinates
        /// </summary>
        protected IList<T> _originalCoordinates = new List<T>();

        /// <summary>
        /// The maximum allowed Y-coordinate
        /// </summary>
        protected double _maxYLimit = double.PositiveInfinity;
        /// <summary>
        /// The minimum allowed Y-coordinate
        /// </summary>
        protected double _minYLimit = double.NegativeInfinity;
        /// <summary>
        /// The maximum allowed X-coordinate
        /// </summary>
        protected double _maxXLimit = double.PositiveInfinity;
        /// <summary>
        /// The minimum allowed X-coordinate
        /// </summary>
        protected double _minXLimit = double.NegativeInfinity;

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
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public double Width => MaxX - MinX;

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public double Height => MaxY - MinY;
        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}" /> class.
        /// </summary>
        protected Extents()
        {
            initializeEmpty();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="maxYLimit">The maximum y limit.</param>
        /// <param name="minYLimit">The minimum y limit.</param>
        /// <param name="maxXLimit">The maximum x limit.</param>
        /// <param name="minXLimit">The minimum x limit.</param>
        protected Extents(IEnumerable<T> coordinates,
            double maxYLimit = double.PositiveInfinity,
            double minYLimit = double.NegativeInfinity,
            double maxXLimit = double.PositiveInfinity,
            double minXLimit = double.NegativeInfinity)
        {
            initializeLimits(
                maxYLimit, minYLimit,
                maxXLimit, minXLimit);
            if (coordinates.Count() > 1)
            {
                initializeForSetting();
            }
            else
            {
                initializeEmpty();
            }
            AddRange(coordinates);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents{T}" /> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        protected Extents(Extents<T> extents)
        {
            initializeLimits(
                extents._maxYLimit, extents._minYLimit,
                extents._maxXLimit, extents._minXLimit);
            initializeForSetting();
            AddExtents(extents);
        }

        /// <summary>
        /// Initializes the limits.
        /// </summary>
        /// <param name="maxYLimit">The maximum y limit.</param>
        /// <param name="minYLimit">The minimum y limit.</param>
        /// <param name="maxXLimit">The maximum x limit.</param>
        /// <param name="minXLimit">The minimum x limit.</param>
        protected void initializeLimits(
            double maxYLimit = double.PositiveInfinity,
            double minYLimit = double.NegativeInfinity,
            double maxXLimit = double.PositiveInfinity,
            double minXLimit = double.NegativeInfinity)
        {
            _maxYLimit = maxYLimit;
            _minYLimit = minYLimit;
            _maxXLimit = maxXLimit;
            _minXLimit = minXLimit;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initializeEmpty()
        {
            MaxY = _maxYLimit;
            MinY = _minYLimit;
            MaxX = _maxXLimit;
            MinX = _minXLimit;
        }

        /// <summary>
        /// Initializes for setting.
        /// </summary>
        protected void initializeForSetting()
        {
            MaxY = _minYLimit;
            MinY = _maxYLimit;
            MaxX = _minXLimit;
            MinX = _maxXLimit;
        }

        /// <summary>
        /// Determines whether [is extents width set].
        /// </summary>
        /// <returns><c>true</c> if [is extents width set]; otherwise, <c>false</c>.</returns>
        protected bool isExtentsWidthSet()
        {
            return (MaxX != _maxXLimit && MinX != _minXLimit);            
        }

        /// <summary>
        /// Determines whether [is extents height set].
        /// </summary>
        /// <returns><c>true</c> if [is extents height set]; otherwise, <c>false</c>.</returns>
        protected bool isExtentsHeightSet()
        {
            return (MaxY != _maxYLimit && MinY != _minYLimit);
        }
        #endregion

        #region Methods: Public 
        /// <summary>
        /// Adds the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void Add(T coordinate)
        {
            bool xIsInitialized = isExtentsWidthSet();
            bool yIsInitialized = isExtentsHeightSet();
            if (!xIsInitialized && !yIsInitialized)
            {
                if (_originalCoordinates.Count < 1)
                {   // Save coordinate in case a second is added later. These will be enough to then establish extents.
                    _originalCoordinates.Add(coordinate);
                    return;
                }
                initializeForSetting();
                addCoordinate(_originalCoordinates[0]);
                _originalCoordinates.Clear();
            }
            addCoordinate(coordinate);
        }

        /// <summary>
        /// Updates the extents to include the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void AddRange(IEnumerable<T> coordinates)
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
        public void AddExtents(Extents<T> extents)
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
            initializeEmpty();
            initializeLimits();
        }

        /// <summary>
        /// Resets the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void Reset(IEnumerable<T> coordinates)
        {
            Clear();
            AddRange(coordinates);
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

        /// <summary>
        /// Gets the geometric center.
        /// </summary>
        /// <returns>T.</returns>
        /// <value>The geometric center.</value>
        public abstract T GeometricCenter();

        /// <summary>
        /// Translates points that define the extents.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>Extents&lt;T&gt;.</returns>
        public abstract Extents<T> Translate(double x, double y);

        /// <summary>
        /// Rotates points that define the extents.
        /// </summary>
        /// <param name="angle">The angle [radians].</param>
        /// <returns>Extents&lt;T&gt;.</returns>
        public abstract Extents<T> Rotate(double angle);
        #endregion

        #region Methods: Protected
        /// <summary>
        /// Adds the coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        protected abstract void addCoordinate(T coordinate);
        #endregion
    }
}

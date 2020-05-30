using System;
using System.Collections.Generic;
using System.Linq;
using MPT.Math.Coordinates;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Class PointBoundary.
    /// Implements the <see cref="MPT.Geometry.Tools.Boundary{CartesianCoordinate}" />
    /// </summary>
    /// <seealso cref="MPT.Geometry.Tools.Boundary{CartesianCoordinate}" />
    public class PointBoundary : Boundary<CartesianCoordinate>
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="PointBoundary"/> class.
        /// </summary>
        public PointBoundary()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointBoundary"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public PointBoundary(IEnumerable<CartesianCoordinate> coordinates) : base(coordinates)
        {
        }
        #endregion

        #region Methods: Public        
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear()
        {
            _coordinates = new List<CartesianCoordinate>();
        }

        /// <summary>
        /// Adds to boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public override void AddRange(IList<CartesianCoordinate> coordinates)
        {
            List<CartesianCoordinate> list = new List<CartesianCoordinate>(_coordinates);
            list.AddRange(coordinates);
            _coordinates = list;
        }

        /// <summary>
        /// Removes from boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public override void RemoveRange(IList<CartesianCoordinate> coordinates)
        {
            if (_coordinates.Intersect(coordinates).Count() == 0) { return; }

            _coordinates = _coordinates.Where(existing => !coordinates.Any(remove => existing.Equals(remove))); 
        }
        #endregion
    }
}

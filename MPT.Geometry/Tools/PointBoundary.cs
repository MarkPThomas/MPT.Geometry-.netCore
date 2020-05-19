using System;
using System.Collections.Generic;

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
           // _coordinates = coordinates;
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

        // TODO: For all boundary changes, clusters and holes occur when a shape is entirely outside of or inside of the shape group.
        // This is to be handled by linking the shape areas by a collinear segment.
        // All positive shapes are determined by CCW travel.
        // All negative shapes are determined by CW travel.

        // TODO: Finish
        /// <summary>
        /// Adds to boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Add(IList<CartesianCoordinate> coordinates)
        {
            throw new NotImplementedException();
        }

        // TODO: Finish
        /// <summary>
        /// Removes from boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Remove(IList<CartesianCoordinate> coordinates)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

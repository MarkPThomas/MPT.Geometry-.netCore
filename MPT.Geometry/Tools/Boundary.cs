// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-29-2020
// ***********************************************************************
// <copyright file="Boundary.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using GL = MPT.Geometry.GeometryLibrary;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Represents boundary coordinates.
    /// </summary>
    /// <typeparam name="T">The type of coordinate.</typeparam>
    public abstract class Boundary<T> where T : ICoordinate
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; } = GL.ZeroTolerance;

        /// <summary>
        /// The coordinates.
        /// </summary>
        protected IEnumerable<T> _coordinates;
        /// <summary>
        /// The coordinates that compose the boundary.
        /// </summary>
        /// <value>The boundary.</value>
        public IList<T> Coordinates => new ReadOnlyCollection<T>(_coordinates.ToList());
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Boundary{T}" /> class.
        /// </summary>
        protected Boundary()
        {
            _coordinates = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Boundary{T}" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        protected Boundary(IEnumerable<T> coordinates)
        {
            _coordinates = new List<T>(coordinates);
        }
        #endregion

        #region Methods: Public        
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Resets the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void Reset(IList<T> coordinates)
        {
            Clear();
            AddRange(coordinates);
        }

        /// <summary>
        /// Adds to boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public abstract void AddRange(IList<T> coordinates);

        /// <summary>
        /// Removes from boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public abstract void RemoveRange(IList<T> coordinates);

        // TODO: Add (single value)
        // TODO: Remove (single value)
        // TODO: Replace (single value)
        // TODO: InsertByIndex (single value or list/'Range')
        // TODO: RemoveByIndex (single value)
        // TODO: ReplaceByIndex (single value)

        // TODO: For all boundary changes, clusters and holes occur when a shape is entirely outside of or inside of the shape group.
        // This is to be handled by linking the shape areas by a collinear segment, which requires duplicating coordinates.
        // All positive shapes are determined by CCW travel.
        // All negative shapes are determined by CW travel.
        #endregion

        #region Methods: Private

        #endregion
    }
}

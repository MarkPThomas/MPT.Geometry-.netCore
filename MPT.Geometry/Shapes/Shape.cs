// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Shape.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Collections.ObjectModel;

using MPT.Geometry.Tools;
using MPT.Math;

namespace MPT.Geometry.Shapes
{
    /// <summary>
    /// Base abstract Shape.
    /// </summary>
    public abstract class Shape
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; } = GeometryLibrary.ZeroTolerance;

        /// <summary>
        /// The name of the shape.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        ///// <summary>
        ///// The boundary
        ///// </summary>
        //protected Boundary _boundary = new Boundary();
        ///// <summary>
        ///// The boundary coordinates that wraps the shape.
        ///// This assumes straight lines between vertices.
        ///// </summary>
        ///// <value>The boundary.</value>
        //public IList<CartesianCoordinate> Boundary => new ReadOnlyCollection<CartesianCoordinate>(_boundary.Coordinates);

        ///// <summary>
        ///// The extents
        ///// </summary>
        //protected Extents _extents = new Extents();
        ///// <summary>
        ///// The extents bounding box of the shape.
        ///// </summary>
        ///// <value>The extents.</value>
        //public IList<CartesianCoordinate> Extents => _extents.Boundary();

        /// <summary>
        /// If true, the shape is considered to be a hole, otherwise it is a solid.
        /// </summary>
        public bool IsHole { get; set; }
        #endregion        
        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        protected Shape()
        {
           // _boundary.Tolerance = Tolerance;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
        }
    }
}

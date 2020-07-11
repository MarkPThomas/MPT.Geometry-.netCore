// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark P Thomas
// Created          : 06-28-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 06-30-2020
// ***********************************************************************
// <copyright file="Triangle306090.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using Numbers = MPT.Math.Numbers;

namespace MPT.Geometry.Shapes
{
    /// <summary>
    /// A triangle of angles 30°, 60° &amp; 90°.
    /// Implements the <see cref="MPT.Geometry.Shapes.RightTriangle" />
    /// </summary>
    /// <seealso cref="MPT.Geometry.Shapes.RightTriangle" />
    public class Triangle306090 : RightTriangle
    {
        #region Properties        
        /// <summary>
        /// The angle, α, which is opposite of side a and is 30°.
        /// </summary>
        /// <value>The alpha.</value>
        public override Angle AngleA => Numbers.PiOver6;

        /// <summary>
        /// The angle, β, which is opposite of side b and is 60°.
        /// </summary>
        /// <value>The beta.</value>
        public override Angle AngleB => Numbers.PiOver3;

        /// <summary>
        /// The angle, γ, which is opposite of side c and is 90°.
        /// </summary>
        /// <value>The beta.</value>
        public override Angle AngleC => Numbers.PiOver2;

        /// <summary>
        /// Length of the base/horizontal side, b.
        /// </summary>
        /// <value>The b.</value>
        public override double b => getWidth(a);

        /// <summary>
        /// Length of the hypotenuse side, c.
        /// </summary>
        /// <value>The c.</value>
        public override double c => getHypotenuse(a);

        /// <summary>
        /// Gets the inradius, r, which describes a circle whose edge is tangent to all 3 sides of the triangle.
        /// </summary>
        /// <value>The in radius.</value>
        public override double InRadius => 0.25 * a * (3.Sqrt() - 1);
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle306090" /> class.
        /// </summary>
        /// <param name="heightA">The height a.</param>
        public Triangle306090(double heightA) : base(getWidth(heightA), heightA)
        {

        }
        #endregion

        #region Methods 
        /// <summary>
        /// Area of the shape.
        /// </summary>
        /// <returns></returns>
        public override double Area()
        {
            return a.Squared() * 3.Sqrt() / 8;
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <param name="heightA">The height a.</param>
        /// <returns>System.Double.</returns>
        private static double getWidth(double heightA)
        {
            return 0.5 * heightA * 3.Sqrt();
        }

        /// <summary>
        /// Gets the hypotenuse.
        /// </summary>
        /// <param name="heightA">The height a.</param>
        /// <returns>System.Double.</returns>
        private static double getHypotenuse(double heightA)
        {
            return 0.5 * heightA;
        }
        #endregion
    }
}

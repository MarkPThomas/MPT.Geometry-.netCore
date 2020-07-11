using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MPT.Geometry.Tools;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Shapes
{
    [TestFixture]
    public static class IsoscelesTriangleTests
    {
        #region Initialization        
        ///// <summary>
        ///// Initializes a new instance of the <see cref="IsoscelesTriangleTests" /> class.
        ///// </summary>
        ///// <param name="lengthsEqualA">Length of sides of equal length, a.</param>
        ///// <param name="lengthB">The length b, of the base of the triangle. This is the unequal length.</param>
        //public IsoscelesTriangleTests(double lengthsEqualA, double lengthB)
        //{
        //    _sidesEqual = lengthsEqualA.Abs();
        //    _sideUnequal = lengthB.Abs();
        //    SetCoordinates(FormulateCoordinates());
        //    setInCenter();
        //    setCircumcenter();
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="IsoscelesTriangleTests" /> class.
        ///// </summary>
        ///// <param name="apexCoordinate">The apex coordinate.</param>
        //public IsoscelesTriangleTests(CartesianCoordinate apexCoordinate)
        //{
        //    double alphaRadians = getAlpha(apexCoordinate.X, apexCoordinate.Y);
        //    _sidesEqual = apexCoordinate.X * Trig.Cos(alphaRadians);
        //    _sideUnequal = 2 * apexCoordinate.X;
        //    SetCoordinates(FormulateCoordinates());
        //    setInCenter();
        //    setCircumcenter();
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="IsoscelesTriangleTests" /> class.
        ///// </summary>
        ///// <param name="lengthB">The length b, of the base of the triangle. This is the unequal length.</param>
        ///// <param name="anglesEqualAlpha">The two angles of equal magnitude, α, which are opposite of the two sides of equal length.</param>
        //public IsoscelesTriangleTests(double lengthB, Angle anglesEqualAlpha)
        //{
        //    _sidesEqual = 0.5 * lengthB / Trig.Cos(anglesEqualAlpha.Radians);
        //    _sideUnequal = lengthB;
        //    SetCoordinates(FormulateCoordinates());
        //    setInCenter();
        //    setCircumcenter();
        //}
        #endregion

        #region Methods   
        ///// <summary>
        ///// Area of the shape.
        ///// </summary>
        ///// <returns></returns>
        //public override double Area()
        //{
        //    return 0.25 * b * (4 * a.Squared() - b.Squared()).Sqrt();
        //}

        ///// <summary>
        ///// Formulates the coordinates for the shape.
        ///// </summary>
        ///// <returns>IList&lt;CartesianCoordinate&gt;.</returns>
        //public IList<CartesianCoordinate> FormulateCoordinates()
        //{
        //    return new List<CartesianCoordinate>()
        //    {
        //        new CartesianCoordinate(0, 0),
        //        new CartesianCoordinate(_sideUnequal, 0),
        //        new CartesianCoordinate(_sideUnequal / 2, getHeight()),
        //    };
        //}
        #endregion
    }
}

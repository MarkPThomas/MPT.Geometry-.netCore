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
    public static class EquilateralTriangleTests 
    {
        #region Initialization        
        ///// <summary>
        ///// Initializes a new instance of the <see cref="EquilateralTriangleTests" /> class.
        ///// </summary>
        ///// <param name="sideLength">Length of the side.</param>
        //public EquilateralTriangleTests(double sideLength) 
        //{
        //    _sidesEqual = sideLength.Abs();
        //    SetCoordinates(FormulateCoordinates());
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="EquilateralTriangleTests" /> class.
        ///// </summary>
        ///// <param name="apexCoordinate">The apex coordinate.</param>
        //public EquilateralTriangleTests(CartesianCoordinate apexCoordinate)
        //{
        //    _sidesEqual = 2 * apexCoordinate.X;
        //    SetCoordinates(FormulateCoordinates());
        //}
        #endregion

        #region Methods      
        ///// <summary>
        ///// Area of the shape.
        ///// </summary>
        ///// <returns></returns>
        //public override double Area()
        //{
        //    return 0.25 * _sidesEqual.Squared() * 3.Sqrt();
        //}

        ///// <summary>
        ///// Length of all sides of the shape.
        ///// </summary>
        ///// <returns></returns>
        //public override double Perimeter()
        //{
        //    return 3 * _sidesEqual;
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
        //        new CartesianCoordinate(_sidesEqual, 0),
        //        new CartesianCoordinate(_sidesEqual / 2, getHeight(_sidesEqual)),
        //    };
        //}
        #endregion
    }
}

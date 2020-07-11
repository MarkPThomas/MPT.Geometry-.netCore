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
    public static class RightTriangleTests 
    {
        #region Initialization        
        ///// <summary>
        ///// Initializes a new instance of the <see cref="RightTriangleTests" /> class.
        ///// </summary>
        ///// <param name="width">The width.</param>
        ///// <param name="height">The height.</param>
        //public RightTriangleTests(double width, double height)
        //{
        //    a = height.Abs();
        //    b = width.Abs();
        //    SetCoordinates(FormulateCoordinates());
        //    setInCenter();
        //    setCircumcenter();
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="RightTriangleTests" /> class.
        ///// </summary>
        ///// <param name="apexCoordinate">The apex coordinate.</param>
        //public RightTriangleTests(CartesianCoordinate apexCoordinate)
        //{
        //    a = apexCoordinate.Y;
        //    b = apexCoordinate.X;
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
        //    return 0.5 * b * a;
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
        //        new CartesianCoordinate(b, 0),
        //        new CartesianCoordinate(b, a),
        //    };
        //}
        #endregion
    }
}

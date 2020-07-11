using System.Collections.Generic;
using MPT.Geometry.Shapes;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Shapes
{
    // TODO: Handle Centroid & similar coordinates for transformations
    [TestFixture]
    public static class TriangleTests 
    {
        #region Initialization   

        ///// <summary>
        ///// Initializes a new instance of the <see cref="QuadrilateralTests" /> class.
        ///// </summary>
        ///// <param name="pointA">The point opposite side a.</param>
        ///// <param name="pointB">The point opposite side b.</param>
        ///// <param name="pointC">The point opposite side c.</param>
        //public TriangleTests(
        //    CartesianCoordinate pointA,
        //    CartesianCoordinate pointB,
        //    CartesianCoordinate pointC) : base(new List<CartesianCoordinate>() { pointC, pointB, pointA })
        //{
        //    setSides();
        //    setCenterCoordinates();

        //    setInRadius();
        //    setCircumradius();
        //}
        #endregion

        #region Methods: Properties
        ///// <summary>
        ///// Area of the shape.
        ///// </summary>
        ///// <returns></returns>
        //public override double Area()
        //{
        //    return (SemiPerimeter * (SemiPerimeter - a) * (SemiPerimeter - b) * (SemiPerimeter - c)).Sqrt();
        //}

        ///// <summary>
        ///// Length of all sides of the shape.
        ///// </summary>
        ///// <returns></returns>
        //public override double Perimeter()
        //{
        //    return a + b + c;
        //}
        #endregion

        #region Methods: Altitudes       
        ///// <summary>
        ///// The length of altitude line A, which spans from point A to a perpendicular intersection with side A.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double AltitudeLengthA()
        //{
        //    return 2 * Area() / a;
        //}

        ///// <summary>
        ///// The coordinate where altitude line A intersects side A.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate AltitudeCoordinateA()
        //{
        //    return projectionIntersection(PointA, SideA, OrthoCenter);
        //}

        ///// <summary>
        ///// The line which spans from point A to a perpendicular intersection with side A.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment AltitudeLineA()
        //{
        //    return new LineSegment(PointA, AltitudeCoordinateA());
        //}

        ///// <summary>
        ///// The length of altitude line B, which spans from point B to a perpendicular intersection with side B.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double AltitudeLengthB()
        //{
        //    return 2 * Area() / b;
        //}

        ///// <summary>
        ///// The coordinate where altitude line B intersects side B.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate AltitudeCoordinateB()
        //{
        //    return projectionIntersection(PointB, SideB, OrthoCenter);
        //}

        ///// <summary>
        ///// The line which spans from point B to a perpendicular intersection with side B.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment AltitudeLineB()
        //{
        //    return new LineSegment(PointB, AltitudeCoordinateB());
        //}

        ///// <summary>
        ///// The length of altitude line C, which spans from point C to a perpendicular intersection with side C.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double AltitudeLengthC()
        //{
        //    return 2 * Area() / c;
        //}

        ///// <summary>
        ///// The coordinate where altitude line C intersects side C.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate AltitudeCoordinateC()
        //{
        //    return projectionIntersection(PointC, SideC, OrthoCenter);
        //}

        ///// <summary>
        ///// The line which spans from point C to a perpendicular intersection with side C.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment AltitudeLineC()
        //{
        //    return new LineSegment(PointC, AltitudeCoordinateC());
        //}
        #endregion

        #region Methods: Medians
        ///// <summary>
        ///// The length of median line A, which spans from point A to an intersection at the midpoint of side A.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double MedianLengthA()
        //{
        //    return 0.5 * (2 * b.Squared() + 2 * c.Squared() - a.Squared()).Sqrt();
        //}

        ///// <summary>
        ///// The coordinate where median line A intersects side A.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate MedianCoordinateA()
        //{
        //    return projectionIntersection(PointA, SideA, Centroid);
        //}

        ///// <summary>
        ///// The line which spans from point A to an intersection at the midpoint of side A.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment MedianLineA()
        //{
        //    return new LineSegment(PointA, MedianCoordinateA());
        //}

        ///// <summary>
        ///// The length of median line B, which spans from point B to an intersection at the midpoint of side B.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double MedianLengthB()
        //{
        //    return 0.5 * (2 * a.Squared() + 2 * c.Squared() - b.Squared()).Sqrt();
        //}

        ///// <summary>
        ///// The coordinate where median line B intersects side B.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate MedianCoordinateB()
        //{
        //    return projectionIntersection(PointB, SideB, Centroid);
        //}

        ///// <summary>
        ///// The line which spans from point B to an intersection at the midpoint of side B.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment MedianLineB()
        //{
        //    return new LineSegment(PointB, MedianCoordinateB());
        //}

        ///// <summary>
        ///// The length of median line C, which spans from point C to an intersection at the midpoint of side C.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double MedianLengthC()
        //{
        //    return 0.5 * (2 * b.Squared() + 2 * a.Squared() - c.Squared()).Sqrt();
        //}

        ///// <summary>
        ///// The coordinate where median line C intersects side C.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate MedianCoordinateC()
        //{
        //    return projectionIntersection(PointC, SideC, Centroid);
        //}

        ///// <summary>
        ///// The line which spans from point C to an intersection at the midpoint of side C.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment MedianLineC()
        //{
        //    return new LineSegment(PointC, MedianCoordinateC());
        //}
        #endregion

        #region Methods: Angle Bisectors
        ///// <summary>
        ///// The length of angle bisector line A, which spans from point A to an intersection with side A such that angle A is bisected.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double AngleBisectorA()
        //{
        //    return (b * c * (1 - (a / (b + c)).Squared())).Sqrt();
        //}

        ///// <summary>
        ///// The coordinate where angle bisector line A intersects side A.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate AngleBisectorCoordinateA()
        //{
        //    return projectionIntersection(PointA, SideA, InCenter);
        //}

        ///// <summary>
        ///// The line which spans from point A to an intersection with side A such that angle A is bisected.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment AngleBisectorLineA()
        //{
        //    return new LineSegment(PointA, AngleBisectorCoordinateA());
        //}

        ///// <summary>
        ///// The length of angle bisector line B, which spans from point B to an intersection with side B such that angle B is bisected.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double AngleBisectorB()
        //{
        //    return (a * c * (1 - (b / (a + c)).Squared())).Sqrt();
        //}

        ///// <summary>
        ///// The coordinate where angle bisector line B intersects side B.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate AngleBisectorCoordinateB()
        //{
        //    return projectionIntersection(PointB, SideB, InCenter);
        //}

        ///// <summary>
        ///// The line which spans from point B to an intersection with side B such that angle B is bisected.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment AngleBisectorLineB()
        //{
        //    return new LineSegment(PointB, AngleBisectorCoordinateB());
        //}

        ///// <summary>
        ///// The length of angle bisector line C, which spans from point C to an intersection with side C such that angle C is bisected.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double AngleBisectorC()
        //{
        //    return (b * a * (1 - (c / (b + a)).Squared())).Sqrt();
        //}

        ///// <summary>
        ///// The coordinate where angle bisector line C intersects side C.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate AngleBisectorCoordinateC()
        //{
        //    return projectionIntersection(PointC, SideC, InCenter);
        //}

        ///// <summary>
        ///// The line which spans from point C to an intersection with side C such that angle C is bisected.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment AngleBisectorLineC()
        //{
        //    return new LineSegment(PointC, AngleBisectorCoordinateC());
        //}
        #endregion

        #region Methods: Perpendicular Side Bisectors
        ///// <summary>
        ///// The length of perpendicular side bisector line A, which spans from the circumcenter to a perpendicular intersection with side A such that side A is bisected.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double PerpendicularSideBisectorA()
        //{
        //    setPerpendicularSideBisectors();
        //    return 2 * _A * Area() / (_A.Squared() + _B.Squared() - _C.Squared());
        //}

        ///// <summary>
        ///// The coordinate where perpendicular side bisector line A intersects side A.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate PerpendicularSideBisectorCoordinateA()
        //{
        //    return projectionIntersection(PointA, SideA, CircumCenter);
        //}

        ///// <summary>
        ///// The line which spans from the circumcenter to a perpendicular intersection with side A such that side A is bisected.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment PerpendicularSideBisectorLineA()
        //{
        //    return new LineSegment(PointA, PerpendicularSideBisectorCoordinateA());
        //}

        ///// <summary>
        ///// The length of perpendicular side bisector line B, which spans from the circumcenter to a perpendicular intersection with side B such that side B is bisected.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double PerpendicularSideBisectorB()
        //{
        //    setPerpendicularSideBisectors();
        //    return 2 * _B * Area() / (_A.Squared() + _B.Squared() - _C.Squared());
        //}

        ///// <summary>
        ///// The coordinate where perpendicular side bisector line BA intersects side B.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate PerpendicularSideBisectorCoordinateB()
        //{
        //    return projectionIntersection(PointB, SideB, CircumCenter);
        //}

        ///// <summary>
        ///// The line which spans from the circumcenter to a perpendicular intersection with side B such that side B is bisected.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment PerpendicularSideBisectorLineB()
        //{
        //    return new LineSegment(PointB, PerpendicularSideBisectorCoordinateB());
        //}

        ///// <summary>
        ///// The length of perpendicular side bisector line C, which spans from the circumcenter to a perpendicular intersection with side C such that side C is bisected.
        ///// </summary>
        ///// <returns>System.Double.</returns>
        //public double PerpendicularSideBisectorC()
        //{
        //    setPerpendicularSideBisectors();
        //    return 2 * _C * Area() / (_A.Squared() - _B.Squared() + _C.Squared());
        //}

        ///// <summary>
        ///// The coordinate where perpendicular side bisector line C intersects side C.
        ///// </summary>
        ///// <returns>CartesianCoordinate.</returns>
        //public CartesianCoordinate PerpendicularSideBisectorCoordinateC()
        //{
        //    return projectionIntersection(PointC, SideC, CircumCenter);
        //}

        ///// <summary>
        ///// The line which spans from the circumcenter to a perpendicular intersection with side C such that side C is bisected.
        ///// </summary>
        ///// <returns>LineSegment.</returns>
        //public LineSegment PerpendicularSideBisectorLineC()
        //{
        //    return new LineSegment(PointC, PerpendicularSideBisectorCoordinateC());
        //}
        #endregion
    }
}

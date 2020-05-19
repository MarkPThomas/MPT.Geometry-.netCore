// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="PointIntersection.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using NMath = System.Math;

using MPT.Math;
using GL = MPT.Geometry.GeometryLibrary;
using hProjection = MPT.Geometry.Intersection.ProjectionHorizontal;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Geometry.Intersection
{
    /// <summary>
    /// Handles calculations related to point intersections.
    /// </summary>
    public static class PointIntersection
    {
        /// <summary>
        /// Determines if the points overlap.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the points lie in the same position, <c>false</c> otherwise.</returns>
        public static bool PointsOverlap(
            CartesianCoordinate point1,
            CartesianCoordinate point2,
            double tolerance = GL.ZeroTolerance)
        {
            return ((NMath.Abs(point1.X - point2.X) < tolerance && 
                     NMath.Abs(point1.Y - point2.Y) < tolerance));
        }

        /// <summary>
        /// Determines whether the specified location is within the shape.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="shapeBoundary">The shape boundary composed of n points.</param>
        /// <param name="includePointOnSegment"></param>
        /// <param name="incluePointOnVertex"></param>
        /// <returns><c>true</c> if the specified location is within the shape; otherwise, <c>false</c>.</returns>
        public static bool IsWithinShape(
            CartesianCoordinate coordinate, 
            CartesianCoordinate[] shapeBoundary,
            bool includePointOnSegment = true,
            bool incluePointOnVertex = true)
        {
            // 3. If # intersections%2 == 0 (even) => point is outside.
            //    If # intersections%2 == 1 (odd) => point is inside.
            // Note: Condition of vertex intersection (# == 1) is not handled, so is treated as inside by default.
            return (hProjection.NumberOfIntersections(
                                    coordinate, 
                                    shapeBoundary,
                                    includePointOnSegment,
                                    incluePointOnVertex).IsOdd());
        }
    }
}

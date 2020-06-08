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
using GL = MPT.Geometry.GeometryLibrary;
using hProjection = MPT.Geometry.Intersections.ProjectionHorizontal;
using vProjection = MPT.Geometry.Intersections.ProjectionVertical;
using Segment = MPT.Geometry.Segments.LineSegment;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using MPT.Math;

namespace MPT.Geometry.Intersections
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
        /// <returns><c>true</c> if the points lie in the same position, <c>false</c> otherwise.</returns>
        public static bool IsOnPoint(
            CartesianCoordinate point1,
            CartesianCoordinate point2)
        {
            return point1 == point2;
            //tolerance = Helper.GetTolerance(point1, point2, tolerance);
            //return (point1.X.IsEqualTo(point2.X, tolerance) &&
            //        point1.Y.IsEqualTo(point2.Y, tolerance));
        }

        /// <summary>
        /// Determines whether the specified location is on the shape.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="shapeBoundary">The shape boundary.</param>
        /// <returns><c>true</c> if [is on shape] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        public static bool IsOnShape(
            CartesianCoordinate coordinate,
            CartesianCoordinate[] shapeBoundary)
        {
            for (int i = 0; i < shapeBoundary.Length - 1; i++)
            {
                Segment segment = new Segment(shapeBoundary[i], shapeBoundary[i + 1]);
                if (segment.IncludesCoordinate(coordinate))
                {
                    return true;
                }
            }
            return false;
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

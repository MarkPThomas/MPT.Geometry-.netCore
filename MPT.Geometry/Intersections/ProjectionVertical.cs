// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="VerticalProjection.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

using Segment = MPT.Geometry.Segments.LineSegment;
using MPT.Math.Coordinates;
using MPT.Math.Curves;

namespace MPT.Geometry.Intersections
{
    /// <summary>
    /// Handles calculations related to vertical projections.
    /// </summary>
    public static class ProjectionVertical
    {
        /// <summary>
        /// The numbers of shape boundary intersections a vertical line makes when projecting to the top from the provided point.
        /// If the point is on a vertex or segment, the function returns either 0 or 1.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="shapeBoundary">The shape boundary composed of n points.</param>
        /// <param name="includePointOnSegment">if set to <c>true</c> [include point on segment].</param>
        /// <param name="includePointOnVertex">if set to <c>true</c> [include point on vertex].</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Int32.</returns>
        public static int NumberOfIntersections(
            CartesianCoordinate coordinate,
            CartesianCoordinate [] shapeBoundary,
            bool includePointOnSegment = true,
            bool includePointOnVertex = true,
            double tolerance = GeometryLibrary.ZeroTolerance)
        {
            if (shapeBoundary[0] != shapeBoundary[shapeBoundary.Length - 1])
                throw new ArgumentException("Shape boundary describes a shape. Closure to the shape boundary is needed.");

            // 1. Check vertical line projection from a pt. n to the top
            // 2. Count # of intersections of the line with shape edges   

            // Note shape coordinates from XML already repeat starting node as an ending node.
            // No need to handle wrap-around below.

            int numberOfIntersections = 0;
            for (int i = 0; i < shapeBoundary.Length - 1; i++)
            {
                CartesianCoordinate vertexI = shapeBoundary[i];
                if (PointIntersection.IsOnPoint(coordinate, vertexI))
                {
                    return includePointOnVertex ? 1 : 0;
                }

                CartesianCoordinate vertexJ = shapeBoundary[i + 1];

                if (!PointIsBelowSegmentBottom(coordinate.X, vertexI, vertexJ))
                {
                    // Pt is above the segment.
                    continue;
                }
                bool pointIsWithinSegmentWidth = PointIsWithinSegmentWidth(
                                                    coordinate.X, 
                                                    vertexI, vertexJ, 
                                                    includeEnds: includePointOnSegment);
                if (!pointIsWithinSegmentWidth)
                {
                    // CartesianCoordinate is out of horizontal bounds of the segment extents.
                    continue;
                }
                bool pointIsWithinSegmentHeight = ProjectionHorizontal.PointIsWithinSegmentHeight(
                                                    coordinate.Y, 
                                                    vertexI, vertexJ, 
                                                    includeEnds: includePointOnSegment);
                if (LinearCurve.IsHorizontal(vertexI, vertexJ))
                {
                    if (pointIsWithinSegmentHeight)
                    { // CartesianCoordinate is on horizontal segment
                        return includePointOnSegment ? 1 : 0;
                    }
                    // CartesianCoordinate hits horizontal segment
                    numberOfIntersections++;
                    continue;
                }
                if (LinearCurve.IsVertical(vertexI, vertexJ))
                {   // Segment would be parallel to line projection.
                    // CartesianCoordinate is collinear since it is within segment height
                    if (pointIsWithinSegmentHeight)
                    { // CartesianCoordinate is on vertical segment
                        return includePointOnSegment ? 1 : 0;
                    }
                    continue;
                }

                double yIntersection = IntersectionPointY(coordinate.X, vertexI, vertexJ);
                if (PointIsBelowSegmentIntersection(coordinate.Y, yIntersection, vertexI, vertexJ))
                {
                    numberOfIntersections++;
                }
                else if (NMath.Abs(coordinate.Y - yIntersection) < tolerance)
                { // CartesianCoordinate is on sloped segment
                    return includePointOnSegment ? 1 : 0;
                }
            }
            return numberOfIntersections;
        }



        /// <summary>
        /// Determines if the point lies within the segment width.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="ptI">The vertex i.</param>
        /// <param name="ptJ">The vertex j.</param>
        /// <param name="includeEnds">if set to <c>true</c> [include ends].</param>
        /// <returns><c>true</c> if the point lies within the segment width, <c>false</c> otherwise.</returns>
        public static bool PointIsWithinSegmentWidth(
            double xPtN,
            CartesianCoordinate ptI,
            CartesianCoordinate ptJ,
            bool includeEnds = true)
        {
            if (includeEnds)
            {
                return (NMath.Min(ptI.X, ptJ.X) <= xPtN && xPtN <= NMath.Max(ptI.X, ptJ.X));
            }
            return (NMath.Min(ptI.X, ptJ.X) < xPtN && xPtN < NMath.Max(ptI.X, ptJ.X));
        }

        /// <summary>
        /// Determines if the point lies below the segment end.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="ptI">Vertex i.</param>
        /// <param name="ptJ">Vertex j.</param>
        /// <param name="includeEnds">if set to <c>true</c> [include ends].</param>
        /// <returns><c>true</c> if the point lies to below the segment bottom, <c>false</c> otherwise.</returns>
        public static bool PointIsBelowSegmentBottom(
            double yPtN,
            CartesianCoordinate ptI,
            CartesianCoordinate ptJ,
            bool includeEnds = true)
        {
            if (includeEnds)
            {
                return yPtN <= NMath.Max(ptI.Y, ptJ.Y);
            }
            return yPtN < NMath.Max(ptI.Y, ptJ.Y);
        }

        /// <summary>
        /// The y-coordinate of the intersection of the vertically projected line with the provided segment.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n, where the projection starts.</param>
        /// <param name="ptI">Vertex i of the segment.</param>
        /// <param name="ptJ">Vertex j of the segment.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Segment is vertical, so intersection point is either infinity or NAN.</exception>
        public static double IntersectionPointY(
            double xPtN,
            CartesianCoordinate ptI,
            CartesianCoordinate ptJ)
        {
            if (LinearCurve.IsVertical(ptI, ptJ))
                throw new ArgumentException("Segment is vertical, so intersection point is either infinity or NAN.");
            return (xPtN - ptI.X) * ((ptJ.Y - ptI.Y) / (ptJ.X - ptI.X)) + ptI.Y;
        }

        /// <summary>
        /// Determines if the point is below the vertically projected segment intersection.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="yIntersection">The y-coordinate of the intersection of the projected line.</param>
        /// <param name="vertexI">The vertex i.</param>
        /// <param name="vertexJ">The vertex j.</param>
        /// <param name="includeEnds">if set to <c>true</c> [include ends].</param>
        /// <returns><c>true</c> if the point is below the vertically projected segment intersection, <c>false</c> otherwise.</returns>
        public static bool PointIsBelowSegmentIntersection(
            double yPtN, 
            double yIntersection,
            CartesianCoordinate vertexI,
            CartesianCoordinate vertexJ,
            bool includeEnds = true)
        {
            return (yPtN < yIntersection &&
                ProjectionHorizontal.PointIsWithinSegmentHeight(yIntersection, vertexI, vertexJ, includeEnds: includeEnds));
        }
    }
}

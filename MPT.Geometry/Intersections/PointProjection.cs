﻿// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-10-2020
// ***********************************************************************
// <copyright file="PointBoundary.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

using MPT.Math.Coordinates;
using MPT.Geometry.Segments;
using MPT.Math.Curves;
using MPT.Geometry.Tools;

namespace MPT.Geometry.Intersections
{
    /// <summary>
    /// Handles calculations related to horizontal projections.
    /// </summary>
    public static class PointProjection
    {
        #region Number of Intersections
        ///// <summary>
        ///// The numbers of shape boundary intersections a horizontal line makes when projecting to the right from the provided point.
        ///// If the point is on a vertex or segment, the function returns either 0 or 1.
        ///// </summary>
        ///// <param name="coordinate">The coordinate.</param>
        ///// <param name="shapeBoundary">The shape boundary composed of n points.</param>
        ///// <param name="includePointOnSegment">if set to <c>true</c> [include point on segment].</param>
        ///// <param name="includePointOnVertex">if set to <c>true</c> [include point on vertex].</param>
        ///// <param name="tolerance">The tolerance.</param>
        ///// <returns>System.Int32.</returns>
        ///// <exception cref="System.ArgumentException">Shape boundary describes a shape. Closure to the shape boundary is needed.</exception>
        //public static int NumberOfIntersectionsOnHorizontalProjection(
        //    CartesianCoordinate coordinate, 
        //    CartesianCoordinate [] shapeBoundary,
        //    bool includePointOnSegment = true,
        //    bool includePointOnVertex = true,
        //    double tolerance = GeometryLibrary.ZeroTolerance)
        //{
        //    if (shapeBoundary[0] != shapeBoundary[shapeBoundary.Length - 1])
        //        throw new ArgumentException("Shape boundary describes a shape. Closure to the shape boundary is needed.");

        //    // 1. Check horizontal line projection from a pt. n to the right
        //    // 2. Count # of intersections of the line with shape edges   
            
        //    // Note shape coordinates from XML already repeat starting node as an ending node.
        //    // No need to handle wrap-around below.

        //    int numberOfIntersections = 0;
        //    for (int i = 0; i < shapeBoundary.Length - 1; i++)
        //    {
        //        CartesianCoordinate vertexI = shapeBoundary[i];
        //        if (PointIntersection.IsOnPoint(coordinate, vertexI))
        //        {
        //            return 0;
        //        }

        //        CartesianCoordinate vertexJ = shapeBoundary[i + 1];
        //        LineSegment segment = new LineSegment(vertexI, vertexJ);
        //        LinearCurve segmentCurve = segment.Curve;
        //        if (segment.IncludesCoordinate(coordinate))
        //        {
        //            return 0;
        //        }
        //        if (segmentCurve.IsHorizontal())
        //        {   // Segment would be parallel or collinear to line projection.
        //            continue;
        //        }


        //        if (!LineToLineIntersection.PointIsLeftOfLineEndExclusive(coordinate.X, vertexI, vertexJ))
        //        {   // Pt is to the right of the segment.
        //            continue;
        //        }
        //        bool pointIsWithinSegmentHeight = LineToLineIntersection.PointIsWithinLineHeightInclusive(
        //                                            coordinate.Y, 
        //                                            vertexI, vertexJ);
        //        if (!pointIsWithinSegmentHeight)
        //        {   // CartesianCoordinate is out of vertical bounds of the segment extents.
        //            continue;
        //        }
        //        //bool pointIsWithinSegmentWidth = ProjectionVertical.PointIsWithinSegmentWidth(
        //        //                                    coordinate.X, 
        //        //                                    vertexI, vertexJ, 
        //        //                                    includeEnds: includePointOnSegment);
        //        if (segmentCurve.IsVertical())
        //        {
        //            //if (pointIsWithinSegmentWidth)
        //            //{ // CartesianCoordinate is on vertical segment
        //            //    return includePointOnSegment ? 1 : 0;
        //            //}
        //            //// CartesianCoordinate hits vertical segment
        //            numberOfIntersections++;
        //            continue;
        //        }
        //        //if (segment.IsHorizontal())
        //        //{   // Segment would be parallel to line projection.
        //        //    // CartesianCoordinate is collinear since it is within segment height
        //        //    if (pointIsWithinSegmentWidth)
        //        //    { // CartesianCoordinate is on horizontal segment
        //        //        return includePointOnSegment ? 1 : 0;
        //        //    }
        //        //    continue;
        //        //}

        //        double xIntersection = LineToLineIntersection.IntersectionPointX(coordinate.Y, vertexI, vertexJ);
        //        if (LineToLineIntersection.PointIsLeftOfSegmentIntersection(coordinate.X, xIntersection, vertexI, vertexJ))
        //        {
        //            numberOfIntersections++;
        //        }
        //        //else if (coordinate.X.IsEqualTo(xIntersection, tolerance))
        //        //{ // CartesianCoordinate is on sloped segment
        //        //    return includePointOnSegment ? 1 : 0;
        //        //}
        //    }
        //    return numberOfIntersections;
        //}

        // TODO: Determine if this sort of function is ever needed: NumberOfIntersectionsOnVerticalProjection
        ///// <summary>
        ///// The numbers of shape boundary intersections a vertical line makes when projecting to the top from the provided point.
        ///// If the point is on a vertex or segment, the function returns either 0 or 1.
        ///// </summary>
        ///// <param name="coordinate">The coordinate.</param>
        ///// <param name="shapeBoundary">The shape boundary composed of n points.</param>
        ///// <param name="includePointOnSegment">if set to <c>true</c> [include point on segment].</param>
        ///// <param name="includePointOnVertex">if set to <c>true</c> [include point on vertex].</param>
        ///// <param name="tolerance">The tolerance.</param>
        ///// <returns>System.Int32.</returns>
        //public static int NumberOfIntersectionsOnVerticalProjection(
        //    CartesianCoordinate coordinate,
        //    CartesianCoordinate[] shapeBoundary,
        //    bool includePointOnSegment = true,
        //    bool includePointOnVertex = true,
        //    double tolerance = GeometryLibrary.ZeroTolerance)
        //{
        //    if (shapeBoundary[0] != shapeBoundary[shapeBoundary.Length - 1])
        //        throw new ArgumentException("Shape boundary describes a shape. Closure to the shape boundary is needed.");

        //    // 1. Check vertical line projection from a pt. n to the top
        //    // 2. Count # of intersections of the line with shape edges   

        //    // Note shape coordinates from XML already repeat starting node as an ending node.
        //    // No need to handle wrap-around below.

        //    int numberOfIntersections = 0;
        //    for (int i = 0; i < shapeBoundary.Length - 1; i++)
        //    {
        //        CartesianCoordinate vertexI = shapeBoundary[i];
        //        if (PointIntersection.IsOnPoint(coordinate, vertexI))
        //        {
        //            return includePointOnVertex ? 1 : 0;
        //        }

        //        CartesianCoordinate vertexJ = shapeBoundary[i + 1];

        //        if (!LineToLineIntersection.PointIsBelowLineBottomInclusive(coordinate.X, vertexI, vertexJ))
        //        {
        //            // Pt is above the segment.
        //            continue;
        //        }
        //        bool pointIsWithinSegmentWidth = LineToLineIntersection.PointIsWithinLineWidthInclusive(
        //                                            coordinate.X,
        //                                            vertexI, vertexJ);
        //        if (!pointIsWithinSegmentWidth)
        //        {
        //            // CartesianCoordinate is out of horizontal bounds of the segment extents.
        //            continue;
        //        }
        //        bool pointIsWithinSegmentHeight = LineToLineIntersection.PointIsWithinLineHeightInclusive(
        //                                            coordinate.Y,
        //                                            vertexI, vertexJ);
        //        if (LinearCurve.IsHorizontal(vertexI, vertexJ))
        //        {
        //            if (pointIsWithinSegmentHeight)
        //            { // CartesianCoordinate is on horizontal segment
        //                return includePointOnSegment ? 1 : 0;
        //            }
        //            // CartesianCoordinate hits horizontal segment
        //            numberOfIntersections++;
        //            continue;
        //        }
        //        if (LinearCurve.IsVertical(vertexI, vertexJ))
        //        {   // Segment would be parallel to line projection.
        //            // CartesianCoordinate is collinear since it is within segment height
        //            if (pointIsWithinSegmentHeight)
        //            { // CartesianCoordinate is on vertical segment
        //                return includePointOnSegment ? 1 : 0;
        //            }
        //            continue;
        //        }

        //        double yIntersection = LineToLineIntersection.IntersectionPointY(coordinate.X, vertexI, vertexJ);
        //        if (LineToLineIntersection.PointIsBelowSegmentIntersection(coordinate.Y, yIntersection, vertexI, vertexJ))
        //        {
        //            numberOfIntersections++;
        //        }
        //        else if (NMath.Abs(coordinate.Y - yIntersection) < tolerance)
        //        { // CartesianCoordinate is on sloped segment
        //            return includePointOnSegment ? 1 : 0;
        //        }
        //    }
        //    return numberOfIntersections;
        //}
        #endregion

        #region Within Bounds
        /// <summary>
        /// Determines if the point lies within the segment extents height.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies within the segment extents height, <c>false</c> otherwise.</returns>
        public static bool PointIsWithinSegmentExtentsHeightInclusive(
            double yPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return (extents.MinY <= yPtN && yPtN <= extents.MaxY);
        }

        /// <summary>
        /// Determines if the point lies within the segment extents height, not including the boundary locations.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies within the segment extents height, <c>false</c> otherwise.</returns>
        public static bool PointIsWithinSegmentExtentsHeightExclusive(
            double yPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return (extents.MinY < yPtN && yPtN < extents.MaxY);
        }


        /// <summary>
        /// Determines if the point lies within the segment extents width.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies within the segment width, <c>false</c> otherwise.</returns>
        public static bool PointIsWithinSegmentExtentsWidthInclusive(
            double xPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return (extents.MinX <= xPtN && xPtN <= extents.MaxX);
        }

        /// <summary>
        /// Determines if the point lies within the segment extents width.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies within the segment width, <c>false</c> otherwise.</returns>
        public static bool PointIsWithinSegmentExtentsWidthExclusive(
            double xPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return (extents.MinX < xPtN && xPtN < extents.MaxX);
        }
        #endregion

        #region Left of/Below Potential Intersection
        /// <summary>
        /// Determines if the point lies to the left of the segment extents max x-coordinate.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies to the left of the segment end, <c>false</c> otherwise.</returns>
        public static bool PointIsLeftOfSegmentExtentsEndInclusive(
            double xPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return xPtN <= extents.MaxX;
        }

        /// <summary>
        /// Determines if the point lies to the left of the segment extents max x-coordinate, not including the boundary coordinate.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies to the left of the segment end, <c>false</c> otherwise.</returns>
        public static bool PointIsLeftOfSegmentExtentsEndExclusive(
            double xPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return xPtN < extents.MaxX;
        }

        /// <summary>
        /// Determines if the point lies below the segment extents end.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies to below the segment bottom, <c>false</c> otherwise.</returns>
        public static bool PointIsBelowSegmentExtentsInclusive(
            double yPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return yPtN <= extents.MaxY;
        }

        /// <summary>
        /// Determines if the point lies below the segment extents end.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point lies to below the segment bottom, <c>false</c> otherwise.</returns>
        public static bool PointIsBelowSegmentExtentsExclusive(
            double yPtN,
            IPathSegment segment)
        {
            PointExtents extents = segment.Extents;
            return yPtN < extents.MaxY;
        }
        #endregion

        #region Left of/Below Intersection
        /// <summary>
        /// The x-coordinate of the intersection of the horizontally projected line with the provided segment.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n, where the projection starts.</param>
        /// <param name="segment">The segment.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Segment is horizontal, so intersection point is either infinity or NAN.</exception>
        public static double IntersectionPointX(
            double yPtN,
            IPathSegment segment)
        {
            return segment.X(yPtN);
        }


        /// <summary>
        /// Determines if the point is to the left of the horizontally projected segment intersection.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n.</param>
        /// <param name="xIntersection">The x-coordinate of the intersection of the projected line.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point is to the left of the horizontally projected segment intersection, <c>false</c> otherwise.</returns>
        public static bool PointIsLeftOfSegmentIntersection(
            double xPtN,
            double xIntersection,
            IPathSegment segment)
        {
            return (xPtN < xIntersection &&
                PointIsWithinSegmentExtentsWidthInclusive(xIntersection, segment));
        }

        /// <summary>
        /// The y-coordinate of the intersection of the vertically projected line with the provided segment.
        /// </summary>
        /// <param name="xPtN">The x-coordinate of pt n, where the projection starts.</param>
        /// <param name="segment">The segment.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Segment is vertical, so intersection point is either infinity or NAN.</exception>
        public static double IntersectionPointY(
            double xPtN,
            IPathSegment segment)
        {
            return segment.Y(xPtN);
        }

        /// <summary>
        /// Determines if the point is below the vertically projected segment intersection.
        /// </summary>
        /// <param name="yPtN">The y-coordinate of pt n.</param>
        /// <param name="yIntersection">The y-coordinate of the intersection of the projected line.</param>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the point is below the vertically projected segment intersection, <c>false</c> otherwise.</returns>
        public static bool PointIsBelowSegmentIntersection(
            double yPtN,
            double yIntersection,
            IPathSegment segment)
        {
            return (yPtN < yIntersection &&
                    PointIsWithinSegmentExtentsHeightInclusive(yIntersection, segment));
        }
        #endregion
    }
}

using MPT.Math.Coordinates;
using System;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Interface for paths that are divisible.
    /// </summary>
    public interface IPathDivisionExtension
    {
        /// <summary>
        /// Splits the segment by the provided point.
        /// </summary>
        /// <param name="pointDivision">The point to use for division.</param>
        /// <returns>Tuple&lt;IPathSegment, IPathSegment&gt;.</returns>
        Tuple<IPathSegment, IPathSegment> SplitBySegmentPoint(CartesianCoordinate pointDivision);

        /// <summary>
        /// Returns a copy of the segment that splits the segment by the relative location.
        /// <paramref name="sRelative"/> must be between 0 and 1.
        /// </summary>
        /// <param name="sRelative">The relative position along the path between 0 (point i) and 1 (point j).</param>
        /// <returns>Tuple&lt;IPathSegment, IPathSegment&gt;.</returns>
        Tuple<IPathSegment, IPathSegment> SplitBySegmentPosition(double sRelative);

        /// <summary>
        /// Extends the segment to the provided point.
        /// </summary>
        /// <param name="pointExtension">The point to extend the segment to.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment ExtendSegmentToPoint(CartesianCoordinate pointExtension);

        ///// <summary>
        ///// Extends the segment to intersect the provided curve.
        ///// </summary>
        ///// <param name="curve">The curve.</param>
        ///// <returns>IPathSegment.</returns>
        //IPathSegment ExtendSegmentToCurve(LinearCurve curve);

        ///// <summary>
        ///// Coordinate of where the segment projection intersects the provided curve.
        ///// </summary>
        ///// <param name="curve">The curve.</param>
        ///// <returns>CartesianCoordinate.</returns>
        ///// <exception cref="ArgumentOutOfRangeException">Curves never intersect.</exception>
        //CartesianCoordinate CoordinateOfSegmentProjectedToCurve(LinearCurve curve);

        /// <summary>
        /// Coordinate of where a perpendicular projection intersects the provided coordinate.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>CartesianCoordinate.</returns>
        CartesianCoordinate CoordinateOfPerpendicularProjection(CartesianCoordinate point);

        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        ///  <paramref name="fraction"/> must be between 0 and 1.
        /// </summary>
        /// <param name="fraction">Fraction of the way from point 1 to point 2.</param>
        /// <returns></returns>
        CartesianCoordinate PointOffsetOnSegment(double fraction);

        /// <summary>
        ///  Returns a point determined by a given ratio of the distance between point i and point j of the segment.
        /// </summary>
        /// <param name="ratio">Ratio of the size of the existing segment. 
        /// If <paramref name="ratio"/>&lt; 0, returned point is offset from point i, in that direction. 
        /// If <paramref name="ratio"/>&gt; 0, returned point is offset from point j, in that direction.</param>
        /// <returns></returns>
        CartesianCoordinate PointScaledFromSegment(double ratio);

        /// <summary>
        /// Returns a copy of the segment that merges the current segment with the prior segment.
        /// </summary>
        /// <param name="priorSegment">The prior segment.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment MergeWithPriorSegment(IPathSegment priorSegment);

        /// <summary>
        /// Returns a copy of the segment that merges the current segment with the following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment MergeWithFollowingSegment(IPathSegment followingSegment);

        /// <summary>
        /// Returns a copy of the segment that joins the current segment with the prior segment.
        /// </summary>
        /// <param name="priorSegment">The prior segment.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment JoinWithPriorSegment(IPathSegment priorSegment);

        /// <summary>
        /// Returns a copy of the segment that joins the current segment with the following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment JoinWithFollowingSegment(IPathSegment followingSegment);


    }
}

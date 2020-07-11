using MPT.Geometry.Tools;
using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Vectors;
using System;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Interface for any segment along a path lying in a plane.
    /// </summary>
    public interface IPathSegment: 
        IEquatable<IPathSegment>, 
        ITransform<IPathSegment>
    {
        /// <summary>
        /// Gets the extents.
        /// </summary>
        /// <value>The extents.</value>
        PointExtents Extents { get; }

        /// <summary>
        /// First coordinate value.
        /// </summary>
        CartesianCoordinate I { get; set; }

        /// <summary>
        /// Second coordinate value.
        /// </summary>
        CartesianCoordinate J { get; set; }

        /// <summary>
        /// Length of the path segment.
        /// </summary>
        /// <returns></returns>
        double Length();

        /// <summary>
        /// X-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        double Xo();

        /// <summary>
        /// Y-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        double Yo();

        /// <summary>
        /// X-coordinate on the path that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        double X(double y);

        /// <summary>
        /// Y-coordinate on the path that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        double Y(double x);

        /// <summary>
        /// Determines whether the segment [has same coordinates] as [the specified segment].
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if [has same coordinates] [the specified segment]; otherwise, <c>false</c>.</returns>
        bool HasSameCoordinates(IPathSegment segment);

        /// <summary>
        /// Coordinate on the path that corresponds to the position along the path.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        /// <returns></returns>
        CartesianCoordinate PointByPathPosition(double sRelative);

        /// <summary>
        /// Vector that is normal to the line connecting the defining points at the position specified.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        Vector NormalVector(double sRelative);

        /// <summary>
        /// Vector that is tangential to the line connecting the defining points at the position specified.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        Vector TangentVector(double sRelative);

        /// <summary>
        /// Returns a copy of the segment with an updated I coordinate.
        /// </summary>
        /// <param name="newCoordinate">The new coordinate.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment UpdateI(CartesianCoordinate newCoordinate);

        /// <summary>
        /// Returns a copy of the segment with an updated J coordinate.
        /// </summary>
        /// <param name="newCoordinate">The new coordinate.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment UpdateJ(CartesianCoordinate newCoordinate);

        /// <summary>
        /// Merges the with leading segment.
        /// </summary>
        /// <param name="leadingSegment">The leading segment.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment MergeWithPriorSegment(IPathSegment leadingSegment);

        /// <summary>
        /// Merges the with following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        IPathSegment MergeWithFollowingSegment(IPathSegment followingSegment);

        /// <summary>
        /// Splits the segment by the relative location.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        /// <returns>Tuple&lt;IPathSegment, IPathSegment&gt;.</returns>
        Tuple<IPathSegment, IPathSegment> SplitBySegmentPosition(double sRelative);
    }
}

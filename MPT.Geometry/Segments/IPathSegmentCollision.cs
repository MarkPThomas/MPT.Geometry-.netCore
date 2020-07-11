using MPT.Math.Coordinates;
using MPT.Math.Curves;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Interface for path segments with methods for collision detection.
    /// </summary>
    public interface IPathSegmentCollision<T>
        where T : Curve
    {
        /// <summary>
        /// Gets the curve.
        /// </summary>
        /// <value>The curve.</value>
        T Curve { get; }

        /// <summary>
        /// Provided point lies on the line segment between or on the defining points.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool IncludesCoordinate(CartesianCoordinate point);

        /// <summary>
        /// Provided line segment intersects the line segment between or on the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        bool IsIntersecting(LineSegment otherLine);

        /// <summary>
        /// Returns a point where the line segment intersects the provided line segment.
        /// </summary>
        /// <param name="otherLine">Line segment that intersects the current line segment.</param>
        /// <returns></returns>
        CartesianCoordinate IntersectionCoordinate(LineSegment otherLine);
    }
}

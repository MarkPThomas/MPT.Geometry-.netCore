using MPT.Math.Coordinates;
using MPT.Math.Vectors;

namespace MPT.Geometry.Line
{
    /// <summary>
    /// Interface for any segment along a path lying in a plane.
    /// </summary>
    public interface IPathSegment
    {
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
    }
}

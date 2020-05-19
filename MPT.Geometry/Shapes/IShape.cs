using MPT.Math;
using MPT.Math.Coordinates;

namespace MPT.Geometry.Shapes
{
    /// <summary>
    /// Interface for all paths that create a closed shape.
    /// </summary>
    public interface IShapeProperties
    {
        /// <summary>
        /// Length of all sides of the shape.
        /// </summary>
        /// <returns></returns>
        double Perimeter();

        /// <summary>
        /// Area of the shape.
        /// </summary>
        /// <returns></returns>
        double Area();

        /// <summary>
        /// X-coordinate of the centroid of the shape.
        /// </summary>
        /// <returns></returns>
        double Xo();

        /// <summary>
        /// Y-coordinate of the centroid of the shape.
        /// </summary>
        /// <returns></returns>
        double Yo();

        /// <summary>
        /// Rotational inertia about the x-axis, passing through the centroid of the shape.
        /// </summary>
        /// <returns></returns>
        double Ixo();

        /// <summary>
        /// Rotational inertia about the y-axis, passing through the centroid of the shape.
        /// </summary>
        /// <returns></returns>
        double Iyo();

        /// <summary>
        /// Polar rotational inertia, passing through the centroid of the shape.
        /// </summary>
        /// <returns></returns>
        double Ixyo();

        /// <summary>
        /// Rotational inertia about the x-axis, passing through an axis offset from the centroid of the shape.
        /// </summary>
        /// <param name="yOffset">Distance along the y-axis that the new coordinate axis is offset.</param>
        /// <returns></returns>
        double Ix(double yOffset);

        /// <summary>
        /// Rotational inertia about the y-axis, passing through an axis offset from the centroid of the shape.
        /// </summary>
        /// <param name="xOffset">Distance along the x-axis that the new coordinate axis is offset.</param>
        /// <returns></returns>
        double Iy(double xOffset);

        /// <summary>
        /// Rotational inertia about the y-axis, passing through an axis offset from the centroid of the shape.
        /// </summary>
        /// <param name="xOffset">Distance along the x-axis that the new coordinate axis is offset.</param>
        /// <param name="yOffset">Distance along the y-axis that the new coordinate axis is offset.</param>
        /// <returns></returns>
        double Ixy(double xOffset, double yOffset);

        /// <summary>
        /// Coordinate for the point offset from the origin.
        /// </summary>
        /// <param name="coordsOffset"></param>
        /// <returns></returns>
        double Ixy(CartesianCoordinate coordsOffset);



        /// <summary>
        /// Rotational inertia about the x-axis, passing through an axis rotated off from the principal axes.
        /// </summary>
        /// <param name="alpha">Rotation of the axes from the principal axes [radians].</param>
        /// <returns></returns>
        double IxRotation(double alpha);

        /// <summary>
        /// Rotational inertia about the y-axis, passing through an axis rotated off from the principal axes.
        /// </summary>
        /// <param name="alpha">Rotation of the axes from the principal axes [radians].</param>
        /// <returns></returns>
        double IyRotation(double alpha);

        /// <summary>
        /// Rotational inertia about the x-axis, passing through an axis rotated off from the principal axes.
        /// </summary>
        /// <param name="alpha">Rotation of the axes from the principal axes [radians].</param>
        /// <returns></returns>
        double IxyRotation(double alpha);
    }
}

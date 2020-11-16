using MPT.Math.Coordinates;
using MPT.Math.Curves;

namespace MPT.Geometry
{
    /// <summary>
    /// Interface ITransform
    /// </summary>
    public interface ITransform<T>
    {
        /// <summary>
        /// Translates the segment.
        /// </summary>
        /// <param name="translation">The amount to translate by.</param>
        /// <returns>IPathSegment.</returns>
        T Translate(CartesianOffset translation);

        /// <summary>
        /// Scales the segment from the provided reference point.
        /// </summary>
        /// <param name="scale">The amount to scale relative to the reference point.</param>
        /// <param name="referencePoint">The reference point.</param>
        /// <returns>IPathSegment.</returns>
        T ScaleFromPoint(double scale, CartesianCoordinate referencePoint);

        /// <summary>
        /// Rotates the segment about the reference point.
        /// </summary>
        /// <param name="rotation">The amount of rotation. [rad]</param>
        /// <param name="referencePoint">The center of rotation reference point.</param>
        /// <returns>IPathSegment.</returns>
        T RotateAboutPoint(Angle rotation, CartesianCoordinate referencePoint);

        /// <summary>
        /// Skews the specified segment to the skewing of a containing box.
        /// </summary>
        /// <param name="stationaryReferencePoint">The stationary reference point of the skew box.</param>
        /// <param name="skewingReferencePoint">The skewing reference point of the skew box.</param>
        /// <param name="magnitude">The magnitude to skew along the x-axis and y-axis.</param>
        /// <returns>IPathSegment.</returns>
        T Skew(
            CartesianCoordinate stationaryReferencePoint,
            CartesianCoordinate skewingReferencePoint,
            CartesianOffset magnitude);

        /// <summary>
        /// Mirrors the specified segment about the specified reference line.
        /// </summary>
        /// <param name="referenceLine">The reference line.</param>
        /// <returns>IPathSegment.</returns>
        T MirrorAboutLine(LinearCurve referenceLine);

        /// <summary>
        /// Mirrors the specified segment about the x-axis.
        /// </summary>
        /// <returns>IPathSegment.</returns>
        T MirrorAboutAxisX();

        /// <summary>
        /// Mirrors the specified segment about the y-axis.
        /// </summary>
        /// <returns>IPathSegment.</returns>
        T MirrorAboutAxisY();
    }
}

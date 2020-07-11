using MPT.Math.Coordinates;

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

        // TODO: Mirror about line

        // TODO: Skew about line
    }
}

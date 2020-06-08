using MPT.Math.Coordinates;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Interface for paths that are divisible.
    /// </summary>
    public interface IPathDivisionExtension
    {
        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        ///  <paramref name="fraction"/> must be between 0 and 1.
        /// </summary>
        /// <param name="fraction">Fraction of the way from point 1 to point 2.</param>
        /// <returns></returns>
        CartesianCoordinate PointDivision(double fraction);

        /// <summary>
        ///  Returns a point determined by a given ratio of the distance between point i and point j of the segment.
        /// </summary>
        /// <param name="ratio">Ratio of the size of the existing segment. 
        /// If <paramref name="ratio"/>&lt; 0, returned point is offset from point i, in that direction. 
        /// If <paramref name="ratio"/>&gt; 0, returned point is offset from point j, in that direction.</param>
        /// <returns></returns>
        CartesianCoordinate PointExtension(double ratio);
    }
}

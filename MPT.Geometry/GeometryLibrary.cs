using Num = MPT.Math.Numbers;
using MPT.Geometry.Line;
using MPT.Math.Vectors;

namespace MPT.Geometry
{
    /// <summary>
    /// Library of operations related to the geometry framework.
    /// </summary>
    public static class GeometryLibrary
    {
        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        public const double ZeroTolerance = Num.ZeroTolerance;

        #region Vector-Derived
        
        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the same direction.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsCollinearSameDirection(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsCollinearSameDirection(line1.ToVector(), line2.ToVector(), tolerance));
        }

        /// <summary>
        /// Vectors form a concave angle.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsConcave(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsConcave(line1.ToVector(), line2.ToVector(), tolerance));
        }

        /// <summary>
        /// Vectors form a 90 degree angle.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsOrthogonal(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsOrthogonal(line1.ToVector(), line2.ToVector(), tolerance));
        }

        /// <summary>
        /// Vectors form a convex angle.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsConvex(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsConvex(line1.ToVector(), line2.ToVector(), tolerance));
        }

        /// <summary>
        ///  True: Segments are parallel, on the same line, oriented in the opposite direction.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsCollinearOppositeDirection(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsCollinearOppositeDirection(line1.ToVector(), line2.ToVector(), tolerance));
        }



        /// <summary>
        /// True: The concave side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool ConcaveInside(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsConcaveInside(line1.ToVector(), line2.ToVector(), tolerance));
        }

        /// <summary>
        /// True: The convex side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool ConvexInside(LineSegment line1, LineSegment line2, double tolerance = ZeroTolerance)
        {
            return (VectorLibrary.IsConvexInside(line1.ToVector(), line2.ToVector(), tolerance));
        }





        /// <summary>
        /// Returns a value indicating the concavity of the vectors. 
        /// 1 = Pointing the same way. 
        /// &gt; 0 = Concave. 
        /// 0 = Orthogonal. 
        /// &lt; 0 = Convex. 
        /// -1 = Pointing the exact opposite way.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static double ConcavityCollinearity(LineSegment line1, LineSegment line2)
        {
            return (line1.ToVector().ConcavityCollinearity(line2.ToVector()));
        }

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static double Dot(LineSegment line1, LineSegment line2)
        {
            return (line1.ToVector().DotProduct(line2.ToVector()));
        }

        /// <summary>
        /// Cross-product of two vectors.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static double Cross(LineSegment line1, LineSegment line2)
        {
            return (line1.ToVector().CrossProduct(line2.ToVector()));
        }

        /// <summary>
        /// Returns the angle [radians] between the two vectors.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static double Angle(LineSegment line1, LineSegment line2)
        {
            return (line1.ToVector().Angle(line2.ToVector()));
        }

        /// <summary>
        /// Returns the area between two vectors.
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static double Area(LineSegment line1, LineSegment line2)
        {
            return (line1.ToVector().Area(line2.ToVector()));
        }
        #endregion
    }
}

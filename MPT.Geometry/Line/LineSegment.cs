using System;
using NMath = System.Math;

using GL = MPT.Geometry.GeometryLibrary;
using MPT.Math.Coordinates;
using MPT.Math.Algebra;
using MPT.Math.Vectors;
using MPT.Math.NumberTypeExtensions;
using MPT.Math;

namespace MPT.Geometry.Line
{
    /// <summary>
    /// Segment that describes a straight line in a plane.
    /// </summary>
    public class LineSegment : PathSegment, IPathDivision, ILine
    {
        #region Properties

        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the line segment with empty points.
        /// </summary>
        public LineSegment() { }

        /// <summary>
        /// Initializes the line segment to span between the provided points.
        /// </summary>
        /// <param name="i">First point of the line.</param>
        /// <param name="j">Second point of the line.</param>
        public LineSegment(CartesianCoordinate i, CartesianCoordinate j): base(i, j){ }
        #endregion

        #region Methods: Override (IPathSegment)
        /// <summary>
        /// Length of the line segment.
        /// </summary>
        /// <returns></returns>
        public override double Length()
        {
            return AlgebraLibrary.SRSS((J.X - I.X), (J.Y - I.Y));
        }

        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public override double X(double y)
        {
            return ((y - InterceptY())/Slope());
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public override double Y(double x)
        {
            return (InterceptY() + Slope() * x);
        }

        /// <summary>
        /// X-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        public override double Xo()
        {
            return 0.5*(J.X - I.X);
        }

        /// <summary>
        /// Y-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        public override double Yo()
        {
            return 0.5 * (J.Y - I.Y);
        }

        /// <summary>
        /// Coordinate on the path that corresponds to the position along the path.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        /// <returns></returns>
        public override CartesianCoordinate PointByPathPosition(double sRelative)
        {
            if ((sRelative < 0) || (1 < sRelative))
            {
                throw new ArgumentException($"Relative position must be between 0 and 1, but was {sRelative}.");
            }
            double x = I.X + (J.X - I.X) * sRelative;
            double y = I.Y + (J.Y - I.Y) * sRelative;

            return new CartesianCoordinate(x, y);
        }

        /// <summary>
        /// Vector that is normal to the line connecting the defining points at the position specified.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        public override Vector NormalVector(double sRelative)
        {
            return NormalVector();
        }

        /// <summary>
        /// Vector that is tangential to the line connecting the defining points at the position specified.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        public override Vector TangentVector(double sRelative)
        {
            return TangentVector();
        }

        #endregion

        #region Methods: Override (IPathSegmentCollision)

        /// <summary>
        /// Provided point lies on the line segment between or on the defining points.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsIntersecting(CartesianCoordinate point)
        {
            if (!IsProjectionIntersecting(point)) { return false; }
            double yMax = NMath.Max(I.Y, J.Y);
            double yMin = NMath.Min(I.Y, J.Y);

            return (yMin.IsLessThan(point.Y, Tolerance) && point.Y.IsLessThan(yMax, Tolerance));
        }

        /// <summary>
        /// Provided line segment intersects the line segment between or on the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsIntersecting(LineSegment otherLine)
        {
            if (!IsProjectionIntersecting(otherLine)) { return false; }

            CartesianCoordinate intersectionPoint = LineIntersect(Slope(), InterceptY(),
                                                     otherLine.Slope(), otherLine.InterceptY());
            return IsIntersecting(intersectionPoint);
        }

        /// <summary>
        /// Returns a point where the line segment intersects the provided line segment.
        /// </summary>
        /// <param name="otherLine">Line segment that intersects the current line segment.</param>
        /// <returns></returns>
        public CartesianCoordinate Intersect(LineSegment otherLine)
        {
            return LineIntersect(Slope(), InterceptY(),
                                 otherLine.Slope(), otherLine.InterceptY());
        }
        #endregion

        #region Methods (IPathDivision)
        /// <summary>
        /// Returns a point a given fraction of the distance between point 1 and point 2.
        /// </summary>
        /// <param name="fraction">Fraction of the way from point 1 to point 2.</param>
        /// <returns></returns>
        public CartesianCoordinate PointDivision(double fraction)
        {
            double x = I.X + fraction * (J.X - I.X);
            double y = I.Y + fraction * (J.Y - I.Y);
            return new CartesianCoordinate(x, y);
        }

        #endregion

        #region Methods: Public
        /// <summary>
        /// Determines whether this instance is horizontal.
        /// </summary>
        /// <returns><c>true</c> if this instance is horizontal; otherwise, <c>false</c>.</returns>
        public bool IsHorizontal()
        {
            return IsHorizontal(I, J, Tolerance);
        }

        /// <summary>
        /// Determines whether this instance is vertical.
        /// </summary>
        /// <returns><c>true</c> if this instance is vertical; otherwise, <c>false</c>.</returns>
        public bool IsVertical()
        {
            return IsVertical(I, J, Tolerance);
        }

        /// <summary>
        /// Slope of the line segment.
        /// </summary>
        /// <returns></returns>
        public double Slope()
        {
            return Slope(I, J, Tolerance);
        }

        /// <summary>
        /// X-Intercept of the line segment.
        /// </summary>
        /// <returns></returns>
        public double InterceptX()
        {
            return InterceptX(I, J, Tolerance);
        }

        /// <summary>
        /// Y-Intercept of the line segment.
        /// </summary>
        /// <returns></returns>
        public double InterceptY()
        {
            return InterceptY(I, J, Tolerance);
        }

        /// <summary>
        /// Lines are parallel to each other.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsParallel(LineSegment otherLine)
        {
            return (Slope() - otherLine.Slope()).IsZeroSign(Tolerance);
        }

        /// <summary>
        /// Lines are perpendicular to each other.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsPerpendicular(LineSegment otherLine)
        {
            return (Slope() * otherLine.Slope()).IsEqualTo(-1, Tolerance);
        }

        /// <summary>
        /// Provided point lies on an infinitely long line projecting off of the line segment. 
        /// It isn't necessarily intersecting between the defining points.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsProjectionIntersecting(CartesianCoordinate point)
        {
            return (point.Y.IsEqualTo(InterceptY() + Slope() * point.X));
        }

        /// <summary>
        /// Provided line segment intersects an infinitely long line projecting off of the line segment. 
        /// It isn't necessarily intersecting between the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsProjectionIntersecting(LineSegment otherLine)
        {
            return !IsParallel(otherLine);
        }

        /// <summary>
        /// Converts the line segment to a vector.
        /// </summary>
        /// <returns></returns>
        public Vector ToVector()
        {
            return new Vector(new CartesianCoordinate(I.X, I.Y), new CartesianCoordinate(J.X, J.Y));
        }

        #endregion

        #region Methods: Static

        #region Alignment
        /// <summary>
        /// Determines if the shape segment is horizontal.
        /// </summary>
        /// <param name="ptI">The point i.</param>
        /// <param name="ptJ">The point j.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the shape segment is horizontal, <c>false</c> otherwise.</returns>
        public static bool IsHorizontal(CartesianCoordinate ptI,
            CartesianCoordinate ptJ,
            double tolerance = GL.ZeroTolerance)
        {
            return NMath.Abs(ptI.Y - ptJ.Y) < tolerance;
        }

        /// <summary>
        /// Determines if the segment is vertical.
        /// </summary>
        /// <param name="ptI">The point i.</param>
        /// <param name="ptJ">The point j.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the segment is vertical, <c>false</c> otherwise.</returns>
        public static bool IsVertical(CartesianCoordinate ptI,
            CartesianCoordinate ptJ,
            double tolerance = GL.ZeroTolerance)
        {
            return NMath.Abs(ptI.X - ptJ.X) < tolerance;
        }

        /// <summary>
        /// True: Slopes are parallel.
        /// </summary>
        /// <param name="slope1"></param>
        /// <param name="slope2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsParallel(double slope1, double slope2, double tolerance = GL.ZeroTolerance)
        {
            if (double.IsNegativeInfinity(slope1) && double.IsPositiveInfinity(slope2)) { return true; }
            if (double.IsNegativeInfinity(slope2) && double.IsPositiveInfinity(slope1)) { return true; }
            return slope1.IsEqualTo(slope2, tolerance);
        }

        /// <summary>
        /// True: Slopes are perpendicular.
        /// </summary>
        /// <param name="slope1"></param>
        /// <param name="slope2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsPerpendicular(double slope1, double slope2, double tolerance = GL.ZeroTolerance)
        {
            if (double.IsNegativeInfinity(slope1) && slope2.IsZeroSign(tolerance)) { return true; }
            if (double.IsNegativeInfinity(slope2) && slope1.IsZeroSign(tolerance)) { return true; }
            if (double.IsPositiveInfinity(slope1) && slope2.IsZeroSign(tolerance)) { return true; }
            if (double.IsPositiveInfinity(slope2) && slope1.IsZeroSign(tolerance)) { return true; }
            return ((slope1 * slope2).IsEqualTo(-1));
        }
        #endregion

        #region Slope
        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="rise">Difference in y-coordinate values or equivalent.</param>
        /// <param name="run">Difference in x-coordinate values or equivalent.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double Slope(double rise, double run, double tolerance = GL.ZeroTolerance)
        {
            if (run.IsZeroSign(tolerance) && rise.IsPositiveSign()) { return double.PositiveInfinity; }
            if (run.IsZeroSign(tolerance) && rise.IsNegativeSign()) { return double.NegativeInfinity; }
            if (run.IsZeroSign(tolerance) && rise.IsZeroSign(tolerance)) { throw new ArgumentException("Rise & run are both zero. Cannot determine a slope for a point."); }
            return (rise / run);
        }

        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double Slope(double x1, double y1, double x2, double y2, double tolerance = GL.ZeroTolerance)
        {
            return Slope((y2 - y1),
                         (x2 - x1), tolerance);
        }


        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="point1">First point.</param>
        /// <param name="point2">Second point.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double Slope(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = GL.ZeroTolerance)
        {
            return Slope((point2.Y - point1.Y),
                         (point2.X - point1.X), tolerance);
        }

        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="delta">The difference between two points.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double Slope(CartesianOffset delta, double tolerance = GL.ZeroTolerance)
        {
            return Slope((delta.J.Y - delta.I.Y),
                         (delta.J.X - delta.I.X), tolerance);
        }
        #endregion

        #region Intercept
        /// <summary>
        /// Returns the x-intercept.
        /// </summary>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double InterceptX(double x1, double y1, double x2, double y2, double tolerance = GL.ZeroTolerance)
        {
            if (y1.IsZeroSign(tolerance)) { return x1; }
            if (y2.IsZeroSign(tolerance)) { return x2; }
            return (-InterceptY(x1, y1, x2, y2, tolerance) / Slope(x1, y1, x2, y2, tolerance));
        }

        /// <summary>
        /// Returns the x-intercept.
        /// </summary>
        /// <param name="point1">First point defining a line.</param>
        /// <param name="point2">Second point defining a line.</param>
        /// <param name="tolerance">>Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double InterceptX(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = GL.ZeroTolerance)
        {
            return InterceptX(point1.X, point1.Y, point2.X, point2.Y, tolerance);
        }

        /// <summary>
        /// Returns the y-intercept.
        /// </summary>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double InterceptY(double x1, double y1, double x2, double y2, double tolerance = GL.ZeroTolerance)
        {
            if (x1.IsZeroSign(tolerance)) { return y1; }
            if (x2.IsZeroSign(tolerance)) { return y2; }
            return (-Slope(x1, y1, x2, y2, tolerance) * x1 + y1);
        }

        /// <summary>
        /// Returns the y-intercept.
        /// </summary>
        /// <param name="point1">First point defining a line.</param>
        /// <param name="point2">Second point defining a line.</param>
        /// <param name="tolerance">>Tolerance by which a double is considered to be zero or equal.</param>
        public static double InterceptY(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = GL.ZeroTolerance)
        {
            return InterceptY(point1.X, point1.Y, point2.X, point2.Y, tolerance);
        }
        #endregion

        #region Intersect
        /// <summary>
        /// The lines intersect.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool AreLinesIntersecting(double slope1, double slope2, double tolerance = GL.ZeroTolerance)
        {
            return (!IsParallel(slope1, slope2, tolerance));
        }

        /// <summary>
        /// The x-coordinate of the intersection of two lines.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="yIntercept1">Y-intercept of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="yIntercept2">Y-intercept of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double LineIntersectX(double slope1, double yIntercept1, double slope2, double yIntercept2, double tolerance = GL.ZeroTolerance)
        {
            if (IsParallel(slope1, slope2, tolerance)) { return double.PositiveInfinity; }
            return (yIntercept1 - yIntercept2) / (slope2 - slope1);
        }

        /// <summary>
        /// The y-coordinate of the intersection of two lines.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="yIntercept1">Y-intercept of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="yIntercept2">Y-intercept of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double LineIntersectY(double slope1, double yIntercept1, double slope2, double yIntercept2, double tolerance = GL.ZeroTolerance)
        {
            if (IsParallel(slope1, slope2, tolerance)) { return double.PositiveInfinity; }
            return (yIntercept1 + slope1 * (yIntercept1 - yIntercept2) / (slope2 - slope1));
        }

        /// <summary>
        /// The coordinates of the intersection of two lines.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="yIntercept1">Y-intercept of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="yIntercept2">Y-intercept of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static CartesianCoordinate LineIntersect(double slope1, double yIntercept1, double slope2, double yIntercept2, double tolerance = GL.ZeroTolerance)
        {
            return new CartesianCoordinate(
                LineIntersectX(slope1, yIntercept1, slope2, yIntercept2, tolerance),
                LineIntersectY(slope1, yIntercept1, slope2, yIntercept2, tolerance));
        }

        #endregion

        #endregion


    }
}

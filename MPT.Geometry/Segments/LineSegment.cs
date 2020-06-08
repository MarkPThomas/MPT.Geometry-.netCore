using System;
using NMath = System.Math;

using MPT.Math.Coordinates;
using MPT.Math.Vectors;
using MPT.Math.NumberTypeExtensions;
using MPT.Math;
using MPT.Math.Curves;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Segment that describes a straight line in a plane.
    /// </summary>
    public class LineSegment : PathSegment, IPathDivisionExtension, ILine
    {
        #region Properties
        private readonly LinearCurve _curve;
        /// <summary>
        /// Curve that spans between the I and J coordinates.
        /// </summary>
        public LinearCurve Curve => new LinearCurve(I, J);

        /// <summary>
        /// First coordinate value.
        /// </summary>
        public override CartesianCoordinate I => _curve.ControlPointI;

        /// <summary>
        /// Second coordinate value.
        /// </summary>
        public override CartesianCoordinate J => _curve.ControlPointJ;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the line segment to span between the provided points.
        /// </summary>
        /// <param name="i">First point of the line.</param>
        /// <param name="j">Second point of the line.</param>
        public LineSegment(CartesianCoordinate i, CartesianCoordinate j): base(i, j)
        {
            _curve = new LinearCurve(i, j);
        }
        #endregion

        #region Methods: Override (IPathSegment)
        /// <summary>
        /// Length of the line segment.
        /// </summary>
        /// <returns></returns>
        public override double Length()
        {
            return _curve.Length();
        }

        /// <summary>
        /// X-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        public override double Xo()
        {
            return 0.5*(I.X + J.X);
        }

        /// <summary>
        /// Y-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        public override double Yo()
        {
            return 0.5 * (I.Y + J.Y);
        }

        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public override double X(double y)
        {
            return _curve.X(y);
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public override double Y(double x)
        {
            return _curve.Y(x);
        }

        /// <summary>
        /// Coordinate on the path that corresponds to the position along the path.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        /// <returns></returns>
        public override CartesianCoordinate PointByPathPosition(double sRelative)
        {
            if(!sRelative.IsWithinInclusive(0, 1, Tolerance))
            {
                throw new ArgumentOutOfRangeException($"Relative position must be between 0 and 1, but was {sRelative}.");
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
        public bool IncludesCoordinate(CartesianCoordinate point)
        {
            double tolerance = Helper.GetTolerance(point, Tolerance);
            if (!_curve.IsIntersectingCoordinate(point)) 
            { 
                return false; 
            }

            if (_curve.IsHorizontal())
            {
                double xMax = NMath.Max(I.X, J.X);
                double xMin = NMath.Min(I.X, J.X);
                return (point.X.IsGreaterThanOrEqualTo(xMin, tolerance) && point.X.IsLessThanOrEqualTo(xMax, tolerance));
            }

            double yMax = NMath.Max(I.Y, J.Y);
            double yMin = NMath.Min(I.Y, J.Y);
            return (point.Y.IsGreaterThanOrEqualTo(yMin, tolerance) && point.Y.IsLessThanOrEqualTo(yMax, tolerance));
        }

        /// <summary>
        /// Provided line segment intersects the line segment between or on the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsIntersecting(LineSegment otherLine)
        {
            if (!_curve.IsIntersectingCurve(otherLine._curve)) 
            {
                return false; 
            }

            CartesianCoordinate intersection = _curve.IntersectionCoordinate(otherLine._curve);
            return IncludesCoordinate(intersection) && otherLine.IncludesCoordinate(intersection);
        }

        /// <summary>
        /// Returns a point where the line segment intersects the provided line segment.
        /// </summary>
        /// <param name="otherLine">Line segment that intersects the current line segment.</param>
        /// <returns></returns>
        public CartesianCoordinate IntersectionCoordinate(LineSegment otherLine)
        {
            CartesianCoordinate intersection = _curve.IntersectionCoordinate(otherLine._curve);
            if(IncludesCoordinate(intersection) && otherLine.IncludesCoordinate(intersection))
            {
                return intersection;
            }
            throw new ArgumentOutOfRangeException("Segments do not intersect between starting and ending coordinates.");
        }
        #endregion

        #region Methods (IPathDivisionExtension)
        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        ///  <paramref name="fraction"/> must be between 0 and 1.
        /// </summary>
        /// <param name="fraction">Fraction of the way from point 1 to point 2.</param>
        /// <returns></returns>
        public CartesianCoordinate PointDivision(double fraction)
        {
            if (!fraction.IsWithinInclusive(0, 1, Tolerance))
            {
                throw new ArgumentOutOfRangeException("Fraction must be within 0 and 1 to divide segment.");
            }
            return pointOffsetOnLine(fraction);
        }

        /// <summary>
        ///  Returns a point determined by a given ratio of the distance between point i and point j of the segment.
        /// </summary>
        /// <param name="ratio">Ratio of the size of the existing segment. 
        /// If <paramref name="ratio"/>&lt; 0, returned point is offset from point i, in that direction. 
        /// If <paramref name="ratio"/>&gt; 0, returned point is offset from point j, in that direction.</param>
        /// <returns></returns>
        public CartesianCoordinate PointExtension(double ratio)
        {
            if (ratio.IsGreaterThanOrEqualTo(0, Tolerance))
            {
                ratio += 1;
            }
            return pointOffsetOnLine(ratio);
        }

        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        /// </summary>
        /// <param name="fraction">Fraction of the way from point 1 to point 2.</param>
        /// <returns></returns>
        private CartesianCoordinate pointOffsetOnLine(double fraction)
        {
            double x = I.X + fraction * (J.X - I.X);
            double y = I.Y + fraction * (J.Y - I.Y);
            return new CartesianCoordinate(x, y);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Converts the line segment to a vector.
        /// </summary>
        /// <returns></returns>
        public Vector ToVector()
        {
            return new Vector(new CartesianCoordinate(I.X, I.Y), new CartesianCoordinate(J.X, J.Y));
        }
        #endregion
    }
}

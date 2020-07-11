using System;

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
    public class LineSegment: PathSegment<LinearCurve>, IPathDivisionExtension, ILine
    {
        #region Properties
        /// <summary>
        /// Curve that spans between the I and J coordinates.
        /// </summary>
        public override LinearCurve Curve => new LinearCurve(I, J);

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
            validateRelativePosition(sRelative);
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

        /// <summary>
        /// Returns a copy of the segment with an updated I coordinate.
        /// </summary>
        /// <param name="newCoordinate">The new coordinate.</param>
        /// <returns>IPathSegment.</returns>
        public override IPathSegment UpdateI(CartesianCoordinate newCoordinate)
        {
            return new LineSegment(newCoordinate, J);
        }

        /// <summary>
        /// Returns a copy of the segment with an updated J coordinate.
        /// </summary>
        /// <param name="newCoordinate">The new coordinate.</param>
        /// <returns>IPathSegment.</returns>
        public override IPathSegment UpdateJ(CartesianCoordinate newCoordinate)
        {
            return new LineSegment(I, newCoordinate);
        }
        #endregion

        #region Methods: Override (IPathSegmentCollision)
        /// <summary>
        /// Provided point lies on the line segment between or on the defining points.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool IncludesCoordinate(CartesianCoordinate point)
        {
            double tolerance = Helper.GetTolerance(point, Tolerance);
            if (!_curve.IsIntersectingCoordinate(point)) 
            { 
                return false; 
            }

            if (_curve.IsHorizontal())
            {
                return (point.X.IsGreaterThanOrEqualTo(_extents.MinX, tolerance) && point.X.IsLessThanOrEqualTo(_extents.MaxX, tolerance));
            }
            return (point.Y.IsGreaterThanOrEqualTo(_extents.MinY, tolerance) && point.Y.IsLessThanOrEqualTo(_extents.MaxY, tolerance));
        }

        /// <summary>
        /// Provided line segment intersects the line segment between or on the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public override bool IsIntersecting(LineSegment otherLine)
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
        public override CartesianCoordinate IntersectionCoordinate(LineSegment otherLine)
        {
            CartesianCoordinate intersection = _curve.IntersectionCoordinate(otherLine._curve);
            if(IncludesCoordinate(intersection) && otherLine.IncludesCoordinate(intersection))
            {
                return intersection;
            }
            throw new ArgumentOutOfRangeException("Segments do not intersect between starting and ending coordinates.");
        }
        #endregion

        #region Methods: PathDivisionExtension
        /// <summary>
        /// Splits the segment by the provided point.
        /// </summary>
        /// <param name="pointDivision">The point to use for division.</param>
        /// <returns>Tuple&lt;IPathSegment, IPathSegment&gt;.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Tuple<IPathSegment, IPathSegment> SplitBySegmentPoint(CartesianCoordinate pointDivision)
        {
            if (!IncludesCoordinate(pointDivision))
            {
                throw new ArgumentOutOfRangeException("Point does not lie on the segment being split.");
            }

            return new Tuple<IPathSegment, IPathSegment>(
                new LineSegment(I, pointDivision),
                new LineSegment(pointDivision, J)
                );
        }

        /// <summary>
        /// Extends the segment to the provided point.
        /// </summary>
        /// <param name="pointExtension">The point to extend the segment to.</param>
        /// <returns>IPathSegment.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IPathSegment ExtendSegmentToPoint(CartesianCoordinate pointExtension)
        {
            if (!_curve.IsIntersectingCoordinate(pointExtension))
            {
                throw new ArgumentOutOfRangeException("Point being extended to does not lie on the segment curve.");
            }
            // TODO: Determine if point is being extended to from I or J
            // TODO: Return curve with appropriate end extended to pointExtension
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        /// <paramref name="sRelative"/> must be between 0 and 1.
        /// </summary>
        /// <param name="sRelative">The relative position along the path between 0 (point i) and 1 (point j).</param>
        /// <returns></returns>
        protected override CartesianCoordinate pointOffsetOnCurve(double sRelative)
        {
            double x = I.X + sRelative * (J.X - I.X);
            double y = I.Y + sRelative * (J.Y - I.Y);
            return new CartesianCoordinate(x, y);
        }


        /// <summary>
        /// Returns a copy of the segment that merges the current segment with the prior segment.
        /// </summary>
        /// <param name="priorSegment">The prior segment.</param>
        /// <returns>IPathSegment.</returns>
        public override IPathSegment MergeWithPriorSegment(IPathSegment priorSegment)
        {
            if (priorSegment.I == I)
            {
                priorSegment = new LineSegment(priorSegment.J, priorSegment.I);
            }
            if (priorSegment.J == J)
            {
                return new LineSegment(priorSegment.I, I);
            }
            return new LineSegment(priorSegment.I, J);
        }

        /// <summary>
        /// Returns a copy of the segment that merges the current segment with the following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        public override IPathSegment MergeWithFollowingSegment(IPathSegment followingSegment)
        {
            if (followingSegment.I == I)
            {
                return new LineSegment(J, followingSegment.J);
            }
            if (followingSegment.J == J)
            {
                followingSegment = new LineSegment(followingSegment.J, followingSegment.I);
            }
            return new LineSegment(I, followingSegment.J);
        }

        /// <summary>
        /// Returns a copy of the segment that joins the current segment with the prior segment.
        /// </summary>
        /// <param name="priorSegment">The prior segment.</param>
        /// <returns>IPathSegment.</returns>
        public override IPathSegment JoinWithPriorSegment(IPathSegment priorSegment)
        {
            if ((priorSegment.I == I || priorSegment.I == J) ||
                (priorSegment.J == I || priorSegment.J == J))
            {  // segments already joined
                return null;
            }
            return new LineSegment(priorSegment.J, I);
        }

        /// <summary>
        /// Returns a copy of the segment that joins the current segment with the following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        public override IPathSegment JoinWithFollowingSegment(IPathSegment followingSegment)
        {
            if ((followingSegment.I == I || followingSegment.I == J) ||
                (followingSegment.J == I || followingSegment.J == J))
            {  // segments already joined
                return null;
            }
            return new LineSegment(J, followingSegment.I);
        }

        /// <summary>
        /// Returns a copy of the segment that splits the segment by the relative location.
        /// <paramref name="sRelative"/> must be between 0 and 1.
        /// </summary>
        /// <param name="sRelative">The relative position along the path between 0 (point i) and 1 (point j).</param>
        /// <returns>Tuple&lt;IPathSegment, IPathSegment&gt;.</returns>
        public override Tuple<IPathSegment, IPathSegment> SplitBySegmentPosition(double sRelative)
        {
            CartesianCoordinate pointDivision = PointOffsetOnSegment(sRelative);

            return SplitBySegmentPoint(pointDivision);
        }

        /// <summary>
        /// Extends the segment to intersect the provided curve.
        /// </summary>
        /// <returns>IPathSegment.</returns>
        public IPathSegment ExtendSegmentToCurve() // TODO: Add curve parameter
        {
            // Provide some curve to method
            // If never intersecting, throw exception
            // If intersecting, get intersection point and return 'ExtendPathToPoint'
            CartesianCoordinate pointExtension = new CartesianCoordinate(0, 0); // TODO: Finish this

            return ExtendSegmentToPoint(pointExtension);
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

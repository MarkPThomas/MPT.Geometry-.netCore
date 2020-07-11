using MPT.Geometry.Tools;
using MPT.Math;
using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Base class used for segment types.
    /// </summary>
    public abstract class PathSegment<T> : 
        IPathSegment,
        IPathSegmentCollision<T>,
        ITolerance
        where T : Curve
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        public double Tolerance { get; set; } = 10E-6;

        /// <summary>
        /// The curve
        /// </summary>
        protected T _curve;
        /// <summary>
        /// Gets the curve.
        /// </summary>
        /// <value>The curve.</value>
        public virtual T Curve => _curve; //.Clone();

        /// <summary>
        /// The extents
        /// </summary>
        protected PointExtents _extents;
        /// <summary>
        /// Gets or sets the extents.
        /// </summary>
        /// <value>The extents.</value>
        public PointExtents Extents => _extents.Clone();

        /// <summary>
        /// First coordinate value.
        /// </summary>
        public virtual CartesianCoordinate I { get; set; }

        /// <summary>
        /// Second coordinate value.
        /// </summary>
        public virtual CartesianCoordinate J { get; set; }

        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the segment to span between the provided points.
        /// </summary>
        /// <param name="i">First point of the line.</param>
        /// <param name="j">Second point of the line.</param>
        protected PathSegment(CartesianCoordinate i, CartesianCoordinate j)
        {
            I = i;
            J = j;
            _extents = new PointExtents(i, j);
        }
        #endregion

        #region Methods: Public        
        /// <summary>
        /// Determines whether the segment [has same coordinates] as [the specified segment].
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if [has same coordinates] [the specified segment]; otherwise, <c>false</c>.</returns>
        public bool HasSameCoordinates(IPathSegment segment)
        {
            return ((I == segment.I) && (J == segment.J));
        }

        /// <summary>
        /// Vector that is tangential to the line connecting the defining points.
        /// </summary>
        /// <returns></returns>
        public Vector TangentVector()
        {
            return Vector.UnitTangentVector(I, J);
        }

        /// <summary>
        /// Vector that is normal to the line connecting the defining points.
        /// </summary>
        /// <returns></returns>
        public Vector NormalVector()
        {
            return Vector.UnitNormalVector(I, J);
        }
        #endregion

        #region Methods: Protected        
        /// <summary>
        /// Validates the relative position provided.
        /// </summary>
        /// <param name="sRelative">The relative position, s.</param>
        /// <exception cref="ArgumentOutOfRangeException">Relative position must be between 0 and 1, but was {sRelative}.</exception>
        protected void validateRelativePosition(double sRelative)
        {
            if (!sRelative.IsWithinInclusive(0, 1, Tolerance))
            {
                throw new ArgumentOutOfRangeException($"Relative position must be between 0 and 1, but was {sRelative}.");
            }
        }

        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        /// <paramref name="sRelative"/> must be between 0 and 1.
        /// </summary>
        /// <param name="sRelative">The relative position along the path between 0 (point i) and 1 (point j).</param>
        /// <returns></returns>
        protected abstract CartesianCoordinate pointOffsetOnCurve(double sRelative);
        #endregion

        #region Methods: IPathSegment

        /// <summary>
        /// Length of the path segment.
        /// </summary>
        /// <returns></returns>
        public abstract double Length();


        /// <summary>
        /// X-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        public abstract double Xo();

        /// <summary>
        /// Y-coordinate of the centroid of the line.
        /// </summary>
        /// <returns></returns>
        public abstract double Yo();

        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public abstract double X(double y);

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public abstract double Y(double x);

        /// <summary>
        /// Coordinate on the path that corresponds to the position along the path.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        /// <returns></returns>
        public abstract CartesianCoordinate PointByPathPosition(double sRelative);

        /// <summary>
        /// Vector that is normal to the line connecting the defining points at the position specified.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        public abstract Vector NormalVector(double sRelative);

        /// <summary>
        /// Vector that is tangential to the line connecting the defining points at the position specified.
        /// </summary>
        /// <param name="sRelative">The relative position along the path.</param>
        public abstract Vector TangentVector(double sRelative);
        #endregion

        #region Methods: IPathTransform        
        /// <summary>
        /// Translates the segment.
        /// </summary>
        /// <param name="translation">The amount to translate by.</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment Translate(CartesianOffset translation)
        {
            return new LineSegment(I + translation, J + translation);
        }

        /// <summary>
        /// Scales the segment from point I.
        /// </summary>
        /// <param name="scaleFromI">The amount to scale from point I.</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment ScaleFromI(double scaleFromI)
        {
            return ScaleFromPoint(scaleFromI, I);
        }

        /// <summary>
        /// Scales the segment from point J.
        /// </summary>
        /// <param name="scaleFromJ">The amount to scale from point J.</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment ScaleFromJ(double scaleFromJ)
        {
            return ScaleFromPoint(scaleFromJ, J);
        }

        /// <summary>
        /// Scales the segment from the provided reference point.
        /// </summary>
        /// <param name="scale">The amount to scale relative to the reference point.</param>
        /// <param name="referencePoint">The reference point.</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment ScaleFromPoint(double scale, CartesianCoordinate referencePoint)
        {
            CartesianOffset offsetJ = scale * (J.OffsetFrom(referencePoint));
            CartesianOffset offsetI = scale * (I.OffsetFrom(referencePoint));

            return new LineSegment(
                referencePoint + offsetI.ToCartesianCoordinate(),
                referencePoint + offsetJ.ToCartesianCoordinate());
        }

        /// <summary>
        /// Rotates the segment from point I.
        /// </summary>
        /// <param name="rotation">The amount of rotation. [rad]</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment RotateAboutI(Angle rotation)
        {
            return new LineSegment(I, CartesianCoordinate.RotateAboutPoint(J, I, rotation.Radians));
        }

        /// <summary>
        /// Rotates the segment from point J.
        /// </summary>
        /// <param name="rotation">The amount of rotation. [rad]</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment RotateAboutJ(Angle rotation)
        {
            return new LineSegment(CartesianCoordinate.RotateAboutPoint(I, J, rotation.Radians), J);
        }

        /// <summary>
        /// Rotates the segment about the reference point.
        /// </summary>
        /// <param name="rotation">The amount of rotation. [rad]</param>
        /// <param name="referencePoint">The center of rotation reference point.</param>
        /// <returns>IPathSegment.</returns>
        public virtual IPathSegment RotateAboutPoint(Angle rotation, CartesianCoordinate referencePoint)
        {
            return new LineSegment(
                CartesianCoordinate.RotateAboutPoint(I, referencePoint, rotation.Radians),
                CartesianCoordinate.RotateAboutPoint(J, referencePoint, rotation.Radians));
        }
        #endregion

        #region Methods: IPathDivisionExtension
        /// <summary>
        /// Returns a point determined by a given fraction of the distance between point i and point j of the segment.
        /// <paramref name="sRelative"/> must be between 0 and 1.
        /// </summary>
        /// <param name="sRelative">The relative position along the path between 0 (point i) and 1 (point j).</param>
        /// <returns></returns>
        public CartesianCoordinate PointOffsetOnSegment(double sRelative)
        {
            validateRelativePosition(sRelative);
            return pointOffsetOnCurve(sRelative);
        }

        /// <summary>
        ///  Returns a point determined by a given ratio of the distance between point i and point j of the segment.
        /// </summary>
        /// <param name="ratio">Ratio of the size of the existing segment. 
        /// If <paramref name="ratio"/>&lt; 0, returned point is offset from point i, in that direction. 
        /// If <paramref name="ratio"/>&gt; 0, returned point is offset from point j, in that direction.</param>
        /// <returns></returns>
        public CartesianCoordinate PointScaledFromSegment(double ratio)
        {
            if (ratio.IsGreaterThanOrEqualTo(0, Tolerance))
            {
                ratio += 1;
            }
            return pointOffsetOnCurve(ratio);
        }

        /// <summary>
        /// Returns a copy of the segment with an updated I coordinate.
        /// </summary>
        /// <param name="newCoordinate">The new coordinate.</param>
        /// <returns>IPathSegment.</returns>
        public abstract IPathSegment UpdateI(CartesianCoordinate newCoordinate);

        /// <summary>
        /// Returns a copy of the segment with an updated J coordinate.
        /// </summary>
        /// <param name="newCoordinate">The new coordinate.</param>
        /// <returns>IPathSegment.</returns>
        public abstract IPathSegment UpdateJ(CartesianCoordinate newCoordinate);

        /// <summary>
        /// Returns a copy of the segment that merges the current segment with the prior segment.
        /// </summary>
        /// <param name="priorSegment">The prior segment.</param>
        /// <returns>IPathSegment.</returns>
        public abstract IPathSegment MergeWithPriorSegment(IPathSegment priorSegment);

        /// <summary>
        /// Returns a copy of the segment that merges the current segment with the following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        public abstract IPathSegment MergeWithFollowingSegment(IPathSegment followingSegment);

        /// <summary>
        /// Returns a copy of the segment that joins the current segment with the prior segment.
        /// </summary>
        /// <param name="priorSegment">The prior segment.</param>
        /// <returns>IPathSegment.</returns>
        public abstract IPathSegment JoinWithPriorSegment(IPathSegment priorSegment);

        /// <summary>
        /// Returns a copy of the segment that joins the current segment with the following segment.
        /// </summary>
        /// <param name="followingSegment">The following segment.</param>
        /// <returns>IPathSegment.</returns>
        public abstract IPathSegment JoinWithFollowingSegment(IPathSegment followingSegment);

        /// <summary>
        /// Returns a copy of the segment that splits the segment by the relative location.
        /// <paramref name="sRelative"/> must be between 0 and 1.
        /// </summary>
        /// <param name="sRelative">The relative position along the path between 0 (point i) and 1 (point j).</param>
        /// <returns>Tuple&lt;IPathSegment, IPathSegment&gt;.</returns>
        public abstract Tuple<IPathSegment, IPathSegment> SplitBySegmentPosition(double sRelative);
        #endregion

        #region IPathSegmentCollision
        /// <summary>
        /// Provided point lies on the line segment between or on the defining points.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public abstract bool IncludesCoordinate(CartesianCoordinate point);

        /// <summary>
        /// Provided line segment intersects the line segment between or on the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public abstract bool IsIntersecting(LineSegment otherLine);

        /// <summary>
        /// Returns a point where the line segment intersects the provided line segment.
        /// </summary>
        /// <param name="otherLine">Line segment that intersects the current line segment.</param>
        /// <returns></returns>
        public abstract CartesianCoordinate IntersectionCoordinate(LineSegment otherLine);
        #endregion

        #region Methods: IEquatable        
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public virtual bool Equals(IPathSegment other)
        {
            return HasSameCoordinates(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is IPathSegment) { return Equals((IPathSegment)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return I.GetHashCode() ^ J.GetHashCode();
        }
        #endregion
    }
}

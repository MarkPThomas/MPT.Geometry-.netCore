// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark P Thomas
// Created          : 06-20-2018
//
// Last Modified By : Mark P Thomas
// Last Modified On : 06-11-2020
// ***********************************************************************
// <copyright file="PolyLine.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Geometry.Tools;
using MPT.Math;
using MPT.Math.Coordinates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Class PolyLine.
    /// </summary>
    public class PolyLine : ITolerance, IEnumerable<IPathSegment>
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; } = GeometryLibrary.ZeroTolerance;

        /// <summary>
        /// Gets the count coordinates.
        /// </summary>
        /// <value>The count coordinates.</value>
        public int CountPoints => _segmentBoundary.PointBoundary().Count;
        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        /// <value>The coordinates.</value>
        public IList<CartesianCoordinate> Coordinates => _segmentBoundary.PointBoundary(); 

        /// <summary>
        /// The segment boundary
        /// </summary>
        private SegmentsBoundary _segmentBoundary;
        /// <summary>
        /// Gets the count segments.
        /// </summary>
        /// <value>The count segments.</value>
        public int CountSegments => _segmentBoundary.Count;
        /// <summary>
        /// Gets or sets the <see cref="IPathSegment"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>IPathSegment.</returns>
        public IPathSegment this[int index]
        {
            get => _segmentBoundary[index];
            set
            {
                throw new ReadOnlyException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly => true;


        // AKA Control points. TODO: Define tangent/control points further as additional path types are created.
        //protected IList<bool> _tangentsAreAlignedAtPointJ; 
        //public bool DefaultNewTangentsAreAlignedAtPointJ { get; set; } = true;

        #endregion

        #region Initialization                     
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLine" /> class.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public PolyLine(CartesianCoordinate i, CartesianCoordinate j)
        {
            _segmentBoundary = new SegmentsBoundary(new List<IPathSegment>() { new LineSegment(i, j) });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLine" /> class.
        /// </summary>
        /// <param name="segment">The segment.</param>
        public PolyLine(IPathSegment segment)
        {
            _segmentBoundary = new SegmentsBoundary(new List<IPathSegment>() { segment });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyLine" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public PolyLine(IEnumerable<CartesianCoordinate> coordinates)
        {
            _segmentBoundary = new SegmentsBoundary();
            IList<CartesianCoordinate> boundaryCoordinates = new List<CartesianCoordinate>(coordinates);
            if (boundaryCoordinates.Count > 1)
            {
                for (int i = 0; i < boundaryCoordinates.Count - 1; i++)
                {
                    _segmentBoundary.AddLast(new LineSegment(boundaryCoordinates[i], boundaryCoordinates[i + 1]));
                }
            }
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns the points that define the boundary between segments.
        /// </summary>
        /// <returns>PointBoundary.</returns>
        public PointBoundary PointBoundary()
        {
            return _segmentBoundary.PointBoundary();
        }

        /// <summary>
        /// Returns the overall extents of the polyline. 
        /// This includes extents for curve shapes in between vertices.
        /// </summary>
        /// <returns>PointExtents.</returns>
        public PointExtents Extents()
        {
            return _segmentBoundary.Extents();
        }
        #endregion

        #region Points        
        /// <summary>
        /// Coordinates the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate Coordinate(int index)
        {
            return _segmentBoundary.PointBoundary()[index];
        }

        /// <summary>
        /// Returns the first point of the polyline.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate FirstPoint()
        {
            return _segmentBoundary.FirstPoint();
        }

        /// <summary>
        /// Returns the last point of the polyline.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate LastPoint()
        {
            return _segmentBoundary.LastPoint();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="point">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>true if <paramref name="point">item</paramref> is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.</returns>
        public bool ContainsPoint(CartesianCoordinate point)
        {
            return _segmentBoundary.PointBoundary().Contains(point);
        }

        /// <summary>
        /// Adds the point as the first point in the polyline.
        /// Segment created will be of the same type as the prior first segment.
        /// </summary>
        /// <param name="point">The point.</param>
        public bool AddFirstPoint(CartesianCoordinate point)
        {
            IPathSegment firstSegment = _segmentBoundary[0];
            IPathSegment newSegment = firstSegment.UpdateI(point);
            newSegment = newSegment.UpdateJ(firstSegment.I);
            return _segmentBoundary.AddFirst(newSegment);
        }

        /// <summary>
        /// Adds the point as the last point in the polyline.
        /// Segment created will be the same type as the prior last segment.
        /// </summary>
        /// <param name="point">The point.</param>
        public bool AddLastPoint(CartesianCoordinate point)
        {
            IPathSegment lastSegment = _segmentBoundary[CountSegments - 1];
            IPathSegment newSegment = lastSegment.UpdateJ(point);
            newSegment = newSegment.UpdateI(lastSegment.J);
            return _segmentBoundary.AddLast(newSegment);
        }

        /// <summary>
        /// Removes the first point and corresponding segment.
        /// </summary>
        public bool RemoveFirstPoint()
        {
            return _segmentBoundary.RemoveFirst();
        }

        /// <summary>
        /// Removes the last point and corresponding segment.
        /// </summary>
        public bool RemoveLastPoint()
        {
            return _segmentBoundary.RemoveLast();
        }


        public bool RemovePoint(CartesianCoordinate point)
        {
            return _segmentBoundary.RemovePoint(point);
        }


        public bool MovePoint(CartesianCoordinate originalPoint, CartesianCoordinate newPoint)
        {
            return _segmentBoundary.MovePoint(originalPoint, newPoint);
        }
        #endregion

        //#region Point Tangents
        //public void AlignPointTangents(int index)
        //{

        //}
        //public void AlignPointTangentsToLeadingSegment(int index)
        //{

        //}
        //public void AlignPointTangentsToFollowingSegment(int index)
        //{

        //}
        //#endregion

        #region Segments
        /// <summary>
        /// Returns the segment at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>IPathSegment.</returns>
        public IPathSegment Segment(int index)
        {
            return _segmentBoundary[index];
        }

        /// <summary>
        /// Determines whether the specified polyline contains the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns><c>true</c> if the polyline contains the specified segment; otherwise, <c>false</c>.</returns>
        public bool ContainsSegment(IPathSegment segment)
        {
            return _segmentBoundary.Contains(segment);
        }

        /// <summary>
        /// Adds the segment as the first segment in the polyline.
        /// Segment created will be of the same type as the prior first segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        public bool AddFirstSegment(IPathSegment segment)
        {
            if (!_segmentBoundary.IsValidFirstSegment(segment))
            {
                throw new ArgumentException(
                    "Segment provided is invalid for adding to the beginning of the polyline. " +
                    "Point J must be the same as the first point of the polyline");
            }
            return _segmentBoundary.AddFirst(segment);
        }

        /// <summary>
        /// Adds the segment as the last segment in the polyline.
        /// Segment created will be the same type as the prior last segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        public bool AddLastSegment(IPathSegment segment)
        {
            if (!_segmentBoundary.IsValidLastSegment(segment))
            {
                throw new ArgumentException(
                    "Segment provided is invalid for adding to the end of the polyline. " +
                    "Point I must be the same as the last point of the polyline");
            }
            return _segmentBoundary.AddLast(segment);
        }

        public bool SplitSegment(IPathSegment segment, double sRelative)
        {
            return _segmentBoundary.SplitSegment(segment, sRelative);
        }

        /// <summary>
        /// Removes the first segment and corresponding point.
        /// </summary>
        public bool RemoveFirstSegment()
        {
            return _segmentBoundary.RemoveFirst();
        }

        /// <summary>
        /// Removes the last segment and corresponding point.
        /// </summary>
        public bool RemoveLastSegment()
        {
            return _segmentBoundary.RemoveLast();
        }

        // TODO: Unhide and test these once more than one segment type is available.
        ///// <summary>
        ///// Resets the segment to the default <see cref="LineSegment"></see>.
        ///// </summary>
        ///// <param name="segment">The segment.</param>
        ///// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        //public bool ResetSegmentToDefault(IPathSegment segment)
        //{
        //    int index = _segmentBoundary.IndexOf(segment);
        //    if (index < 0)
        //    {
        //        return false;
        //    }

        //    IPathSegment existingSegment = _segmentBoundary[index];
        //    _segmentBoundary[index] = new LineSegment(existingSegment.I, existingSegment.J);
        //    return true;
        //}

        ///// <summary>
        ///// Changes the segment in the polyine to the provided segment if they have matching starting and ending coordinates.
        ///// </summary>
        ///// <param name="segment">The segment.</param>
        ///// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        //public bool ChangeSegment(IPathSegment segment)
        //{
        //    int index = _segmentBoundary.IndexOf(segment);
        //    if (index < 0)
        //    {
        //        return false;
        //    }

        //    _segmentBoundary[index] = segment;
        //    return true;
        //}
        #endregion

        #region Enumerator
        /// <summary>
        /// Gets the coordinate enumerator.
        /// </summary>
        /// <value>The coordinate enumerator.</value>
        public IEnumerator<CartesianCoordinate> GetCoordinateEnumerator()
        {
            return _segmentBoundary.PointBoundary().GetEnumerator();
        }
        /// <summary>
        /// Gets the segment enumerator.
        /// </summary>
        /// <value>The segment enumerator.</value>
        public IEnumerator<IPathSegment> GetSegmentEnumerator()
        {
            return _segmentBoundary.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<IPathSegment> GetEnumerator()
        {
            return _segmentBoundary.GetEnumerator();
        }

        // ncrunch: no coverage start
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        // ncrunch: no coverage end
        #endregion

        #region Methods: Private

        #endregion
    }
}

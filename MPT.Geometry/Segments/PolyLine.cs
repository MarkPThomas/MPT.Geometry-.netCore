using MPT.Geometry.Tools;
using MPT.Math.Coordinates;
using System.Collections.Generic;

namespace MPT.Geometry.Segments
{
    /// <summary>
    /// Class PolyLine.
    /// </summary>
    public class PolyLine
    {
        private PointBoundary _pointBoundary;
        private IList<IPathSegment> _segments;
        
        // AKA Control points. TODO: Define tangent/control points further as additional path types are created.
        //protected IList<bool> _tangentsAreAlignedAtPointJ; 
        //public bool DefaultNewTangentsAreAlignedAtPointJ { get; set; } = true;

        public PolyLine(IPathSegment segment)
        {
            _pointBoundary = new PointBoundary(new List<CartesianCoordinate>() { segment.I, segment.J });
            _segments = new List<IPathSegment>() { segment };
        }

        #region Points
        public CartesianCoordinate FirstPoint()
        {
            return _pointBoundary.Coordinates[0];
        }
        public CartesianCoordinate LastPoint()
        {
            return _pointBoundary.Coordinates[_pointBoundary.Coordinates.Count - 1];
        }

        public void AddPoint(CartesianCoordinate point)
        {
            // Always adds LinearSegment by default
        }

        public void RemovePoint(CartesianCoordinate point)
        {
            removePoint();
        }
        public void RemovePoint(int index)
        {
            removePoint();
        }
        private void removePoint()
        {
            // If at end, removes tangent, segment
            // Otherwise, if segment I & J are the same, add segment of type with i & j+2 control points
            // Otherwise, if segment I & J are different, default to LinearSegment for newly added segment w/ reset control points
        }


        public void InsertPoint(CartesianCoordinate point, int index)
        {
            // Always adds 2 segments of type split, with constrained tangent matching existing tangent.
        }
        #endregion

        #region Point Tangents
        //public void AlignPointTangents(int index)
        //{

        //}
        //public void AlignPointTangentsToLeadingSegment(int index)
        //{

        //}
        //public void AlignPointTangentsToFollowingSegment(int index)
        //{

        //}
        #endregion

        #region Segments
        public void AddSegment<T>(CartesianCoordinate ptJ) where T : PathSegment
        {
            //T pathSegment = new T(LastPoint(), ptJ);
            //_segments.Add(pathSegment);
            //_pointBoundary.Add(ptJ);
            //_tangentsAreAlignedAtPointJ.Add(DefaultNewTangentsAreAlignedAtPointJ);
        }
        public void ResetSegmentToDefault(int index)
        {
            // resets to LinearSegment
        }
        public void RemoveSegment(int index)
        {
            // always removes associated J-coordinate
            // RemovePoint(index) takes care of what is done with segments & vetices
        }
        public void ChangeSegment<T>(int index)
        {

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.Geometry.Shapes
{
    /// <summary>
    /// Interface for all paths that create a closed shape where all sides and angles are congruent.
    /// </summary>
    public interface IRegularPolygon : IShapeProperties
    {
        /// <summary>
        /// A circle that passes through all vertices of a plane figure and contains the entire figure in its interior. 
        /// All triangles have circumcircles and so do all regular polygons. Most other polygons do not.
        /// </summary>
        double Circumcirle { get; }

        /// <summary>
        /// The line segment from the center of a regular polygon to the midpoint of a side, or the length of this segment. 
        /// Same as the inradius; that is, the radius of a regular polygon's inscribed circle.
        /// </summary>
        double Apothem { get; }

        /// <summary>
        /// Number of sides (n) of the polygon.
        /// </summary>
        int NumberOfSides { get; }

        /// <summary>
        /// Length of any side of the polygon.
        /// </summary>
        double SideLength { get; }

        /// <summary>
        /// Angle between any two sides of the polygon on the inside of the shape.
        /// </summary>
        double InteriorAngle { get; }

        /// <summary>
        /// Sum of all interior angles of the shape.
        /// </summary>
        double InteriorAngleSum { get; }
    }
}

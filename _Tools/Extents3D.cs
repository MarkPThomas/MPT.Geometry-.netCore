// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Extents.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.Math;
using NMath = System.Math;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Represents the coordinate bounds of a shape or line, or cluster of points in 3D-space.
    /// </summary>
    public class Extents3D
    {
        /// <summary>
        /// The maximum Y-coordinate
        /// </summary>
        protected double _maxY = double.NegativeInfinity;
        /// <summary>
        /// The minimum Y-coordinate
        /// 
        /// </summary>
        protected double _minY = double.PositiveInfinity;

        /// <summary>
        /// The maximum X-coordinate
        /// </summary>
        protected double _maxX = double.NegativeInfinity;
        /// <summary>
        /// The minimum X-coordinate
        /// </summary>
        protected double _minX = double.PositiveInfinity;

        /// <summary>
        /// The maximum Z-coordinate
        /// </summary>
        protected double _maxZ = double.NegativeInfinity;
        /// <summary>
        /// The minimum Z-coordinate
        /// </summary>
        protected double _minZ = double.PositiveInfinity;

        /// <summary>
        /// Gets the maximum Y-coordinate.
        /// </summary>
        /// <value>The maximum Y-coordinate.</value>
        public double MaxY { get; private set; }
        /// <summary>
        /// Gets the minimum Y-coordinate.
        /// </summary>
        /// <value>The minimum Y-coordinate.</value>
        public double MinY { get; private set; }

        /// <summary>
        /// Gets the maximum X-coordinate.
        /// </summary>
        /// <value>The maximum X-coordinate.</value>
        public double MaxX { get; private set; }
        /// <summary>
        /// Gets the minimum X-coordinate.
        /// </summary>
        /// <value>The minimum X-coordinate.</value>
        public double MinX { get; private set; }

        /// <summary>
        /// Gets the maximum Z-coordinate.
        /// </summary>
        /// <value>The maximum Z-coordinate.</value>
        public double MaxZ { get; private set; }
        /// <summary>
        /// Gets the minimum Z-coordinate.
        /// </summary>
        /// <value>The minimum Z-coordinate.</value>
        public double MinZ { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3D"/> class.
        /// </summary>
        public Extents3D()
        {
            initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3D"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public Extents3D(IEnumerable<Point3D> coordinates)
        {
            initialize();
            Add(coordinates);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3D"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public Extents3D(Extents3D extents)
        {
            initialize();
            Add(new Point3D(extents.MaxX, extents.MaxY, extents.MaxZ));
            Add(new Point3D(extents.MaxX, extents.MinY, extents.MaxZ));
            Add(new Point3D(extents.MinX, extents.MinY, extents.MaxZ));
            Add(new Point3D(extents.MinX, extents.MaxY, extents.MaxZ));
            Add(new Point3D(extents.MaxX, extents.MaxY, extents.MinZ));
            Add(new Point3D(extents.MaxX, extents.MinY, extents.MinZ));
            Add(new Point3D(extents.MinX, extents.MinY, extents.MinZ));
            Add(new Point3D(extents.MinX, extents.MaxY, extents.MinZ));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3D"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        /// <param name="offsetZ">The vertical to project the extents object from.</param>
        public Extents3D(Extents extents, double offsetZ)
        {
            initialize();
            Add(extents, offsetZ);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void initialize()
        {
            MaxY = _maxY;
            MinY = _minY;
            MaxX = _maxX;
            MinX = _minX;
            MaxZ = _maxZ;
            MinZ = _minZ;
        }

        /// <summary>
        /// Updates the extents to include the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void Add(Point3D coordinate)
        {
            if (coordinate.Y > MaxY)
            {
                MaxY = NMath.Min(coordinate.Y, _minY);
            }
            if (coordinate.Y < MinY)
            {
                MinY = NMath.Max(coordinate.Y, _maxY);
            }

            if (coordinate.X > MaxX)
            {
                MaxX = NMath.Min(coordinate.X, _minX);
            }
            if (coordinate.X < MinX)
            {
                MinX = NMath.Max(coordinate.X, _maxX);
            }
            
            if (coordinate.Z > MaxZ)
            {
                MaxZ = NMath.Min(coordinate.Z, _minZ);
            }
            if (coordinate.Z < MinZ)
            {
                MinZ = NMath.Max(coordinate.Z, _maxZ);
            }
        }

        /// <summary>
        /// Updates the extents to include the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public void Add(IEnumerable<Point3D> coordinates)
        {
            foreach (Point3D coordinate in coordinates)
            {
                Add(coordinate);
            }
        }

        /// <summary>
        /// Adds the specified extents.
        /// </summary>
        /// <param name="extents">The extents.</param>
        /// <param name="zCoordinate">The z-coordinate of the extents object.</param>
        public void Add(Extents extents, double zCoordinate)
        {
            List<Point3D> points = new List<Point3D>()
                                    {
                                        new Point3D(extents.MaxX, extents.MaxY, zCoordinate),
                                        new Point3D(extents.MaxX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MaxY, zCoordinate),
                                        new Point3D(extents.MaxX, extents.MaxY, zCoordinate),
                                        new Point3D(extents.MaxX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MaxY, zCoordinate),
                                    };
            Add(points);
        }

        /// <summary>
        /// Updates the extents to include the specified extents.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public void Add(Extents3D extents)
        {
            if (extents.MaxY > MaxY)
            {
                MaxY = extents.MaxY;
            }
            if (extents.MinY < MinY)
            {
                MinY = extents.MinY;
            }

            if (extents.MaxX > MaxX)
            {
                MaxX = extents.MaxX;
            }
            if (extents.MinX < MinX)
            {
                MinX = extents.MinX;
            }

            if (extents.MaxZ > MaxZ)
            {
                MaxZ = extents.MaxZ;
            }
            if (extents.MinZ < MinZ)
            {
                MinZ = extents.MinZ;
            }
        }



        /// <summary>
        /// Projects this instance to a 2-dimensional extents object in the X-Y plane.
        /// </summary>
        /// <returns>Extents.</returns>
        public Extents ProjectXY()
        {
            Extents extents = new Extents();
            extents.Add(new Point(MaxX, MaxY));
            extents.Add(new Point(MaxX, MinY));
            extents.Add(new Point(MinX, MinY));
            extents.Add(new Point(MinX, MaxY));
            return extents;
        }

        /// <summary>
        /// Projects this instance to a 2-dimensional extents object in the X-Z plane, where the y-coordinate is to be taken as the y-coordinate.
        /// </summary>
        /// <returns>Extents.</returns>
        public Extents ProjectXZ()
        {
            Extents extents = new Extents();
            extents.Add(new Point(MaxX, MaxZ));
            extents.Add(new Point(MaxX, MinZ));
            extents.Add(new Point(MinX, MinZ));
            extents.Add(new Point(MinX, MaxZ));
            return extents;
        }

        /// <summary>
        /// Projects this instance to a 2-dimensional extents object in the Y-Z plane, where the x-coordinate is to be taken as the z-coordinate.
        /// </summary>
        /// <returns>Extents.</returns>
        public Extents ProjectYZ()
        {
            Extents extents = new Extents();
            extents.Add(new Point(MaxZ, MaxY));
            extents.Add(new Point(MaxZ, MinY));
            extents.Add(new Point(MinZ, MinY));
            extents.Add(new Point(MinZ, MaxY));
            return extents;
        }



        /// <summary>
        /// Determines whether the coordinate lies within the extents.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the extents; otherwise, <c>false</c>.</returns>
        public bool IsWithinExtents(Point3D coordinate)
        {
            return ((MinX <= coordinate.X && coordinate.X <= MaxX) &&
                    (MinY <= coordinate.Y && coordinate.Y <= MaxY) &&
                    (MinZ <= coordinate.Z && coordinate.Z <= MaxZ));
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public Extents3D Clone()
        {
            return new Extents3D(this);
        }
    }
}

using Autodesk.AutoCAD.Geometry;
using System;
using System.Runtime.CompilerServices;

namespace AcBlockAtributeIncrement
{
    public class TextInfo
    {
        public Point3d Alignment
        {
            get;
            set;
        }

        public bool IsAligned
        {
            get;
            set;
        }

        public Point3d Position
        {
            get;
            set;
        }

        public TextInfo(Point3d position, Point3d alignment, bool aligned)
        {
            this.Position = position;
            this.Alignment = alignment;
            this.IsAligned = aligned;
        }
    }
}
using GeometryLib;
using ICNCLib;

namespace CNCLib
{

    public class ArcPathEntity : PathEntity
    {

        public ArcSpecType ArcType { get; set; }
        public ArcPlane ArcPlane { get; set; }
        public bool FullArcFlag { get; set; }
        public double Icoordinate { get; set; }
        public double Jcoordinate { get; set; }
        public double Kcoordinate { get; set; }

        public double SweepAngle { get; set; }
        public double StartAngleRad { get; set; }
        public double Radius { get; set; }
        public Vector3 CenterPoint { get; set; }


        internal ArcPathEntity(BlockType type)
        {

            CenterPoint = new Vector3();
            ArcType = ArcSpecType.IJKRelative;
            ArcPlane = ArcPlane.XY;
            base.Type = type;
        }
    }
}

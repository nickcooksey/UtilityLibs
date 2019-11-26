using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{


    public class PointCyl : DwgEntity, IPointCyl
    {
        public double R { get; set; }
        public double ThetaRad { get; set; }
        public double ThetaDegPosOnly { get { return ToDegsPosOnly(ThetaRad); } }
        public double ThetaDeg { get { return ToDegs(ThetaRad); } }
        public double Z { get; set; }
        public static string CSVHeader()
        {
            return "R,Theta(degs),Z";
        }
        public PointCyl()
        {

            Col = System.Drawing.Color.FromArgb(255, 255, 255);
        }
        public PointCyl(IVector3 pt)
        {
            R = Math.Sqrt(Math.Pow(pt.X, 2) + Math.Pow(pt.Y, 2));
            ThetaRad = Math.Atan2(pt.Y, pt.X);
            Z = pt.Z;
            Col = pt.Col;
        }
        public PointCyl(double rIn, double thetaInRad, double zIn, System.Drawing.Color col, int id)
        {
            Init(rIn, thetaInRad, zIn, col, id);
        }
        public PointCyl(double rIn, double thetaInRad, double zIn, int id)
        {
            var col = System.Drawing.Color.FromArgb(255, 255, 255);
            Init(rIn, thetaInRad, zIn, col, id);
        }
        public PointCyl(double rIn, double thetaInRad, double zIn)
        {
            int id = 0;
            var col = System.Drawing.Color.FromArgb(255, 255, 255);
            Init(rIn, thetaInRad, zIn, col, id);
        }
        public PointCyl(double rIn, double thetaInRad, double zIn, System.Drawing.Color col)
        {
            int id = 0;
            Init(rIn, thetaInRad, zIn, col, id);
        }
        private void Init(double rIn, double thetaInRad, double zIn, System.Drawing.Color col, int id)
        {
            R = rIn;
            ThetaRad = thetaInRad;
            Z = zIn;
            ID = id;
            Col = System.Drawing.Color.FromArgb(col.R, col.G, col.B);
        }
        public IBoundingBox BoundingBox()
        {
            Vector3 v = new Vector3(this);
            IBoundingBox ext = new BoundingBox(0, 0, 0, v.X, v.Y, v.Z);
            return ext;

        }
        public static double ToDegs(double thetaRad)
        {
            try
            {
                var degs = 180 * thetaRad / Math.PI;
                return degs;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static double ToDegsPosOnly(double thetaRad)
        {
            try
            {
                var degs = 180 * thetaRad / Math.PI;
                while (degs < 0)
                {
                    degs += 360;
                }
                degs %= 360;
                return degs;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static double ToRadians(double thetaDegs)
        {
            double radians = Math.PI * thetaDegs / 180;

            return radians;

        }
        public IPointCyl InterpolateR(double radius, IPointCyl ptLow, IPointCyl ptHigh)
        {
            try
            {
                var dr = ptHigh.R - ptLow.R;

                var dz = ptHigh.Z - ptLow.Z;
                var dt = ptHigh.ThetaRad - ptLow.ThetaRad;
                if (dr == 0)
                {
                    return ptLow;
                }
                else
                {
                    var proportion = (radius - ptLow.R) / dr;
                    var z = (proportion * dz) + ptLow.Z;
                    var t = (proportion * dt) + ptLow.ThetaRad;
                    return new PointCyl(radius, t, z);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IVector3 UnrollCylPt(double scaling, double unrollingRadius)
        {
            try
            {
                var result = new Vector3(ThetaRad * unrollingRadius, R * scaling, Z, Col);
                return result;
            }

            catch (Exception)
            {

                throw;
            }
        }
        public IPointCyl Translate(IVector3 translation)
        {
            IVector3 ptXYZ = new Vector3(this);
            IVector3 ptTrans = ptXYZ.Translate(translation);
            PointCyl ptCylTrans = new PointCyl(ptTrans);
            ptCylTrans.Col = Col;
            return ptCylTrans;
        }
        public IPointCyl RotateX(IVector3 rotationPt, double angleRad)
        {
            IVector3 ptXYZ = new Vector3(this);
            IVector3 ptRot = ptXYZ.RotateX(rotationPt, angleRad);
            IPointCyl ptCylRot = new PointCyl(ptRot);
            ptCylRot.Col = Col;
            return ptCylRot;
        }
        public IPointCyl RotateY(IVector3 rotationPt, double angleRad)
        {
            IVector3 ptXYZ = new Vector3(this);
            IVector3 ptRot = ptXYZ.RotateY(rotationPt, angleRad);
            IPointCyl ptCylRot = new PointCyl(ptRot);
            ptCylRot.Col = Col;
            return ptCylRot;
        }
        public IPointCyl RotateZ(IVector3 rotationPt, double angleRad)
        {
            IVector3 ptXYZ = new Vector3(this);
            IVector3 ptRot = ptXYZ.RotateZ(rotationPt, angleRad);
            IPointCyl ptCylRot = new PointCyl(ptRot);
            ptCylRot.Col = Col;
            return ptCylRot;
        }
        public double DistanceTo(IPointCyl p2)
        {
            IVector3 p1c = new Vector3(this);
            IVector3 p2c = new Vector3(p2);
            return p1c.DistanceTo(p2c);

        }
        public override List<string> AsDXFString()
        {
            throw new NotImplementedException();
        }

        public IPointCyl Clone()
        {
            return new PointCyl(R, ThetaRad, Z, Col, ID);
        }


    }
}

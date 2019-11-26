using System;

namespace GeometryLib
{

    public class Rotation : Matrix3x3
    {

        public Rotation AboutZ(double radians)
        {
            var rz = new Rotation();
            rz.Matrix[0, 0] = Math.Cos(radians);
            rz.Matrix[0, 1] = -1 * Math.Sin(radians);
            rz.Matrix[0, 2] = 0;

            rz.Matrix[1, 0] = Math.Sin(radians);
            rz.Matrix[1, 1] = Math.Cos(radians);
            rz.Matrix[1, 2] = 0;

            rz.Matrix[2, 0] = 0;
            rz.Matrix[2, 1] = 0;
            rz.Matrix[2, 2] = 1;
            return rz;
        }
        public Rotation AboutY(double radians)
        {
            var ry = new Rotation();
            ry.Matrix[0, 0] = Math.Cos(radians);
            ry.Matrix[0, 1] = 0;
            ry.Matrix[0, 2] = Math.Sin(radians);

            ry.Matrix[1, 0] = 0;
            ry.Matrix[1, 1] = 1;
            ry.Matrix[1, 2] = 0;

            ry.Matrix[2, 0] = -1 * Math.Sin(radians);
            ry.Matrix[2, 1] = 0;
            ry.Matrix[2, 2] = Math.Cos(radians);
            return ry;
        }

        public Rotation AboutX(double radians)
        {
            var rx = new Rotation();
            rx.Matrix[0, 0] = 1;
            rx.Matrix[0, 1] = 0;
            rx.Matrix[0, 2] = 0;

            rx.Matrix[1, 0] = 0;
            rx.Matrix[1, 1] = Math.Cos(radians);
            rx.Matrix[1, 2] = -1 * Math.Sin(radians);

            rx.Matrix[2, 0] = 0;
            rx.Matrix[2, 1] = Math.Sin(radians);
            rx.Matrix[2, 2] = Math.Cos(radians);
            return rx;
        }
    }
}

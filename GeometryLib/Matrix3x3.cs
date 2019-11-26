using IGeometryLib;
namespace GeometryLib
{
    public class Matrix3x3
    {
        public double[,] Matrix;
        public static Matrix3x3 Identity()
        {
            Matrix3x3 m = new Matrix3x3();
            m.Matrix[0, 0] = 1;
            m.Matrix[0, 1] = 0;
            m.Matrix[0, 2] = 0;
            m.Matrix[1, 0] = 0;
            m.Matrix[1, 1] = 1;
            m.Matrix[1, 2] = 0;
            m.Matrix[2, 0] = 0;
            m.Matrix[2, 1] = 0;
            m.Matrix[2, 2] = 1;
            return m;
        }
        public static Matrix3x3 operator *(Matrix3x3 m, double scalar)
        {
            Matrix3x3 mOut = new Matrix3x3();
            mOut.Matrix[0, 0] = m.Matrix[0, 0] * scalar;
            mOut.Matrix[0, 1] = m.Matrix[0, 1] * scalar;
            mOut.Matrix[0, 2] = m.Matrix[0, 2] * scalar;

            mOut.Matrix[1, 0] = m.Matrix[1, 0] * scalar;
            mOut.Matrix[1, 1] = m.Matrix[1, 1] * scalar;
            mOut.Matrix[1, 2] = m.Matrix[1, 2] * scalar;

            mOut.Matrix[2, 0] = m.Matrix[2, 0] * scalar;
            mOut.Matrix[2, 1] = m.Matrix[2, 1] * scalar;
            mOut.Matrix[2, 2] = m.Matrix[2, 2] * scalar;

            return mOut;
        }
        public static Matrix3x3 operator *(double scalar, Matrix3x3 m)
        {
            Matrix3x3 mOut = new Matrix3x3();
            mOut.Matrix[0, 0] = m.Matrix[0, 0] * scalar;
            mOut.Matrix[0, 1] = m.Matrix[0, 1] * scalar;
            mOut.Matrix[0, 2] = m.Matrix[0, 2] * scalar;

            mOut.Matrix[1, 0] = m.Matrix[1, 0] * scalar;
            mOut.Matrix[1, 1] = m.Matrix[1, 1] * scalar;
            mOut.Matrix[1, 2] = m.Matrix[1, 2] * scalar;

            mOut.Matrix[2, 0] = m.Matrix[2, 0] * scalar;
            mOut.Matrix[2, 1] = m.Matrix[2, 1] * scalar;
            mOut.Matrix[2, 2] = m.Matrix[2, 2] * scalar;

            return mOut;
        }
        public static IVector3 operator *(Matrix3x3 m, IVector3 v)
        {
            var vOut = new Vector3();

            vOut.X = m.Matrix[0, 0] * v.X + m.Matrix[0, 1] * v.Y + m.Matrix[0, 2] * v.Z;
            vOut.Y = m.Matrix[1, 0] * v.X + m.Matrix[1, 1] * v.Y + m.Matrix[1, 2] * v.Z;
            vOut.Z = m.Matrix[2, 0] * v.X + m.Matrix[2, 1] * v.Y + m.Matrix[2, 2] * v.Z;

            return vOut;
        }
        public static Matrix3x3 operator *(Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 mOut = new Matrix3x3();

            mOut.Matrix[0, 0] = m1.Matrix[0, 0] * m2.Matrix[0, 0] + m1.Matrix[0, 1] * m2.Matrix[1, 0] + m1.Matrix[0, 2] * m2.Matrix[2, 0];
            mOut.Matrix[0, 1] = m1.Matrix[0, 0] * m2.Matrix[0, 1] + m1.Matrix[0, 1] * m2.Matrix[1, 1] + m1.Matrix[0, 2] * m2.Matrix[2, 1];
            mOut.Matrix[0, 2] = m1.Matrix[0, 0] * m2.Matrix[0, 2] + m1.Matrix[0, 2] * m2.Matrix[1, 2] + m1.Matrix[0, 2] * m2.Matrix[2, 2];

            mOut.Matrix[1, 0] = m1.Matrix[1, 0] * m2.Matrix[0, 0] + m1.Matrix[1, 1] * m2.Matrix[1, 0] + m1.Matrix[1, 2] * m2.Matrix[2, 0];
            mOut.Matrix[1, 1] = m1.Matrix[1, 0] * m2.Matrix[0, 1] + m1.Matrix[1, 1] * m2.Matrix[1, 1] + m1.Matrix[1, 2] * m2.Matrix[2, 1];
            mOut.Matrix[1, 2] = m1.Matrix[1, 0] * m2.Matrix[0, 2] + m1.Matrix[1, 2] * m2.Matrix[1, 2] + m1.Matrix[1, 2] * m2.Matrix[2, 2];

            mOut.Matrix[2, 0] = m1.Matrix[2, 0] * m2.Matrix[0, 0] + m1.Matrix[2, 1] * m2.Matrix[1, 0] + m1.Matrix[2, 2] * m2.Matrix[2, 0];
            mOut.Matrix[2, 1] = m1.Matrix[2, 0] * m2.Matrix[0, 1] + m1.Matrix[2, 1] * m2.Matrix[1, 1] + m1.Matrix[2, 2] * m2.Matrix[2, 1];
            mOut.Matrix[2, 2] = m1.Matrix[2, 0] * m2.Matrix[0, 2] + m1.Matrix[2, 2] * m2.Matrix[1, 2] + m1.Matrix[2, 2] * m2.Matrix[2, 2];

            return mOut;
        }
        public double Determinant()
        {
            double det =
                Matrix[0, 0] * Matrix[1, 1] * Matrix[2, 2] +
                Matrix[0, 1] * Matrix[1, 2] * Matrix[2, 0] +
                Matrix[0, 2] * Matrix[1, 0] * Matrix[2, 1] -
                Matrix[2, 0] * Matrix[1, 1] * Matrix[0, 2] -
                Matrix[2, 1] * Matrix[1, 2] * Matrix[0, 0] -
                Matrix[2, 2] * Matrix[1, 0] * Matrix[0, 1];
            return det;
        }
        public Matrix3x3(double[,] matIn)
        {
            Matrix = new double[3, 3];
            Matrix = matIn;
        }
        public Matrix3x3()
        {
            Matrix = new double[3, 3];
            Matrix[0, 0] = 0;
            Matrix[0, 1] = 0;
            Matrix[0, 2] = 0;
            Matrix[1, 0] = 0;
            Matrix[1, 1] = 0;
            Matrix[1, 2] = 0;
            Matrix[2, 0] = 0;
            Matrix[2, 1] = 0;
            Matrix[2, 2] = 0;
        }
        public Matrix3x3(double r0c0, double r0c1, double r0c2, double r1c0, double r1c1, double r1c2, double r2c0, double r2c1, double r2c2)

        {
            Matrix = new double[3, 3];
            Matrix[0, 0] = r0c0;
            Matrix[0, 1] = r0c1;
            Matrix[0, 2] = r0c2;
            Matrix[1, 0] = r1c0;
            Matrix[1, 1] = r1c1;
            Matrix[1, 2] = r1c2;
            Matrix[2, 0] = r2c0;
            Matrix[2, 1] = r2c1;
            Matrix[2, 2] = r2c2;
        }
    }
}

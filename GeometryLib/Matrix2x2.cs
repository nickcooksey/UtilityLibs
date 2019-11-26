namespace GeometryLib
{

    public class Matrix2x2
    {
        public double[,] Matrix;

        public Matrix2x2()
        {
            Matrix = new double[2, 2];
            Matrix[0, 0] = 0;
            Matrix[0, 1] = 0;
            Matrix[1, 0] = 0;
            Matrix[1, 1] = 0;
        }
        public Matrix2x2(double r0c0, double r0c1, double r1c0, double r1c1)
        {
            Matrix = new double[2, 2];
            Matrix[0, 0] = r0c0;
            Matrix[0, 1] = r0c1;
            Matrix[1, 0] = r1c0;
            Matrix[1, 1] = r1c1;
        }
        public static Matrix2x2 operator *(double scalar, Matrix2x2 m)
        {
            m.Matrix[0, 0] = m.Matrix[0, 0] * scalar;
            m.Matrix[0, 1] = m.Matrix[0, 1] * scalar;
            m.Matrix[1, 0] = m.Matrix[1, 0] * scalar;
            m.Matrix[1, 1] = m.Matrix[1, 1] * scalar;
            return m;
        }
        public static Matrix2x2 operator *(Matrix2x2 m, double scalar)
        {
            m.Matrix[0, 0] = m.Matrix[0, 0] * scalar;
            m.Matrix[0, 1] = m.Matrix[0, 1] * scalar;
            m.Matrix[1, 0] = m.Matrix[1, 0] * scalar;
            m.Matrix[1, 1] = m.Matrix[1, 1] * scalar;
            return m;
        }
        public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
        {
            Matrix2x2 mOut = new Matrix2x2();
            mOut.Matrix[0, 0] = m1.Matrix[0, 0] * m2.Matrix[0, 0] + m1.Matrix[0, 1] * m2.Matrix[1, 0];
            mOut.Matrix[0, 1] = m1.Matrix[0, 1] * m2.Matrix[0, 1] + m1.Matrix[0, 1] * m2.Matrix[1, 1];
            mOut.Matrix[1, 0] = m1.Matrix[1, 0] * m2.Matrix[0, 0] + m1.Matrix[1, 1] * m2.Matrix[1, 0];
            mOut.Matrix[1, 1] = m1.Matrix[1, 1] * m2.Matrix[0, 1] + m1.Matrix[1, 1] * m2.Matrix[1, 1];
            return mOut;
        }
        public double Determinant()
        {
            double det = Matrix[0, 0] * Matrix[1, 1] - Matrix[0, 1] * Matrix[1, 0];
            return det;
        }
        public Matrix2x2(double[,] matIn)
        {

            Matrix = new double[2, 2];

            Matrix = matIn;
        }

    }
}

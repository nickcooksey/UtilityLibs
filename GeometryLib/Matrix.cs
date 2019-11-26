using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib
{
    public class Matrix<T>
    {
        int rowCount;
        int colCount;
        protected T[,] matrix;
        public T[,] Mat
        {
            get
            {
                return matrix;
            }
            set
            {
                loadMatrix(value);
            }
        }
        public int RowCount { get { return rowCount; } }
        public int ColCount { get { return colCount; } }

        void loadMatrix(T[,] matIn)
        {

            int dim0 = matIn.GetLength(0);
            int dim1 = matIn.GetLength(1);
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    matrix[i, j] = default(T);
                }
            }
            for (int i = 0; i < Math.Min(RowCount, dim0); i++)
            {
                for (int j = 0; j < Math.Min(ColCount, dim1); j++)
                {
                    matrix[i, j] = matIn[i, j];
                }
            }
        }
        public Matrix(int rows, int cols)
        {
            rowCount = rows;
            colCount = cols;
            matrix = new T[rowCount, colCount];

        }
    }
}

using System;
using System.Collections.Generic;

namespace FileIOLib
{

    public class CSVFileParser
    {
        private List<string> dataList;
        public CSVFileParser()
        {
            dataList = new List<string>();
        }
        public string[,] ParseFile(string fileName)
        {
            try
            {
                dataList = FileIO.ReadTextFile(fileName);
                string[] firstLine = dataList[0].Split(',');
                int rowCount = dataList.Count;
                int colCount = firstLine.Length;

                var outputArr = new string[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    string[] words = dataList[i].Split(',');
                    int TempColCount = Math.Min(words.Length, colCount);
                    for (int j = 0; j < TempColCount; j++)
                    {
                        outputArr[i, j] = words[j];
                    }
                }
                return outputArr;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public double[,] ParseToDoubleArr(string fileName, int headerRowCount)
        {
            try
            {
                List<string> dataList = FileIO.ReadTextFile(fileName);

                int columns = 0;
                int rows = 0;
                double[,] data = new double[rows, columns]; ;

                if (dataList.Count > headerRowCount)
                {
                    string[] words = dataList[headerRowCount].Split(',');
                    columns = words.Length;
                    rows = dataList.Count - headerRowCount;
                    double result = 0;
                    data = new double[rows, columns];
                    int row = 0;
                    int col = 0;

                    for (int i = headerRowCount; i < dataList.Count; i++)
                    {
                        string line = dataList[i];
                        words = line.Split(',');
                        col = 0;
                        foreach (string word in words)
                        {
                            result = 0;
                            if (double.TryParse(word, out result))
                            {
                                data[row, col] = result;
                            }
                            col++;
                        }
                        row++;
                    }
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

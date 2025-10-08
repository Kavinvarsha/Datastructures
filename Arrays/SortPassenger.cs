using System;
using System.Linq;

namespace MyCSVApp
{
    class SortPassenger
    {
        // Sort by Age (column index 5)
        public static string[,] SortByAge(string[,] rectArray, int rows, int cols)
        {
            var sorted = Enumerable.Range(0, rows)
                .Select(i => Enumerable.Range(0, cols).Select(j => rectArray[i, j]).ToArray())
                .OrderBy(r =>
                {
                    double val;
                    return double.TryParse(r[5], out val) ? val : double.MaxValue;
                })
                .ToArray();

            return To2DArray(sorted, cols);
        }

        // Sort by Fare (column index 9)
        public static string[,] SortByFare(string[,] rectArray, int rows, int cols)
        {
            var sorted = Enumerable.Range(0, rows)
                .Select(i => Enumerable.Range(0, cols).Select(j => rectArray[i, j]).ToArray())
                .OrderBy(r =>
                {
                    double val;
                    return double.TryParse(r[9], out val) ? val : double.MaxValue;
                })
                .ToArray();

            return To2DArray(sorted, cols);
        }

        // Sort by Survived status (column index 1)
        public static string[,] SortBySurvival(string[,] rectArray, int rows, int cols)
        {
            var sorted = Enumerable.Range(0, rows)
                .Select(i => Enumerable.Range(0, cols).Select(j => rectArray[i, j]).ToArray())
                .OrderBy(r => r[1]) // 0 = did not survive, 1 = survived
                .ToArray();

            return To2DArray(sorted, cols);
        }

        // Convert jagged array back to 2D
        private static string[,] To2DArray(string[][] data, int cols)
        {
            int rows = data.Length;
            string[,] result = new string[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = data[i][j];
            return result;
        }

        // Helper to display sorted array
        public static void Display(string[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write(array[i, j] + "\t");
                Console.WriteLine();
            }
        }
    }
}

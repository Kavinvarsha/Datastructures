using System;

namespace MyCSVApp
{
    class SearchPassenger
    {
        // Linear search by PassengerId in 2D rectangular array
        public static int LinearSearch(string[,] array, int rows, int colIndex, string searchValue)
        {
            for (int i = 0; i < rows; i++)
            {
                if (array[i, colIndex] == searchValue)
                    return i; // return row index
            }
            return -1; // not found
        }

        // Binary search by PassengerId with reference to 2D array
        public static int BinarySearch(string[] passengerIdArray, string[,] rectArray, string searchValue)
        {
            // Create an array of indexes to preserve original positions
            int[] indexes = new int[passengerIdArray.Length];
            for (int i = 0; i < indexes.Length; i++)
                indexes[i] = i;

            // Sort indexes based on PassengerId values
            Array.Sort(passengerIdArray, indexes, StringComparer.Ordinal);

            int left = 0;
            int right = passengerIdArray.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int cmp = string.Compare(passengerIdArray[mid], searchValue, StringComparison.Ordinal);

                if (cmp == 0)
                {
                    int originalRow = indexes[mid]; // get original row index in 2D array
                    Console.WriteLine($"PassengerId found at row {originalRow + 1}");
                    Console.WriteLine($"Name: {rectArray[originalRow, 3]}, Age: {rectArray[originalRow, 5]}");
                    return originalRow;
                }
                else if (cmp < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1; // not found
        }
    }
}

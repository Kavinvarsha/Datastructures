using System;
using System.IO;
using System.Linq;

namespace MyCSVApp
{
    class Program
    {
        static void Main()
        {
            string filePath = "tested.csv";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ CSV file not found!");
                return;
            }

            // ===================== READ CSV =====================
            string[] lines = File.ReadAllLines(filePath);
            string[] dataLines = lines.Skip(1).ToArray(); // Skip header

            // ===================== CREATE ARRAYS =====================
            string[] oneDArray = dataLines;
            int rows = dataLines.Length;
            int cols = lines[0].Split('\t').Length;

            // 2D Rectangular Array
            string[,] rectArray = new string[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                string[] columns = dataLines[i].Split('\t');
                for (int j = 0; j < cols; j++)
                    rectArray[i, j] = columns[j];
            }

            // Jagged Array
            string[][] jaggedArray = new string[rows][];
            for (int i = 0; i < rows; i++)
                jaggedArray[i] = dataLines[i].Split('\t');

            // PassengerId Array for Binary Search
            string[] passengerIdArray = dataLines.Select(line => line.Split('\t')[0]).ToArray();

            Console.WriteLine("✅ CSV loaded successfully!");
            Console.WriteLine($"Total Rows: {rows}, Columns: {cols}\n");

            // ===================== MENU =====================
            while (true)
            {
                Console.WriteLine("=== MAIN MENU ===");
                Console.WriteLine("1. Display 1D Array");
                Console.WriteLine("2. Display 2D Array");
                Console.WriteLine("3. Display Jagged Array");
                Console.WriteLine("4. Linear Search by PassengerId");
                Console.WriteLine("5. Binary Search by PassengerId");
                Console.WriteLine("6. Sorting Menu");
                Console.WriteLine("7. Frequency Analysis (Survival by Class & Gender)");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "8") break;

                switch (choice)
                {
                    // ===== Display 1D Array =====
                    case "1":
                        Console.WriteLine("=== 1D Array ===");
                        foreach (var line in oneDArray)
                            Console.WriteLine(line);
                        Console.WriteLine("✅ Display complete!\n");
                        break;

                    // ===== Display 2D Array =====
                    case "2":
                        Console.WriteLine("=== 2D Array ===");
                        Display2D(rectArray);
                        Console.WriteLine("✅ Display complete!\n");
                        break;

                    // ===== Display Jagged Array =====
                    case "3":
                        Console.WriteLine("=== Jagged Array ===");
                        foreach (var row in jaggedArray)
                            Console.WriteLine(string.Join("\t", row));
                        Console.WriteLine("✅ Display complete!\n");
                        break;

                    // ===== Linear Search =====
                    case "4":
                        Console.Write("Enter PassengerId to search: ");
                        string idLinear = Console.ReadLine();
                        int rowIndex = SearchPassenger.LinearSearch(rectArray, rows, 0, idLinear);
                        if (rowIndex != -1)
                            Console.WriteLine($"✅ Found at row {rowIndex + 1}: Name={rectArray[rowIndex, 3]}, Age={rectArray[rowIndex, 5]}");
                        else
                            Console.WriteLine("❌ PassengerId not found!");
                        Console.WriteLine();
                        break;

                    // ===== Binary Search =====
                    case "5":
                        Console.Write("Enter PassengerId to search: ");
                        string idBinary = Console.ReadLine();
                        int result = SearchPassenger.BinarySearch(passengerIdArray, rectArray, idBinary);
                        if (result == -1)
                            Console.WriteLine("❌ PassengerId not found!");
                        Console.WriteLine();
                        break;

                    // ===== Sorting Menu =====
                    case "6":
                        SortingMenu(rectArray, rows, cols);
                        break;

                    // ===== Frequency Analysis =====
                    case "7":
                        FrequencyAnalysis.Analyze(rectArray, rows, cols);
                        break;

                    default:
                        Console.WriteLine("⚠️ Invalid choice! Try again.\n");
                        break;
                }
            }
        }

        // ===== Helper to Display 2D Array =====
        static void Display2D(string[,] array)
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

        // ===== Sorting Submenu =====
        static void SortingMenu(string[,] rectArray, int rows, int cols)
        {
            while (true)
            {
                Console.WriteLine("=== SORTING MENU ===");
                Console.WriteLine("1. Sort by Age");
                Console.WriteLine("2. Sort by Fare");
                Console.WriteLine("3. Sort by Survival Status");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Enter choice: ");
                string sortChoice = Console.ReadLine();
                Console.WriteLine();

                if (sortChoice == "4") break;

                string[,] sorted = null;

                switch (sortChoice)
                {
                    case "1":
                        sorted = SortPassenger.SortByAge(rectArray, rows, cols);
                        Console.WriteLine("✅ Sorted by Age:");
                        SortPassenger.Display(sorted);
                        break;

                    case "2":
                        sorted = SortPassenger.SortByFare(rectArray, rows, cols);
                        Console.WriteLine("✅ Sorted by Fare:");
                        SortPassenger.Display(sorted);
                        break;

                    case "3":
                        sorted = SortPassenger.SortBySurvival(rectArray, rows, cols);
                        Console.WriteLine("✅ Sorted by Survival Status:");
                        SortPassenger.Display(sorted);
                        break;

                    default:
                        Console.WriteLine("⚠️ Invalid choice!\n");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}

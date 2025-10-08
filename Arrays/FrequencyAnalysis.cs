using System;
using System.Collections.Generic;

namespace MyCSVApp
{
    class FrequencyAnalysis
    {
        // Perform frequency analysis on survival based on class and gender
        public static void Analyze(string[,] rectArray, int rows, int cols)
        {
            // Column indexes (based on Titanic dataset format)
            // Survived = 1, Pclass = 2, Sex = 4
            int survivedCol = 1;
            int classCol = 2;
            int genderCol = 4;

            // Dictionary for counting
            // Key format: "Class-Gender"
            var survivalStats = new Dictionary<string, (int total, int survived)>();

            for (int i = 0; i < rows; i++)
            {
                string pClass = rectArray[i, classCol];
                string gender = rectArray[i, genderCol];
                string survived = rectArray[i, survivedCol];

                string key = $"{pClass}-{gender}";

                if (!survivalStats.ContainsKey(key))
                    survivalStats[key] = (0, 0);

                var stats = survivalStats[key];
                stats.total++;

                if (survived == "1")
                    stats.survived++;

                survivalStats[key] = stats;
            }

            // Display results
            Console.WriteLine("=== FREQUENCY ANALYSIS: Survival Rates by Class & Gender ===");
            Console.WriteLine("Class\tGender\tTotal\tSurvived\tSurvival Rate (%)");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var kvp in survivalStats)
            {
                string[] parts = kvp.Key.Split('-');
                string pClass = parts[0];
                string gender = parts[1];

                double rate = (kvp.Value.total > 0)
                    ? (kvp.Value.survived * 100.0 / kvp.Value.total)
                    : 0;

                Console.WriteLine($"{pClass}\t{gender}\t{kvp.Value.total}\t{kvp.Value.survived}\t\t{rate:F2}");
            }

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("✅ Frequency analysis completed!\n");
        }
    }
}

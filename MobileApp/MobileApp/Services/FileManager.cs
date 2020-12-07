using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace MobileApp.Services
{
    public class FileManager
    {
        // Data write to .csv with a separator ";"
        public static void WriteToCSV(double[,] dbCargo)
        {
            string stCargo = "t;PV;SV;MV;E;PartP;PartI;PartD;\r\n";
            string WritePath = Path.Combine(FileSystem.AppDataDirectory,"PID.csv");

            int n = dbCargo.GetUpperBound(0) + 1;
            int m = dbCargo.Length / n;
            for (int l = 0; l < m; l++)
            {
                stCargo += $"{l};";
                for (int k = 0; k < n; k++)
                {
                    stCargo += $"{dbCargo[k, l]};";
                }
                stCargo += "\r\n";
            }

            File.WriteAllText(WritePath, stCargo);
            //using (var w = new StreamWriter(WritePath))
            //{
            //    w.WriteLine(stCargo);
            //    w.Flush();
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClusterAnalysis_KMeans.Models
{
    public class Writer
    {
        public string IterNumberTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\Iteration.txt";
        public string Cluster1PointsPath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\Cluster1Points.txt";
        public string Cluster2PointsPath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\Cluster2Points.txt";
        public string Cluster3PointsPath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\Cluster3Points.txt";
        public string ResultsPath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\Results.txt";

        public void WriteToReportFile(string msg, bool append = true)
        {
            using var sw = new StreamWriter(ResultsPath, append, Encoding.Default);
            sw.Write(msg);
        }

        public void WriteToFileIter(int n)
        {
            using var sw = new StreamWriter(IterNumberTextFilePath, false, Encoding.Default);
            sw.Write(n);
        }

        public void WriteToFileCluster(int i, List<Point> points, Centroid centroid)
        {
            string p = "";
            switch (i)
            {
                case 1:
                    p = Cluster1PointsPath;
                    break;
                case 2:
                    p = Cluster2PointsPath;
                    break;
                case 3:
                    p = Cluster3PointsPath;
                    break;
            }
            using var sw = new StreamWriter(p, false, Encoding.Default);
            points.ForEach(x => sw.Write(x.X + " ")); sw.Write(centroid.X);
            sw.Write('\n');
            points.ForEach(x => sw.Write(x.Y + " ")); sw.Write(centroid.Y);
        }
    }
}

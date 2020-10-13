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
        public string PointsXTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\PointsX.txt";
        public string PointsYTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\PointsY.txt";
        public string CentroidsXTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\CentroidsX.txt";
        public string CentroidsYTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\CentroidsY.txt";

        void WriteToFileIter(int n)
        {
            using var sw = new StreamWriter(IterNumberTextFilePath, false, Encoding.Default);
            sw.Write(n);
        }
        void WriteToFileCentroids(List<Centroid> centroids)
        {
            WriteToFile(CentroidsXTextFilePath, centroids.Select(x => x.X).ToList());
            WriteToFile(CentroidsYTextFilePath, centroids.Select(x => x.Y).ToList());
        }
        void WriteToFilePoints(List<Point> points)
        {
            WriteToFile(PointsXTextFilePath, points.Select(x => x.X).ToList());
            WriteToFile(PointsYTextFilePath, points.Select(x => x.Y).ToList());
        }
        void WriteToFile(string path, List<double> v)
        {
            using var sw = new StreamWriter(path, false, Encoding.Default);
            v.ForEach(x => sw.Write(x + " "));
        }
    }
}

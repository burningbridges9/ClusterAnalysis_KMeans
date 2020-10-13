using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClusterAnalysis_KMeans.Models
{
    public class KMeansAlgo
    {
        public string IterNumberTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\Iteration.txt";
        public string PointsXTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\PointsX.txt";
        public string PointsYTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\PointsY.txt";
        public string CentroidsXTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\CentroidsX.txt";
        public string CentroidsYTextFilePath { get; set; } = @"C:\Users\Rustam\Documents\Visual Studio 2019\ClusterAnalysis_KMeans\ClusterAnalysis_KMeans\Data\CentroidsY.txt";
        public List<Point> Points { get; } = new List<Point>()
        {
            new Point(9,3),
            new Point(2,3),
            new Point(6,3),
            new Point(9,1),
            new Point(9,1),
            new Point(7,2),
            new Point(7,3),
            new Point(6,9),
            new Point(7,1),
            new Point(0,7),
            new Point(0,8),
            new Point(7,5),
            new Point(6,10),
            new Point(7,9),
        };

        public void Compute()
        {
            List<Centroid> centroids = GetInitialCentroids();
            List<Cluster> clustersPrev = null;
            List<Cluster> clustersCur = null;
            bool firstIter = true;

            do
            {
                int j = firstIter ? 3 : 0;
                for (int i = 0 + j; i < Points.Count; i++)
                {
                    List<double> distances = new List<double>();
                    centroids.ForEach(x => distances.Add(x.Distance(Points[i])));

                    var minDistance = distances.Min();
                    var minDistanceIndex = distances.IndexOf(minDistance);
                    var minDistanceCentroid = centroids[minDistanceIndex];

                    centroids[minDistanceIndex] = new Centroid(Points[i], minDistanceCentroid);
                    Points[i].Cluster = (Cluster)minDistanceIndex + 1;
                }
                firstIter = false;
            } while (ExitCondition(clustersPrev, clustersCur));
        }

        private List<Centroid> GetInitialCentroids()
        {
            Points[0].Cluster = Cluster.K1;
            Points[1].Cluster = Cluster.K2;
            Points[2].Cluster = Cluster.K3;
            return new List<Centroid>()
            {
                new Centroid(Points[0]),
                new Centroid(Points[1]),
                new Centroid(Points[2]),
            };
        }

        private bool ExitCondition(List<Cluster> cPrev, List<Cluster> cCur)
        {
            if (cPrev == null) // first iter
                return false;
            else
            {
                for (int i = 0; i < cPrev.Count; i++)
                {
                    if (cPrev[i] != cCur[i])
                        return false;
                }
                return true;
            }
        }

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
        void WriteToFilePoints()
        {
            WriteToFile(PointsXTextFilePath, Points.Select(x => x.X).ToList());
            WriteToFile(PointsYTextFilePath, Points.Select(x => x.Y).ToList());
        }
        void WriteToFile(string path, List<double> v)
        {
            using var sw = new StreamWriter(path, false, Encoding.Default);
            v.ForEach(x => sw.Write(x + " "));
        }
    }
}

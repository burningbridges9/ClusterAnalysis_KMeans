using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClusterAnalysis_KMeans.Models
{
    public class KMeansAlgo
    {
        public Writer Writer { get; set; } = new Writer();

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

        public void Compute(List<Centroid> centroids)
        {
            List<Cluster> clustersPrev = null;
            List<Cluster> clustersCur = null;
            bool firstIter = true;
            int iterCount = 0;
            do
            {
                iterCount++;
                Writer.WriteToFileIter(iterCount);
                if (!firstIter)
                {
                    clustersPrev = new List<Cluster>(clustersCur);
                }
                for (int i = 0; i < Points.Count; i++)
                {
                    if (firstIter && Points[i].Cluster != Cluster.Unknown)
                        continue;

                    List<double> distances = new List<double>();
                    centroids.ForEach(x => distances.Add(x.Distance(Points[i])));

                    var minDistance = distances.Min();
                    var minDistanceIndex = distances.IndexOf(minDistance);
                    var minDistanceCentroid = centroids[minDistanceIndex];

                    centroids[minDistanceIndex] = new Centroid(Points[i], minDistanceCentroid);
                    Points[i].Cluster = (Cluster)minDistanceIndex + 1;
                }
                clustersCur = new List<Cluster>(Points.Select(x => x.Cluster));
                firstIter = false;

                Writer.WriteToFileCluster(1, Points.Where(p => p.Cluster == Cluster.K1).ToList(), centroids[0]);
                Writer.WriteToFileCluster(2, Points.Where(p => p.Cluster == Cluster.K2).ToList(), centroids[1]);
                Writer.WriteToFileCluster(3, Points.Where(p => p.Cluster == Cluster.K3).ToList(), centroids[2]);
            } while (!ExitCondition(clustersPrev, clustersCur));
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

        private List<Centroid> GetInitialCentroids(int i, int j, int k)
        {
            Points[i].Cluster = Cluster.K1;
            Points[j].Cluster = Cluster.K2;
            Points[k].Cluster = Cluster.K3;
            return new List<Centroid>()
            {
                new Centroid(Points[i]),
                new Centroid(Points[j]),
                new Centroid(Points[k]),
            };
        }

        private List<Centroid> GetInitialCentroids(Point p1, Point p2, Point p3)
        {
            return new List<Centroid>()
            {
                new Centroid(p1),
                new Centroid(p2),
                new Centroid(p3),
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

        public void Start(int startParam, int i = 0, int j = 0, int k = 0)
        {
            List<Centroid> centroids;
            switch (startParam)
            {
                case 1:
                    centroids = GetInitialCentroids();
                    Compute(centroids);
                    break;
                case 2:
                    centroids = GetInitialCentroids(i, j, k);
                    Compute(centroids);
                    break;
                case 3:
                    centroids = GetInitialCentroids(new Point(2, 2), new Point(5, 5), new Point(7, 7));
                    Compute(centroids);
                    break;
            }
        }

    }
}

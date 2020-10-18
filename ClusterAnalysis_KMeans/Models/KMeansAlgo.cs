using Kmeans_disp;
using System.Collections.Generic;
using System.Linq;

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
            Plotter plotter = new Plotter();
            List<Cluster> clustersPrev = null;
            List<Cluster> clustersCur = null;
            bool firstIter = true;
            int iterCount = 0;
            Writer.WriteToReportFile("", false);
            do
            {
                iterCount++;
                Writer.WriteToFileIter(iterCount);
                if (!firstIter)
                {
                    clustersPrev = new List<Cluster>(clustersCur);
                }
                string msg = $"Iteration {iterCount}: \n";
                Writer.WriteToReportFile(msg);
                for (int i = 0; i < Points.Count; i++)
                {
                    Writer.WriteToReportFile($"Customer {i + 1}\n");
                    List<double> distances = new List<double>();

                    var centroidsMsg = "";
                    for (int j = 0; j < centroids.Count; j++)
                    {
                        var distance = centroids[j].Distance(Points[i]);
                        centroidsMsg += $"Centroid {j + 1} = {centroids[j].ToString()}, Distance = {distance}\n";
                        distances.Add(distance);
                    }

                    var minDistance = distances.Min();
                    var minDistanceIndex = distances.IndexOf(minDistance);
                    var minDistanceCentroid = centroids[minDistanceIndex];

                    Writer.WriteToReportFile(centroidsMsg);
                    Writer.WriteToReportFile($"Min distance = {minDistance} at centroid {minDistanceIndex} = {minDistanceCentroid.ToString()}\n");

                    centroids[minDistanceIndex] = new Centroid(Points[i], minDistanceCentroid);
                    Points[i].Cluster = (Cluster)minDistanceIndex + 1;

                    Writer.WriteToReportFile($"New centroid = {centroids[minDistanceIndex].ToString()}\n\n");
                    
                }
                clustersCur = new List<Cluster>(Points.Select(x => x.Cluster));
                firstIter = false;

                var iterResult = $"\nIteration results:\n" +
                    $"Cluster 1:\n" +
                    $"Points: {string.Join("; ", Points.Where(x => x.Cluster == Cluster.K1).Select(x => x.ToString()))}\n" +
                    $"Centroid: {centroids[0]}\n" +
                    $"Cluster 2:\n" +
                    $"Points: {string.Join(';', Points.Where(x => x.Cluster == Cluster.K2).Select(x => x.ToString()))}\n" +
                    $"Centroid: {centroids[1]}\n" +
                    $"Cluster 3:\n" +
                    $"Points: {string.Join(';', Points.Where(x => x.Cluster == Cluster.K3).Select(x => x.ToString()))}\n" +
                    $"Centroid: {centroids[2]}\n\n\n"
                    ;
                Writer.WriteToReportFile(iterResult);

                Writer.WriteToFileCluster(1, Points.Where(p => p.Cluster == Cluster.K1).ToList(), centroids[0]);
                Writer.WriteToFileCluster(2, Points.Where(p => p.Cluster == Cluster.K2).ToList(), centroids[1]);
                Writer.WriteToFileCluster(3, Points.Where(p => p.Cluster == Cluster.K3).ToList(), centroids[2]);
                plotter.Kmeans_disp();
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
                    centroids = GetInitialCentroids(new Point(2, 3), new Point(7, 9), new Point(7, 3));// GetInitialCentroids(i, j, k);
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

using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterAnalysis_KMeans.Models
{
    public class Point : AbstractPoint
    {
        public Cluster Cluster { get; set; } = Cluster.Unknown;
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}

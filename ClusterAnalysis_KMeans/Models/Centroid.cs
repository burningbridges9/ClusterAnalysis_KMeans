using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterAnalysis_KMeans.Models
{
    public class Centroid : AbstractPoint, IDistance
    {
        public Centroid()
        {

        }

        public Centroid(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }

        public Centroid(Point p, Centroid c)
        {
            this.X = (p.X + c.X) / 2.0;
            this.Y = (p.Y + c.Y) / 2.0;
        }

        public double Distance(Point p)
        {
            return Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2));
        }
    }
}

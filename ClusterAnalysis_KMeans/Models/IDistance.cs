using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterAnalysis_KMeans.Models
{
    public interface IDistance
    {
        double Distance(Point p);
    }
}

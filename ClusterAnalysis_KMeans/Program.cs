using ClusterAnalysis_KMeans.Models;
using System;

namespace ClusterAnalysis_KMeans
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            KMeansAlgo algo = new KMeansAlgo();
            algo.Start(1);
        }
    }
}

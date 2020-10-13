using ClusterAnalysis_KMeans.Models;
using System;

namespace ClusterAnalysis_KMeans
{
    class Program
    {
        static void Main(string[] args)
        {
            KMeansAlgo algo = new KMeansAlgo();
            algo.ComputeFirstVariant();
        }
    }
}

namespace ClusterAnalysis_KMeans.Models
{
    public abstract class AbstractPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString() => $"(X = {X}, Y = {Y})";
    }
}

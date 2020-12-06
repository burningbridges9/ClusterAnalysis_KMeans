using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSeries
{
    public class SeriesBase
    {
        public virtual int[] Arr { get; set; }
        public int N { get => Arr.Length; }
        public double[] Lambdas
        {
            get
            {
                var lambdas = new List<double>();
                for (int i = 1; i < Arr.Length; i++)
                {
                    var l = Math.Abs(Arr[i] - Arr[i - 1]) / Sy;
                    lambdas.Add(l);
                }
                return lambdas.ToArray();
            }
        }
        public double Mean { get => Arr.Average(); }
        public double Sy
        {
            get
            {
                var mean = Arr.Average();
                var diffsSquared = new List<double>();
                Arr.ToList().ForEach(a => diffsSquared.Add(Math.Pow(a - mean, 2)));
                var sy = Math.Sqrt(diffsSquared.Sum() / (N - 1));
                return sy;
            }
        }
        public string LambdasToString()
        {
            var strs = new string[Lambdas.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = Lambdas[i].ToString("N2");
            }

            return string.Join("; ", strs);
        }
        public string ArrToString() => string.Join("; ", Arr);

        public override string ToString() =>
            $"Series - {this.GetType().Name}\n" +
                $"Mean = {this.Mean.ToString("N2")}\n" +
                $"Sy = {this.Sy.ToString("N2")}\n" +
                $"Arr: \n{this.ArrToString()}\n" +
                $"Lambdas: \n{this.LambdasToString()}\n" +
                $"MovingAverage: \n{MovingAverageToString()}\n" +
                $"NormalizedMovingAverage: \n{NormalizedMovingAverageToString()}\n" +
                $"AscAndDescCriterion: \n{AscAndDescCriterionToString()}";

        public readonly int k_0 = 5;
        public double AllCriterionsCheck { get => 1.0 / 3 * (2 * N - 1) - 1.96 * Math.Sqrt((16 * N - 29) / 90.0); }
        public (int maxCriterionLeng, int allCriterions, bool sampleIsRandom) AscAndDescCriterion()
        {
            int maxCriterionLeng = 0;
            int allCriterions = 0;
            var currentCriterionLength = 0;
            SeriesType currentType = SeriesType.None;
            for (int i = 0; i < Arr.Length - 1; i++)
            {
                var next = Arr[i + 1];
                var current = Arr[i];

                if (currentCriterionLength > maxCriterionLeng)
                    maxCriterionLeng = currentCriterionLength;

                if (next > current)
                {
                    if (currentType != SeriesType.Ascending)
                    {
                        allCriterions++;
                        currentCriterionLength = 1;
                    }
                    else
                    {
                        currentCriterionLength++;
                    }
                    currentType = SeriesType.Ascending;
                }
                else
                {
                    if (currentType != SeriesType.Descending)
                    {
                        allCriterions++;
                        currentCriterionLength = 1;
                    }
                    else
                    {
                        currentCriterionLength++;
                    }
                    currentType = SeriesType.Descending;
                }
            }

            bool sampleIsRandom =
                (allCriterions > AllCriterionsCheck) &&
                (maxCriterionLeng <= k_0);

            return (maxCriterionLeng, allCriterions, sampleIsRandom);
        }


        public string AscAndDescCriterionToString() =>
            $"Length of max serie: {AscAndDescCriterion().maxCriterionLeng}\n" +
            $"All series count: {AscAndDescCriterion().allCriterions}\n" +
            $"Sample is random: {AscAndDescCriterion().sampleIsRandom}\n";

        public double[] MovingAverage
        {
            get
            {
                var avgs = new List<double>();
                for (int i = 1; i < Arr.Length - 1; i++)
                {
                    var prev = Arr[i - 1];
                    var cur = Arr[i];
                    var next = Arr[i + 1];
                    var avg = (prev + cur + next) / 3.0;
                    avgs.Add(avg);
                }
                return avgs.ToArray();
            }
        }

        public string MovingAverageToString()
        {
            var strs = new string[MovingAverage.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = MovingAverage[i].ToString("N2");
            }

            return string.Join("; ", strs);
        }


        public double[] NormalizedMovingAverage
        {
            get
            {
                const double c1 = -3.0 / 35.0, c2 = 12.0 / 35.0, c3 = 17.0 / 35.0;
                var avgs = new List<double>();
                for (int i = 2; i < Arr.Length - 2; i++)
                {
                    var prev2 = c1 * Arr[i - 2];
                    var prev1 = c2 * Arr[i - 1];
                    var cur = c3 * Arr[i];
                    var next1 = c2 * Arr[i + 1];
                    var next2 = c1 * Arr[i + 2];
                    var avg = prev2 + prev1 + cur + next1 + next2;
                    avgs.Add(avg);
                }
                return avgs.ToArray();
            }
        }
        public string NormalizedMovingAverageToString()
        {
            var strs = new string[NormalizedMovingAverage.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = NormalizedMovingAverage[i].ToString("N2");
            }

            return string.Join("; ", strs);
        }


        public double DWCreterion
        {
            get
            {
                var y_squared = Arr.Sum(x => x * x);
                var diff_squared = 0.0;
                for (int i = 1; i < N - 1; i++)
                {
                    diff_squared += Math.Pow(Arr[i + 1] - Arr[i], 2);
                }

                double d = diff_squared / y_squared;
                return d;
            }
        }

        public double AutoCorrCoef
        {
            get
            {

            }
        }
    }

    public enum SeriesType
    {
        None,
        Ascending,
        Descending
    };
}

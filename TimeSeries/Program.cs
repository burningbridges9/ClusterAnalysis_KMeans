using System;
using System.Globalization;
using System.Threading;

namespace TimeSeries
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Console.WriteLine("Hello World!");
            SeriesInitial seriesInitial = new SeriesInitial();
            //Console.WriteLine(seriesInitial);
            //seriesInitial.AscAndDescCriterion();
            //var o = seriesInitial.MovingAverage;

            SeriesNormalized seriesNorm = new SeriesNormalized();
            Console.WriteLine(seriesNorm);
            var d = seriesInitial.DWCreterion;
            Console.ReadKey();
        }

        //private static void ShowNormalizedSeries()
        //{
        //    SeriesNormalized seriesNorm = new SeriesNormalized();
        //    Console.WriteLine($"Array:\n {seriesNorm.ArrToString()}");
        //    Console.WriteLine($"Lambdas:\n {seriesNorm.LambdasToString()}");
        //}
    }
}

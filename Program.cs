using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grubbs
{
    class Program
    {



        public static double[] TestData = new double[]{ 1.9   ,20  ,20  ,20  ,20  ,19  ,20  ,19  ,20  ,20  ,19  ,20  ,19  ,19  ,19  ,19  ,19  ,19  ,200  ,18 };
        
        static void Main(string[] args)
        {
            foreach (var ii in Grubbs.GetUnusualData(TestData).ToList())
            {
                Console.WriteLine(ii + 1);
            }
            Console.ReadLine();
        }
    }


    public class Grubbs
    {
        private static double[] GrubbsCon = new double[31] { 0, 0, 0, 1.135, 1.463, 1.672, 1.822, 1.938, 2.032, 2.11, 2.176, 2.234, 2.285, 2.331, 2.371, 2.409, 2.443, 2.475, 2.504, 2.532, 2.557, 2.58, 2.603, 2.624, 2.644, 2.663, 2.745, 2.811, 2.866, 2.914, 2.956 };

        public static int[] GetUnusualData(double[] WillCamData)
        {
            List<int> unusualData = new List<int>();

            List<double> WTestData = WillCamData.ToList();

            for (int i = 0; i < WillCamData.Length; i++)
            {
                double[] TestData = WTestData.ToArray();

                double maxd = WTestData.Max();
                double mind = WTestData.Min();               

                double biaoZhunCha = ArrayStatistics.PopulationStandardDeviation(TestData);
                double mean = ArrayStatistics.Mean(TestData);

                double keyi = maxd - mean > mean - mind ? maxd : mind;
                
                int keyiid = maxd - mean > mean - mind ? WTestData.Count - 1 : 0;
                double gx = Math.Abs((keyi - mean) / biaoZhunCha);

                bool bkeyi = gx > Grubbs.GrubbsCon[TestData.Length];
                if (bkeyi)
                {
                    unusualData.Add(WTestData.IndexOf(keyi));
                    WTestData.RemoveAt(WTestData.IndexOf(keyi));
                }
                else
                {
                    break;
                }
            }
            return unusualData.ToArray();
        }

    }

}

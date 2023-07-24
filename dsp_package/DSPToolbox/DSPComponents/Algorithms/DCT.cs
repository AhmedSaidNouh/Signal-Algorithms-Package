using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        List<float> samples = new List<float>();

        public override void Run()
        {
            double seta = 0;
            double result = 0;
            for (int i=0;i< InputSignal.Samples.Count; i++)
            {
                 result = 0;
                for(int j=0;j< InputSignal.Samples.Count; j++)
                {
                    double s = (((2.00 * j) + 1.00) * i * Math.PI)/(2* InputSignal.Samples.Count);
                    seta = Math.Cos(s);
                    double term = seta * InputSignal.Samples[j];
                    result = result + term;
                }
                if(i==0)
                result = result * Math.Sqrt(1.0/ InputSignal.Samples.Count);
                else
                    result = result * Math.Sqrt(2.0 / InputSignal.Samples.Count);
                samples.Add((float)result);
            }
            try
            {
                StreamWriter sww = new StreamWriter(@"C:\Users\OMEN\Desktop\dct.txt");
                for (int i = 0; i < samples.Count; i++)
                {
                    
                    sww.WriteLine(samples[i]);
                }
                sww.Close();
            }
            catch (Exception e)
            {
                e.GetBaseException();
               // Console.WriteLine("errrro");
            }
            OutputSignal = new Signal(samples, false);
        }
    }
}

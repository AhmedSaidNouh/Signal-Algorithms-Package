using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
             samples = new List<float>();


         if (type== "sin")
         {
               double seta=0;
                double rad = 0;
                for(int i=0;i< SamplingFrequency;i++)
                {
                    rad = ((2.00 * 180.00 * AnalogFrequency * i) / SamplingFrequency) *( Math.PI)/ 180.00;
                    seta = rad + PhaseShift;
                    double result = A * Math.Sin(seta);
                    result = Math.Round(result, 6);
                    samples.Add((float)result);
                   
                }
            
         }
         else
            {
                double seta = 0;
                double rad = 0;
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    rad = ((2.00 * 180.00 * AnalogFrequency * i) / SamplingFrequency) * (Math.PI) / 180.00;
                    seta = rad + PhaseShift;
                    double result = A * Math.Cos(seta);
                    result = Math.Round(result, 6);
                    samples.Add((float)result);

                }

            }
        }   

       
    }

}

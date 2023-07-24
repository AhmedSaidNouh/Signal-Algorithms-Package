using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay:Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }
        List<float> corr = new List<float>();
        DirectCorrelation cor;
       float max=0;
            int index = 0;
        public override void Run()
        {
            cor = new DirectCorrelation();
            cor.InputSignal1 = InputSignal1;
            cor.InputSignal2 = InputSignal2;
            cor.Run();
            corr = cor.OutputNormalizedCorrelation;
            for(int i=0;i<corr.Count;i++)
            {
                if(Math.Abs(corr[i])>max)
                {
                    max = corr[i];
                        index = i;
                }
            }
            OutputTimeDelay = index * InputSamplingPeriod;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        List<float> x = new List<float>();
        public override void Run()
        {
            for(int i=0;i<InputSignal.Samples.Count;i++)
            {
                if(i==0)
                {
                    x.Add(InputSignal.Samples[i]);

                }
                else
                {
                    float result = 0;
                    for(int j=0;j<=i;j++)
                    {
                        result = InputSignal.Samples[j] + result;
                    }
                    x.Add(result);
                }

            }
            OutputSignal = new Signal(x, false);
        }
    }
}

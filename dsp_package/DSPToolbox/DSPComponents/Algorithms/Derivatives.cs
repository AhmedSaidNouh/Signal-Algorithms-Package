using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }
        List<float> samp1=new List<float>();
        List<float> samp2 = new List<float>();
        public override void Run()
        { float result = 0;

          for(int i=0;i < InputSignal.Samples.Count-1;i++)
            {
                if ((i - 1) >= 0)
                {
                    result = InputSignal.Samples[i]- InputSignal.Samples[i-1]; 
                }
                else
                    result = InputSignal.Samples[i];
                samp1.Add(result);

            }
            for (int i = 0; i < InputSignal.Samples.Count-1; i++)
            {
                if ((i - 1) >= 0 && (i + 1) < InputSignal.Samples.Count)
                {
                    result = InputSignal.Samples[i + 1] - 2*InputSignal.Samples[i] + InputSignal.Samples[i - 1];
                }
                else if ((i + 1) < InputSignal.Samples.Count && (i - 1) < 0)
                {
                    result = InputSignal.Samples[i + 1] - 2*InputSignal.Samples[i];
                }
                else if ((i - 1) >= 0 && (i + 1) > InputSignal.Samples.Count)
                {
                    result = -2*InputSignal.Samples[i] + InputSignal.Samples[i - 1];
                }
                else
                    result = -2*InputSignal.Samples[i];

               // Console.WriteLine(result);

                samp2.Add(result);
            }
            FirstDerivative = new Signal(samp1, false);
            SecondDerivative = new Signal(samp2, false);
        }
    }
}

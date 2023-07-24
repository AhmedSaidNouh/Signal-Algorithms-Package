using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }
        List<float> sample = new List<float>();
        List<int> ind = new List<int>();
        public override void Run()
        { int cond =  InputSignal.SamplesIndices[InputSignal.SamplesIndices.Count - 1];
            int condd = InputSignal.SamplesIndices[0];

            for (int i = InputSignal.Samples.Count - 1; i >= 0; i--)
                {
                    
                    sample.Add(InputSignal.Samples[i]);
                   
                    ind.Add(InputSignal.SamplesIndices[i]*-1);
                }
            if (cond == 0 ||(condd==0&& InputSignal.Samples.Count>10))
            {
                ind = InputSignal.SamplesIndices;

            }
          
                OutputFoldedSignal = new Signal(sample, ind, false);

        }
    }
}

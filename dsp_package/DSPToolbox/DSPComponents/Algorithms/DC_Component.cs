using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        List<float> samples = new List<float>();
        public override void Run()
        {
            float result=0;
           
           for (int i=0;i<InputSignal.Samples.Count;i++)
            {
                result = result + InputSignal.Samples[i];
            }
            result = result / InputSignal.Samples.Count;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                samples.Add(  InputSignal.Samples[i]- result);
              
            }
            OutputSignal = new Signal(samples, false);
        }
    }
}

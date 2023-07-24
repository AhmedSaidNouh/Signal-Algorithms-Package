using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }
        List<float> samples = new List<float>();
        float min;
        float max;
        float result;
        public override void Run()
        {
            max = InputSignal.Samples.Max();
            min = InputSignal.Samples.Min();
            for (int i=0;i< InputSignal.Samples.Count;i++)
            {
                result = (InputMaxRange - InputMinRange) * ((InputSignal.Samples[i] - min) /( max - min))+ InputMinRange;
                samples.Add(result);
            }
            OutputNormalizedSignal = new Signal(samples,false);
        }

    }
}

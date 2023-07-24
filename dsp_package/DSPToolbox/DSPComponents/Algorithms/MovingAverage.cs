using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
        List<float> x = new List<float>();
 
        public override void Run()
        {
           for(int i=0;i<InputSignal.Samples.Count-(InputWindowSize-1);i++)
            {
                float result = 0;
                for(int j=0;j<InputWindowSize;j++)
                {
                    result = InputSignal.Samples[i + j] + result;
                   
                }
                x.Add(result / InputWindowSize);
            }
            OutputAverageSignal = new Signal(x, false);
        }
    }
}

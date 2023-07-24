using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }
        List<int> ind = new List<int>();
        void shift()
        {
            int result;
            if (InputSignal.Samples[0]==1)
            for (int i = 0; i < InputSignal.SamplesIndices.Count; i++)
            {
                result = InputSignal.SamplesIndices[i] - ShiftingValue;
                ind.Add(result);

            }
            else
            {
                for (int i = 0; i < InputSignal.SamplesIndices.Count; i++)
                {
                    result = InputSignal.SamplesIndices[i] + ShiftingValue;
                    ind.Add(result);

                }

            }
        }
        public override void Run()
        {

            shift();
            
          
            OutputShiftedSignal = new Signal(InputSignal.Samples,ind,false);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }//mid point in sample//
        public List<int> OutputIntervalIndices { get; set; } // حط فى رقم الليفل//
        public List<string> OutputEncodedSignal { get; set; }// حط الليفل بالبينرى
        public List<float> OutputSamplesError { get; set; }//حط لكل واح الايرور بتاعه//
        List<float> mid = new List<float>();
        float max;
        float min;
        float lenthg;
        List<float> intterval = new List<float>();
        public override void Run()
        {
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();
           OutputIntervalIndices = new List<int>();
            max = InputSignal.Samples.Max();
            min = InputSignal.Samples.Min();
          if (InputLevel==0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            if (InputNumBits == 0)
            {
                InputNumBits =(int) Math.Log(InputLevel, 2);
            }
            lenthg = (max - min) / InputLevel;

            intterval.Add(min);
            for(int i=0;i<InputLevel;i++)
            {
                float result;
                result = intterval[i] + lenthg;
                intterval.Add(result);
            }
            for(int i=0;i< InputSignal.Samples.Count;i++)
            {
                for(int j=0;j<InputLevel;j++)
                {
                    if (InputSignal.Samples[i]>= intterval[j] && InputSignal.Samples[i]<= intterval[j+1]+0.0001)
                    {
                       
                       
                        OutputIntervalIndices.Add(j + 1);
                      
                    }
                }
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float result = (intterval[OutputIntervalIndices[i]] + intterval[OutputIntervalIndices[i ]-1]) / 2;
                mid.Add(result);
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float result = mid[i] - InputSignal.Samples[i];
                OutputSamplesError.Add(result);
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            { string result = Convert.ToString(OutputIntervalIndices[i] - 1, 2).PadLeft(InputNumBits, '0');
                OutputEncodedSignal.Add(result);
            }
            OutputQuantizedSignal = new Signal(mid, false);
        }
       
    }
}

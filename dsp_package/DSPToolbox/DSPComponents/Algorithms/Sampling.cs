using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }




        public override void Run()
        {
            List<float> output = new List<float>();
            List<float> s = new List<float>();
            OutputSignal = new Signal(new List<float>(), false);
            if (M == 0 && L != 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        output.Add(0);
                    }
                }
                FIR fir = new FIR();
                fir.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputCutOffFrequency = 1500;
                fir.InputTransitionBand = 500;
                fir.InputTimeDomainSignal = new Signal(output, false);
                fir.Run();
                for (int i = 0; i < 1165; i++)
                {
                    if(fir.OutputYn.Samples[i]!=0)
                    OutputSignal.Samples.Add(fir.OutputYn.Samples[i]);
                }
            }
            else if (M != 0 && L == 0)
            {
                 FIR fir = new FIR();
                fir.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputCutOffFrequency = 1500;
                fir.InputTransitionBand = 500;
                fir.InputTimeDomainSignal = new Signal(InputSignal.Samples, false);
                fir.Run();
                output = fir.OutputYn.Samples;
                for (int i = 0; i < (output.Count - 1) / 2; i++)
                {
                    s.Add(output[M * i]);
                }
                OutputSignal = new Signal(s, false);
            }
            else if (M != 0 && L != 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        output.Add(0);
                    }
                }
                FIR fir = new FIR();
                fir.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputCutOffFrequency = 1500;
                fir.InputTransitionBand = 500;
                fir.InputTimeDomainSignal = new Signal(output, false);
                fir.Run();
                output = fir.OutputYn.Samples;
                for (int i = 0; i <= (output.Count - 1) / 2; i++)
                {
                    s.Add(output[M * i]);
                }

                OutputSignal = new Signal(s, false);
            }
            else
            {
                //throw new NotImplementedException();
              
            }

          

        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        List<Complex> signal1 = new List<Complex>();
        List<Complex> signal2 = new List<Complex>();
        List<Complex> signal11 = new List<Complex>();
        List<Complex> mult = new List<Complex>();
        List<Complex> inf= new List<Complex>();
        List<float> amp = new List<float>();
        List<float> phas = new List<float>();
        List<float> corr = new List<float>();
        List<float> norm = new List<float>();
        DiscreteFourierTransform fast1 = new DiscreteFourierTransform();
        DiscreteFourierTransform fast2 = new DiscreteFourierTransform();
        InverseDiscreteFourierTransform inff=new InverseDiscreteFourierTransform();
        float sum1, sum2, total;
        public override void Run()
        {
            sum1 = 0;
            sum2 = 0;
            total = 0;

            if (InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;

            }
            if (InputSignal1.Samples.Count > InputSignal2.Samples.Count)
            {
                int num_samp;
                num_samp = (InputSignal1.Samples.Count + InputSignal2.Samples.Count) - 1;
                int s = (num_samp - InputSignal1.Samples.Count);
                for (int i = 0; i < s; i++)
                {

                    InputSignal1.Samples.Add(0);

                }
                s = (num_samp - InputSignal2.Samples.Count);
                for (int i = 0; i < s; i++)
                {
                    InputSignal2.Samples.Add(0);
                }
            }
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                sum1 = sum1 + (float)Math.Pow(InputSignal1.Samples[i], 2);

            }
            for (int i = 0; i < InputSignal2.Samples.Count; i++)
            {

                sum2 = sum2 + (float)Math.Pow(InputSignal2.Samples[i], 2);
            }
            total = (float)Math.Sqrt((sum1 * sum2)) / InputSignal1.Samples.Count;
            fast1.InputTimeDomainSignal = new DSPAlgorithms.DataStructures.Signal(InputSignal1.Samples, false);
            fast1.Run();
            signal1 = fast1.frar;
            fast2.InputTimeDomainSignal = new DSPAlgorithms.DataStructures.Signal(InputSignal2.Samples, false);
            fast2.Run();
            signal2 = fast2.frar;
            for (int i = 0; i < signal1.Count; i++)
                signal11.Add(new Complex(signal1[i].Real, (-1 * signal1[i].Imaginary)));
            for (int i = 0; i < signal2.Count; i++)
                mult.Add(signal11[i] * signal2[i]);


            for (int i = 0; i < mult.Count; i++)
            {
                amp.Add((float)mult[i].Magnitude);
                phas.Add((float)mult[i].Phase);
            }

            inff.InputFreqDomainSignal = new DSPAlgorithms.DataStructures.Signal(true, InputSignal1.Frequencies, amp, phas);
            inff.Run();
            inf = inff.result;
            for (int i = 0; i < inf.Count; i++)
            {
                inf[i] = inf[i] / inf.Count;
              //  float x = (float)Math.Round(((float)inf[i].Real / inf.Count), 13);
                 float x = (float)inf[i].Real / inf.Count;
                corr.Add(x);
                norm.Add(corr[i] / total);



            }

            OutputNonNormalizedCorrelation = corr;
            OutputNormalizedCorrelation = norm;
        }
    }
}
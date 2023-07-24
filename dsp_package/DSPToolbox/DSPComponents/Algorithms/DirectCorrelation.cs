using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        List<float> sample1 = new List<float>();
        List<float> sample2= new List<float>();
        List<float> corr = new List<float>();
        List<float> nor = new List<float>();
        float sum1,sum2,total;
        public override void Run()
        {
            sum1 = 0;
            sum2 = 0;
            total = 0;
            int num_samp;
            if (InputSignal2!=null )
            {
              
               
                num_samp = (InputSignal1.Samples.Count + InputSignal2.Samples.Count) - 1;
                sample1 = InputSignal1.Samples;
                sample2 = InputSignal2.Samples;
                int s = (num_samp - InputSignal1.Samples.Count);
                for (int i = 0; i < s; i++)
                {

                    sample1.Add(0);

                }
                s = (num_samp - InputSignal2.Samples.Count);
                for (int i = 0; i < s; i++)
                {
                    sample2.Add(0);
                }

            }
            else
            {
                num_samp = InputSignal1.Samples.Count;
                sample1 = InputSignal1.Samples;
                sample2 = InputSignal1.Samples;
            }

            for (int i = 0; i < num_samp; i++)
            {
                sum1 = sum1 + (float)Math.Pow(sample1[i], 2);
                sum2 = sum2 + (float)Math.Pow(sample2[i], 2);
            }
            total = (float)Math.Sqrt((sum1 * sum2)) / num_samp;
            if (InputSignal1.Periodic == false)
            {

                for (int i = 0; i < num_samp; i++)
                {
                    float x = 0;
                    for (int j = 0; j < num_samp - i; j++)
                    {
                        x = sample1[j] * sample2[j + i] + x;

                    }
                    corr.Add(x / num_samp);
                }

            }
            else
            {
                for (int i = 0; i < num_samp; i++)
                {
                    float x = 0;
                    int c = 0;
                    for (int j = 0; j < num_samp; j++)
                    {
                        if (i + j >= num_samp)
                        {
                            x = sample1[j] * sample2[c] + x;
                            c++;
                        }
                        else
                            x = sample1[j] * sample2[j + i] + x;

                    }
                    corr.Add(x / num_samp);
                }

            }
            for (int i = 0; i < corr.Count; i++)
            {
                nor.Add(corr[i] / total);
            }
        

        OutputNonNormalizedCorrelation = corr;
                OutputNormalizedCorrelation = nor ;
        }
    }
}
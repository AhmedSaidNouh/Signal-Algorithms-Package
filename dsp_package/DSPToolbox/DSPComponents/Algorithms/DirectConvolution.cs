using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }
       
        public override void Run()
        {
            int m = InputSignal1.Samples.Count;
            int n = InputSignal2.Samples.Count;
            int k = m + n - 1;

            var convolution = new List<float>();

            float sum = 0;
            for (int i = 0; i < k; i++)
            {
                sum = 0;

                for (int j = 0; j < n; j++)
                {
                    if (i - j >= 0 && i - j < m)
                    {
                        sum += InputSignal1.Samples[i - j] * InputSignal2.Samples[j];
                    }

                }

                convolution.Add(sum);
            }
            var indexies = new List<int>();
            indexies.Add(InputSignal1.SamplesIndices[0]);
            for (int i = 1; i < k; i++)
            {
                indexies.Add(indexies[i - 1] + 1);
            }

            OutputConvolvedSignal = new Signal(convolution, indexies, false);
        }
        public Signal Get_convolo()
        {
            //float v = 0;
            List<float> conv = new List<float>();
            List<int> index = new List<int>();
            //throw new NotImplementedException();
            int len1 = InputSignal1.Samples.Count;
            int len2 = InputSignal2.Samples.Count;
            int n = len1 + len2 - 1;
            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    int val = InputSignal1.SamplesIndices[i] + InputSignal2.SamplesIndices[j];
                    if (!index.Contains(val))
                    {
                        index.Add(val);
                    }
                }
            }
            int maximum = Math.Max(len1, len2);
            for (int i = 0; i < n; i++)
            {
                float a = 0;
                float b = 0;
                float v = 0;
                for (int j = 0; j <= i; j++)
                {
                    if (!(i - j >= len1))
                    {
                        a = InputSignal1.Samples[i - j];
                    }
                    else
                    {
                        a = 0;
                    }

                    if (!(j >= len2))
                    {
                        b = InputSignal2.Samples[j];
                    }
                    else
                    {
                        b = 0;
                    }


                    v += a * b;


                }
                conv.Add(v);

            }
            return OutputConvolvedSignal = new Signal(conv, index, false);
        }
    }
}

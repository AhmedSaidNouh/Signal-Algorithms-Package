using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }// الارقام بتاع الموجه
        public float InputSamplingFrequency { get; set; }//
        public Signal OutputFreqDomainSignal { get; set; }
        List<float>[] power_e = new List<float>[5500];// تخزين بور ال e
        List<float>[] real_terms = new List<float>[5500];
        List<float>[] img_terms = new List<float>[5500];
        List<float> real = new List<float>();
        List<float> img = new List<float>();
        List<float> ampl = new List<float>();
        List<float> shaft = new List<float>();
        List<Complex> rit = new List<Complex>();
        public List<Complex> frar = new List<Complex>();
        public static void fourer(List<float>[] power_e, List<float>[] real_terms, List<float>[] img_terms ,int df, Signal InputTimeDomainSignal)
        {
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)//i equals k
            {
                power_e[i] = new List<float>();

                for (int j = 0; j < InputTimeDomainSignal.Samples.Count; j++)//j equal n
                {
                    float result = (2 * i * j) / (float)InputTimeDomainSignal.Samples.Count;
                    power_e[i].Add(result);
                }

            }
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)//real and img
            {
                real_terms[i] = new List<float>();
                img_terms[i] = new List<float>();

                for (int j = 0; j < InputTimeDomainSignal.Samples.Count; j++)
                {
                    float seta = ((power_e[i][j] * 180) * (float)Math.PI) / 180;
                    if (df == 0)
                    {
                        float re = (float)Math.Cos(seta) * InputTimeDomainSignal.Samples[j];
                        float im = (float)(Math.Sin(seta) * InputTimeDomainSignal.Samples[j]) * -1;
                        real_terms[i].Add(re);
                        img_terms[i].Add(im);          
                    }
                }


            }
        }
        public override void Run()
        {


            fourer( power_e,  real_terms,  img_terms, 0, InputTimeDomainSignal);
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)//real and img
            {
                float re = 0;
                float im = 0;
                for (int j = 0; j < InputTimeDomainSignal.Samples.Count; j++)
                {
                    re = real_terms[i][j] + re;
                    im = img_terms[i][j] + im;
                }

                real.Add(re);
                img.Add(im);
                float amplutude = (float)Math.Sqrt(Math.Pow(re, 2) + Math.Pow(im, 2));
                 float tan =(float) Math.Atan2(im, re);
                ampl.Add(amplutude);
                rit.Add(new Complex(amplutude, tan));
                 shaft.Add(tan);
            }
            try
            {
                StreamWriter sww = new StreamWriter(@"D:\computer scince\الفرقه الثالثه\signal\lab\Lab 1\Package\fcisdsp-dsp.toolbox-78ddd969882b\DSPToolbox\Test.txt");
                 for (int i=0;i<rit.Count;i++)
                 {
                     //x = rit[i].ToString();
                     sww.WriteLine(rit[i]);
                 }
                sww.Close();
            }
            catch(Exception e)
            {

            }

            for (int i = 0; i < real.Count; i++)
            {
                frar.Add(new Complex(real[i], img[i]));
            }
            OutputFreqDomainSignal = new Signal(false,null,ampl, shaft);
           
        }
    }
}

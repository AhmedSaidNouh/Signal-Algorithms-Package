using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }
        List<float>[] power_e = new List<float>[5500];
        List<Complex>[] comp_e = new List<Complex>[5500];
        List<Complex> frequen = new List<Complex>();
        List<Complex>[] comp_each_term = new List<Complex>[5500];
       public List<Complex> result = new List<Complex>();
        List<float> x = new List<float>();
        public override void Run()
        {
           
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                float re = InputFreqDomainSignal.FrequenciesAmplitudes[i] * (float)Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                float im = InputFreqDomainSignal.FrequenciesAmplitudes[i] * (float)Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                frequen.Add(new Complex(re, im));
            }

            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)//i equals n
            {
                power_e[i] = new List<float>();

                for (int j = 0; j < InputFreqDomainSignal.FrequenciesAmplitudes.Count; j++)//j equal k
                {
                    float result = (2 * i * j) / (float)InputFreqDomainSignal.FrequenciesAmplitudes.Count;
                    power_e[i].Add(result);
                }

            }
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)//real and img
            {
                comp_e[i] = new List<Complex>();

                for (int j = 0; j < InputFreqDomainSignal.FrequenciesAmplitudes.Count; j++)
                {
                    float seta = ((power_e[i][j] * 180) * (float)Math.PI) / 180;


                    float re = (float)Math.Cos(seta);
                    float im = (float)Math.Sin(seta);
                    comp_e[i].Add(new Complex(re, im));
                }


            }
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)//real and img
            {
                

                comp_each_term[i] = new List<Complex>();


                for (int j = 0; j < InputFreqDomainSignal.FrequenciesAmplitudes.Count; j++)
                {
                    Complex c = frequen[j] * comp_e[i][j];
                    comp_each_term[i].Add(c);
                }
              
            }
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)//real and img
            {
                Complex c = comp_each_term[i][0];
                


                for (int j = 1; j <( InputFreqDomainSignal.FrequenciesAmplitudes.Count); j++)
                {
                    c = c + comp_each_term[i][j];

                   
                }
                result.Add(c);
                int rest = (int)(Math.Round(result[i].Real / 8,1));
                x.Add((float)rest);
            }
            OutputTimeDomainSignal = new Signal(x, false);
        }
    }
}

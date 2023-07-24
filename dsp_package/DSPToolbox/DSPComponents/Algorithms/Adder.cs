using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }
       List<float> new_sample=new List<float>();
        List<float>[] samples;
        int num_signle;
        int size;
        public override void Run()
        {
            size = 0;
            num_signle = InputSignals.Count;
            samples = new List<float>[20];
            for(int i=0;i<20;i++)
            {
                samples[i] = new List<float>();
            }
            for(int i=0;i<InputSignals.Count;i++)
            {
                if(InputSignals[i].Samples.Count>size)
                {
                    size = InputSignals[i].Samples.Count;
                }
                for(int j=0;j<InputSignals[i].Samples.Count;j++)
                {
                    samples[i].Add(InputSignals[i].Samples[j]);
                }
                float y = 15.000f;
                Console.WriteLine("wwwww" + y);
            }
            for(int i=0;i< InputSignals.Count; i++)
            {
                if(samples[i].Count<size)
                {
                    for(int j=samples[i].Count;j<size;j++)
                    {
                        samples[i].Add(0);
                    }
                }
            }
            for (int i = 0; i < InputSignals.Count; i++)
            {
                for(int j=0;j<size;j++)
                {
                    if (i == 0)
                    {
                        new_sample.Add(samples[i][j]);
                    }
                    else
                    {
                        new_sample[j] = new_sample[j] + samples[i][j];
                    }
                }
                
            }
          //  for (int i = 0; i < samples.Count; i++)
          // {
          // sample = sample + samples[i];
          //}
           OutputSignal = new Signal(new_sample, false);


        }
    }
}
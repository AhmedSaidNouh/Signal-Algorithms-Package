using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }
        int N = 0;

        double catch_window(int num, int n)
        {
            switch (num)
            {
                case 1:
                    //rectangular
                    return 1.0;
                case 2:
                    return (float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * n) / N));
                case 3:
                    //hamming
                    return (float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * n) / N));
                case 4:
                    //blackman
                    double sec1 = (float)(0.5 * Math.Cos((2 * Math.PI * n) / (N - 1)));
                    double sec2 = (float)(0.08 * Math.Cos((4 * Math.PI * n) / (N - 1)));
                    return (float)(0.42 + sec1 + sec2);
            }
            return 0.0;
        }

        public override void Run()
        {
            int window = 0;
            double Ndash = 0;
            //rectangular
            if (InputStopBandAttenuation > 0 && InputStopBandAttenuation <= 21)
            {
                window = 1;
                Ndash = (0.9 / (InputTransitionBand / InputFS));
            }
            //hamming
            else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
            {
                window = 2;
                Ndash = (3.1 / (InputTransitionBand / InputFS));
            }//hamming
            else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
            {
                window = 3;
                Ndash = (3.3 / (InputTransitionBand / InputFS));
            }//blackman
            else if (InputStopBandAttenuation > 53)
            {
                window = 4;
                Ndash = (5.5 / (InputTransitionBand / InputFS));
            }
            if (Math.Ceiling(Ndash) % 2 == 0)
                N = (int)Math.Ceiling(Ndash) + 1;

            else
                N = (int)Math.Ceiling(Ndash);

            List<float> samples = new List<float>();
            List<int> indecies = new List<int>();
            //after thati have N and window Number
            if (InputFilterType == FILTER_TYPES.LOW)
            {
                InputCutOffFrequency = InputCutOffFrequency + (InputTransitionBand / 2);
                InputCutOffFrequency /= InputFS;
                int ii = (N - 1) / 2;
                float v1 = 0, v2 = 0;
                for (int i = -ii; i <= ii; i++)
                {

                    if (i == 0)
                    {
                        v1 = (float)catch_window(window, i);
                        v2 = 2 * (float)InputCutOffFrequency;
                    }

                    else
                    {
                        v1 = (float)catch_window(window, Math.Abs(i));
                        double rad = ((Math.Abs(i) * 360 * (double)InputCutOffFrequency) * (float)Math.PI) / 180;
                        v2 = 2 * (float)InputCutOffFrequency *
                            (float)((float)(Math.Sin(rad)) / (Math.Abs(i) * (2 * Math.PI * (double)InputCutOffFrequency)));

                      
                    }
                    indecies.Add(i);
                    samples.Add((v1 * v2));

                }

            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                InputCutOffFrequency = (InputCutOffFrequency + (InputTransitionBand / 2));
                InputCutOffFrequency /= InputFS;      
                int ii = (N - 1) / 2;
                double v1 = 0, v2 = 0;
                for (int i = -ii; i <= ii; i++)
                {

                    if (i == 0)
                    {
                        v1 = catch_window(window, i);
                        v2 = 1 - (2 * (double)InputCutOffFrequency);

                    }

                    else
                    {
                        v1 = (double)catch_window(window, Math.Abs(i));
                        double rad = ((Math.Abs(i) * 360 * (double)InputCutOffFrequency) * Math.PI) / 180;
                        v2 = -2 * (double)InputCutOffFrequency * ((Math.Sin(rad)) / (Math.Abs(i) * (2 * Math.PI * (double)InputCutOffFrequency)));

                    }
                    indecies.Add(i);
                    samples.Add((float)(v1 * v2));
                }


            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                InputF1 = (InputF1 - (InputTransitionBand / 2));
                InputF1 /= InputFS;
                InputF2 = (InputF2 + (InputTransitionBand / 2));
                InputF2 /= InputFS;
                int ii = (N - 1) / 2;
                double v1 = 0, v2 = 0;
                for (int i = -ii; i <= ii; i++)
                {

                    if (i == 0)
                    {
                        v1 = catch_window(window, Math.Abs(i));
                        v2 = (2 * (float)(InputF2 - InputF1));

                    }

                    else
                    {
                        v1 = (float)catch_window(window, Math.Abs(i));
                        float part2 = (float)(2 * Math.PI * InputF2 * Math.Abs(i));
                        float part1 = (float)(2 * Math.PI * InputF1 * Math.Abs(i));
                        v2 = (float)(((2 * InputF2 * Math.Sin(part2)) / part2) - ((2 * InputF1 * Math.Sin(part1)) / part1));

                    }
                    indecies.Add(i);
                    samples.Add((float)(v1 * v2));
                }
            }
            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                InputF1 = (InputF1 - (InputTransitionBand / 2));
                InputF1 /= InputFS;
                InputF2 = (InputF2 + (InputTransitionBand / 2));
                InputF2 /= InputFS;
                int ii = (N - 1) / 2;
                double v1 = 0, v2 = 0;
                for (int i = -ii; i <= ii; i++)
                {


                    if (i == 0)
                    {
                        v2 = 1 - (2 * (float)(InputF2 - InputF1));
                        v1 = (float)catch_window(window, Math.Abs(i));

                    }
                    else
                    {
                        float w2 = (float)(2 * Math.PI * InputF2 * Math.Abs(i));
                        float w1 = (float)(2 * Math.PI * InputF1 * Math.Abs(i));
                        v2 = (float)((2 * InputF1 * Math.Sin(w1) / w1) - (2 * InputF2 * Math.Sin(w2) / w2));

                        v1 = (float)catch_window(window, Math.Abs(i));

                    }
                    indecies.Add(i);
                    samples.Add((float)(v1 * v2));
                }
            }
            OutputHn = new Signal(samples, indecies, false);
            DirectConvolution c = new DirectConvolution();
            c.InputSignal1 = InputTimeDomainSignal;
            c.InputSignal2 = OutputHn;
            c.Run();
            OutputYn = c.Get_convolo();
            try
            {
                StreamWriter sww = new StreamWriter("file.txt",false);
                for (int i = 0; i < OutputHn.Samples.Count; i++)
                {

                    sww.WriteLine(OutputHn.Samples[i]);
                }
                sww.Close();
            }
            catch (Exception e)
            {
                e.GetBaseException();
                // Console.WriteLine("errrro");
            }
        }
    }
}

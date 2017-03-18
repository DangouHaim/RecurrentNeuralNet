using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNN
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] pat = new double[][]
            {
                new double[] { 1 },
                new double[] { 2 },
                new double[] { 3 },
                new double[] { 4 }
            };

            double[] a = new double[] { 1, 3, 5, 7 };

            Neuron[] n = new Neuron[9];
            for(int i = 0; i < n.Length; i++)
            {
                n[i] = new Neuron(1, 4, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            }
            Neuron[] c = new Neuron[9];
            for(int i = 0; i < c.Length; i++)
            {
                c[i] = new Neuron(4, 4, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            }
            Neuron[] c2 = new Neuron[9];
            for(int i = 0; i < c2.Length; i++)
            {
                c2[i] = new Neuron(4, 4, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            }
            Neuron[] o = new Neuron[9];
            for(int i = 0; i < o.Length; i++)
            {
                o[i] = new Neuron(4, 1, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            }








            double ge = 0;
            do
            {
                ge = 0;
                for(int p = 0; p < pat.Length; p++)
                {
                    for (int current = 0; current < n.Length; current++)
                    {
                        n[current].SetIn(pat[p]);
                        n[current].Out();
                        n[current].SetRecurrentContext(c[current]);

                        c[current].SetIn(n[current]._out);
                        c[current].Out();
                        c[current].SetRecurrentContext(c2[current]);

                        c2[current].SetIn(c[current]._out);
                        c2[current].Out();


                        o[current].SetIn(n[current]._out);
                        o[current].Out();
                    }

                    double[] maxOut = maxOut = o[0]._out;
                    double s = 0;
                    for (int i = 0; i < o.Length; i++)
                    {
                        double cur = 0;
                        foreach (double d in o[i]._out)
                        {
                            cur += d;
                        }
                        if(cur > s)
                        {
                            s = cur;
                            maxOut = o[i]._out;
                        }
                    }

                    for (int current = 0; current < n.Length; current++)
                    { 
                        

                        List<double> e = new List<double>();//////////////////////////////
                        for (int i = 0; i < o[current]._out.Length; i++)
                        {
                            e.Add(a[p] - maxOut[i]);
                            ge += Math.Abs(e[i]);
                        }

                        List<double[]> e1 = new List<double[]>();
                        for (int i = 0; i < o[current]._out.Length; i++)
                        {
                            List<double> cur = new List<double>();

                            for (int j = 0; j < o[current]._in.Length; j++)
                            {
                                cur.Add(e[i] * o[current]._wio[j, i]);
                            }
                            e1.Add(cur.ToArray());
                        }

                        o[current].Study(e.ToArray());

                        foreach (double[] d in e1)
                        {
                            c2[current].Study(d);
                            c[current].Study(d);
                            n[current].Study(d);
                        }
                    }
                }


                Console.WriteLine(ge);
            }
            while (ge > 0);

            Console.WriteLine();
            pat = new double[][]
            {
                new double[] { 5 },
                new double[] { 6 }
            };
            for (int p = 0; p < pat.Length; p++)
            {
                for (int current = 0; current < n.Length; current++)
                {
                    n[current].SetIn(pat[p]);
                    n[current].Out();
                    n[current].SetRecurrentContext(c[current]);

                    c[current].SetIn(n[current]._out);
                    c[current].Out();
                    c[current].SetRecurrentContext(c2[current]);

                    c2[current].SetIn(c[current]._out);
                    c2[current].Out();


                    o[current].SetIn(n[current]._out);
                    o[current].Out();
                }

                double[] maxOut = maxOut = o[0]._out;
                double s = 0;
                for (int i = 0; i < o.Length; i++)
                {
                    double cur = 0;
                    foreach (double d in o[i]._out)
                    {
                        cur += d;
                    }
                    if (cur > s)
                    {
                        s = cur;
                        maxOut = o[i]._out;
                    }
                }

                foreach (double d in maxOut)
                {
                    Console.WriteLine(d);
                }
            }

            Console.Read();
        }
    }
}

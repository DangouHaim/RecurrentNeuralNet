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

            Neuron n = new Neuron(1, 4, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            Neuron c = new Neuron(4, 4, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            Neuron c2 = new Neuron(4, 4, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);
            Neuron o = new Neuron(4, 1, new double[] { 0, 1, 3, 5, 7, 9, 11 }, 0.6, 0.01);

            double ge = 0;
            do
            {
                ge = 0;
                for(int p = 0; p < pat.Length; p++)
                {
                    n.SetIn(pat[p]);
                    n.Out();
                    n.SetRecurrentContext(c);

                    c.SetIn(n._out);
                    c.Out();
                    c.SetRecurrentContext(c2);

                    c2.SetIn(c._out);
                    c2.Out();


                    o.SetIn(n._out);
                    o.Out();

                    List<double> e = new List<double>();
                    for(int i = 0; i < o._out.Length; i++)
                    {
                        e.Add(a[p] - o._out[i]);
                        ge += Math.Abs(e[i]);
                    }

                    List<double[]> e2 = new List<double[]>();
                    for (int i = 0; i < o._out.Length; i++)
                    {
                        List<double> cur = new List<double>();

                        for (int j = 0; j < o._in.Length; j++)
                        {
                            cur.Add(e[i] * o._wio[j, i]);
                        }
                        e2.Add(cur.ToArray());
                    }

                    o.Study(e.ToArray());

                    foreach (double[] d in e2)
                    {
                        c2.Study(d);
                        c.Study(d);
                        n.Study(d);
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
                n.SetIn(pat[p]);
                n.Out();
                n.SetRecurrentContext(c);

                c.SetIn(n._out);
                c.Out();
                c.SetRecurrentContext(c2);

                c2.SetIn(c._out);
                c2.Out();


                o.SetIn(n._out);
                o.Out();

                foreach (double d in o._out)
                {
                    Console.WriteLine(d);
                }
            }

            Console.Read();
        }
    }
}

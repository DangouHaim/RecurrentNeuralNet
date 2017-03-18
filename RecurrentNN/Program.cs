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
                new double[] { 3 }
            };

            double[] a = new double[] { 1, 2, 3 };

            Neuron n = new Neuron(1, 1, new double[] { 0, 1, 2, 3, 4, 5 }, 0.9, 0.01);
            Neuron c = new Neuron(1, 1, new double[] { 0, 1, 2, 3, 4, 5 }, 0.9, 0.01);
            Neuron o = new Neuron(1, 1, new double[] { 0, 1, 2, 3, 4, 5 }, 0.9, 0.01);

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
                    o.SetIn(n._out);
                    o.Out();

                    List<double> e = new List<double>();
                    for(int i = 0; i < o._out.Length; i++)
                    {
                        e.Add(a[p] - o._out[i]);
                        ge += Math.Abs(e[i]);
                    }

                    List<double[]> e2 = new List<double[]>();
                    for(int i = 0; i < o._out.Length; i++)
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
                new double[] { 4 }
            };
            for (int p = 0; p < pat.Length; p++)
            {
                n.SetIn(pat[p]);
                n.Out();
                n.SetRecurrentContext(c);
                c.SetIn(n._out);
                c.Out();
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

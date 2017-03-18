using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurrentNN
{
    class Neuron
    {
        public double[] _in;
        public double[] _out;
        public double[,] _wio;
        public double[] _lim;
        double _speed;
        double _scale;

        public Neuron(int inputs, int outputs, double[] limits, double limitScaleFactor, double speed)
        {
            _in = new double[inputs];
            _out = new double[outputs];
            _wio = new double[inputs, outputs];
            _lim = limits;
            _scale = limitScaleFactor;
            _speed = speed;
            SetW();
        }

        void SetW()
        {
            Random r = new Random();
            for(int i = 0; i < _in.Length; i++)
            {
                for(int j = 0; j < _out.Length; j++)
                {
                    _wio[i, j] = r.NextDouble() * 0.3 + 0.1;
                }
            }
        }

        public void Out()
        {
            for(int i = 0; i < _out.Length; i++)
            {
                _out[i] = 0;
                for(int j = 0; j < _in.Length; j++)
                {
                    _out[i] += _in[j] * _wio[j, i];
                }
                double o = 0;
                double le = 0;
                for(int j = 0; j < _lim.Length; j++)
                {
                    if (_out[i] > (le + (_lim[j] - le) * _scale))
                    {
                        o = _lim[j];
                    }
                    le = _lim[j];
                }
                _out[i] = o;
            }
        }

        public void SetIn(double[] inputs)
        {
            if(_in.Length == inputs.Length)
            {
                _in = inputs;
            }
            else
            {
                for(int i = 0; i < _in.Length; i++)
                {
                    _in[i] = inputs[i];
                }
            }
        }

        public void Study(double[] errors)
        {
            for(int i = 0; i < _in.Length; i++)
            {
                for(int j = 0; j < _out.Length; j++)
                {
                    _wio[i, j] += _speed * errors[j] * _in[i];
                }
            }
        }

        public void SetRecurrentContext(Neuron context)
        {
            for(int i = 0; i < _out.Length; i++)
            {
                _out[i] += context._out[i];
            }
        }

    }
}

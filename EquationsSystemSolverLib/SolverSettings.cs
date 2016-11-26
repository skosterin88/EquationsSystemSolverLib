using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsSystemSolverLib
{
    public interface ISolverSettings
    {
        /// <summary>
        /// Начальные значения переменных системы. 
        /// </summary>
        double[] InitialValues { get; set; }

        /// <summary>
        /// Точность вычисления решения системы. 
        /// </summary>
        double Precision { get; set; }

        /// <summary>
        /// Максимальное количество итераций поиска решения. 
        /// </summary>
        int MaxIterations { get; set; }

    }

    public class NewtonRaphsonSettings : ISolverSettings
    {
        private double[] _initVals;
        private double _precision;
        private int _maxIter;

        public double[] InitialValues
        {
            get
            {
                return _initVals;
            }
            set
            {
                _initVals = value;
            }
        }

        public double Precision
        {
            get
            {
                return _precision;
            }
            set
            {
                _precision = value;
            }
        }

        public int MaxIterations
        {
            get
            {
                return _maxIter;
            }
            set
            {
                _maxIter = value;
            }
        }

        /// <summary>
        /// Копирование настроек решения системы уравнения для повторного присваивания.
        /// </summary>
        /// <returns></returns>
        public ISolverSettings Clone()
        {
            ISolverSettings clone = new NewtonRaphsonSettings();

            clone.InitialValues = new double[_initVals.Length];
            for (int i = 0; i < _initVals.Length; i++)
            {
                clone.InitialValues[i] = _initVals[i];
            }

            clone.Precision = _precision;
            clone.MaxIterations = _maxIter;

            return clone;
        }

        
    }
}

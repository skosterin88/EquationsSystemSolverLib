using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsSystemSolverLib
{
    /// <summary>
    /// Вспомогательный класс для вычисления производных. 
    /// Методы этого класса используются для вычисления якобианов (матриц первых производных) систем уравнений.
    /// </summary>
    public class Derivative
    {
        /// <summary>
        /// Численное дифференцирование входной функции в заданной точке по заданной переменной.
        /// </summary>
        /// <param name="function">Вид дифференцируемой функции.</param>
        /// <param name="variables">Числовые значения переменных.</param>
        /// <param name="inxVariable">Номер переменной, по которой находим производную.</param>
        /// <param name="coefficients">Коэффициенты функции.</param>
        /// <param name="eps">Требуемая точность вычисления производной.</param>
        /// <returns></returns>
        public static double GetDerivative(Func<double[], double[], double> function, double[] variables, int inxVariable, double[] coefficients, double eps)
        {
            double derivative = 0.0;

            double[] variablesPlusEps = new double[variables.Length];
            for (int i = 0; i < variables.Length; i++)
            {
                if (i == inxVariable)
                {
                    variablesPlusEps[i] = variables[i] + eps;
                }
                else
                {
                    variablesPlusEps[i] = variables[i];
                }
            }
            double[] variablesPlus2Eps = new double[variables.Length];
            for (int i = 0; i < variables.Length; i++)
            {
                if (i == inxVariable)
                {
                    variablesPlus2Eps[i] = variables[i] + 2.0 * eps;
                }
                else
                {
                    variablesPlus2Eps[i] = variables[i];
                }
            }

            double[] variablesMinusEps = new double[variables.Length];
            for (int i = 0; i < variables.Length; i++)
            {
                if (i == inxVariable)
                {
                    variablesMinusEps[i] = variables[i] - eps;
                }
                else
                {
                    variablesMinusEps[i] = variables[i];
                }
            }

            double[] variablesMinus2Eps = new double[variables.Length];
            for (int i = 0; i < variables.Length; i++)
            {
                if (i == inxVariable)
                {
                    variablesMinus2Eps[i] = variables[i] - 2.0 * eps;
                }
                else
                {
                    variablesMinus2Eps[i] = variables[i];
                }
            }

            double f = function(variables, coefficients);
            double fPlusEps = function(variablesPlusEps, coefficients);
            double fPlus2Eps = function(variablesPlus2Eps, coefficients);
            double fMinusEps = function(variablesMinusEps, coefficients);
            double fMinus2Eps = function(variablesMinus2Eps, coefficients);

            derivative = (fMinus2Eps - 8.0 * fMinusEps + 8.0 * fPlusEps - fPlus2Eps) / (12.0 * eps);

            return derivative;
        }

    }
}

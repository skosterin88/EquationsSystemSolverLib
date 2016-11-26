using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsSystemSolverLib
{
    /// <summary>
    /// Класс, описывающий систему уравнений. 
    /// </summary>
    public class EquationsSystem
    {
        /// <summary>
        /// Функции, определяющие внешний вид уравнений системы. Каждое уравнение имеет вид F(x1,x2,...xN) = 0.
        /// </summary>
        public List<Func<double[], double[], double>> EquationFunctions { get; set; }
        /// <summary>
        /// Коэффициенты в уравнениях системы.
        /// </summary>
        public List<double[]> EquationsCoefficients { get; set; }
        /// <summary>
        /// Названия переменных. 
        /// </summary>
        public string[] VariableNames { get; set; }

        /// <summary>
        /// Вычисление якобиана системы (т.е. матрицы первых производных входящих в систему функций по переменным).
        /// </summary>
        /// <param name="variables">Значения переменных, при которых вычисляем якобиан. </param>
        /// <param name="eps">Точность вычисления производных, входящих в якобиан.</param>
        /// <returns></returns>
        public double[,] GetJacobianMatrix(double[] variables, double eps)
        {
            int qEquations = this.EquationFunctions.Count;
            int qVariables = this.VariableNames.Length;

            double[,] matrJacobian = new double[qEquations, qVariables];

            for (int i = 0; i < qEquations; i++)
            {
                for (int j = 0; j < qVariables; j++)
                {
                    //(i,j)-ый элемент якобиана вычисляем как производную i-ой функции-уравнения по j-ой переменной. 
                    matrJacobian[i, j] = Derivative.GetDerivative(this.EquationFunctions[i], variables, j, this.EquationsCoefficients[i], eps);
                }
            }

            return matrJacobian;
        }

        /// <summary>
        /// Вычисление значений выражений, задаваемых уравнениями системы. 
        /// </summary>
        /// <param name="variables"></param>
        /// <returns></returns>
        public double[] GetEquationsValues(double[] variables)
        {
            double[] equationsValues = new double[this.EquationFunctions.Count];

            for (int i = 0; i < this.EquationFunctions.Count; i++)
            {
                equationsValues[i] = this.EquationFunctions[i](variables, this.EquationsCoefficients[i]);
            }

            return equationsValues;
        }

        /// <summary>
        /// Создание копии экземпляра класса "система уравнений" для повторного присваивания. 
        /// </summary>
        /// <returns></returns>
        public EquationsSystem Clone()
        {
            EquationsSystem clone = new EquationsSystem();

            clone.EquationFunctions = new List<Func<double[], double[], double>>();
            clone.EquationsCoefficients = new List<double[]>();

            for (int i = 0; i < this.EquationFunctions.Count; i++)
            {
                clone.EquationFunctions.Add(new Func<double[], double[], double>(this.EquationFunctions[i].Invoke));
            }

            for (int i = 0; i < this.EquationsCoefficients.Count; i++)
            {
                clone.EquationsCoefficients.Add(new double[this.EquationsCoefficients[i].Length]);

                for (int j = 0; j < clone.EquationsCoefficients[i].Length; j++)
                {
                    clone.EquationsCoefficients[i][j] = this.EquationsCoefficients[i][j];
                }
            }

            clone.VariableNames = new string[this.VariableNames.Length];

            for (int i = 0; i < this.VariableNames.Length; i++)
            {
                clone.VariableNames[i] = this.VariableNames[i];
            }

            return clone;
        }
    }
}

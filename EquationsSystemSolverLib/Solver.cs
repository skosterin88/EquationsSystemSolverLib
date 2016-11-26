using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationsSystemSolverLib
{
    /// <summary>
    /// Класс-вычислитель решения системы уравнений. Именно его мы вызываем при решении конкретной задачи. 
    /// Содержит следующие параметры: 
    /// - (вход) решаемая система уравнений
    /// - (вход) настройки решения системы
    /// - (вход) непосредственный вычислитель решения, использующий один из предусмотренных методов.
    /// По состоянию на 20.09.2016 для решения систем уравнений предусмотрен только метод Ньютона-Рафсона. 
    /// - (выход) решение системы - массив числовых значений искомых переменных.
    /// </summary>
    public class EquationsSystemSolver
    {
        private ISolveMethod _solutionMethod;
        private double[] _solution;
        #region Входные параметры
        public ISolveMethod SolutionMethod 
        {
            get
            {
                return _solutionMethod;
            }
            set
            {
                _solutionMethod = value;
            }
        }
        #endregion

        #region Выходные параметры
        /// <summary>
        /// Итоговое решение системы уравнений - числовые значения переменных.
        /// </summary>
        public double[] Solution
        {
            get
            {
                return _solution;
            }
            set
            {
                _solution = value;
            }
        }
        #endregion

        public EquationsSystemSolver()
        {
            _solutionMethod = new NewtonRaphsonMethod(null, null);
        }

        public EquationsSystemSolver(ISolveMethod solutionMethod)
        {
            _solutionMethod = solutionMethod;
        }

        /// <summary>
        /// Запуск процесса поиска решения заданной системы.
        /// </summary>
        public void Run()
        {
            int qEquations = _solutionMethod.EqSystem.EquationFunctions.Count;
            int qVars = _solutionMethod.Settings.InitialValues.Length;
            
            //_solution = null;

            if (qEquations != qVars)
            {
                throw new VariablesCountMismatchException("Число неизвестных должно совпадать с числом уравнений!");
            }
            else
            {
                _solution = _solutionMethod.SolveEquationsSystem();
            }
        }
    }
}

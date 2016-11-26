using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace EquationsSystemSolverLib
{
    public interface ISolveMethod
    {
        EquationsSystem EqSystem { get; set; }
        ISolverSettings Settings { get; set; }
        double[] SolveEquationsSystem();
    }

    public class NewtonRaphsonMethod : ISolveMethod
    {
        private EquationsSystem _eqSystem;
        private ISolverSettings _settings;

        public EquationsSystem EqSystem
        {
            get
            {
                return _eqSystem;
            }
            set
            {
                _eqSystem = value;
            }
        }

        public ISolverSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        public NewtonRaphsonMethod(EquationsSystem system, ISolverSettings settings)
        {
            _eqSystem = system;
            _settings = settings;
        }

        public double[] SolveEquationsSystem()
        {
            double[] solution = new double[_eqSystem.EquationFunctions.Count];

            //Выполняем нулевую итерацию на начальных значениях, пришедших из настроек.

            //Находим якобиан при начальных значениях.
            double epsJacobian = 0.00001;
            double[,] jacob = _eqSystem.GetJacobianMatrix(_settings.InitialValues, epsJacobian);
            double[] eqVals = _eqSystem.GetEquationsValues(_settings.InitialValues);

            Matrix<double> matrJacobian = CreateMatrix.DenseOfArray(jacob);
            Vector<double> vectEquationsValues = CreateVector.DenseOfArray(eqVals);

            Matrix<double> matrInvJacobian = matrJacobian.Inverse();
            Vector<double> deltaX = matrInvJacobian.Multiply(-1.0).Multiply(vectEquationsValues);

            Vector<double> vectPrevIterX = CreateVector.DenseOfArray(_settings.InitialValues);

            for (int i = 0; i < _settings.MaxIterations; i++)
            {
                Vector<double> vectCurrIterX = vectPrevIterX.Add(deltaX);
                solution = vectCurrIterX.ToArray();

                jacob = _eqSystem.GetJacobianMatrix(vectCurrIterX.ToArray(), epsJacobian);
                eqVals = _eqSystem.GetEquationsValues(vectCurrIterX.ToArray());

                matrJacobian = CreateMatrix.DenseOfArray(jacob);
                vectEquationsValues = CreateVector.DenseOfArray(eqVals);

                matrInvJacobian = matrJacobian.Inverse();
                deltaX = matrInvJacobian.Multiply(-1.0).Multiply(vectEquationsValues);

                double[] currIterEpsX = new double[vectCurrIterX.Count];

                for (int j = 0; j < vectCurrIterX.Count; j++)
                {
                    currIterEpsX[j] = Math.Abs(vectCurrIterX[j] - vectPrevIterX[j]);
                }

                bool isPreciseSolution = true;

                for (int j = 0; j < currIterEpsX.Length; j++)
                {
                    if (currIterEpsX[j] > _settings.Precision)
                    {
                        isPreciseSolution = false;
                        break;
                    }
                }

                if (isPreciseSolution == true)
                {
                    break;
                }
                else
                {
                    vectPrevIterX = CreateVector.DenseOfArray(vectCurrIterX.ToArray());

                    bool isZeroEqVals = true;

                    for (int k = 0; k < eqVals.Length; k++)
                    {
                        if (Math.Abs(eqVals[k]) > _settings.Precision)
                        {
                            isZeroEqVals = false;
                            break;
                        }
                    }

                    if (isZeroEqVals == true)
                    {
                        break;
                    }
                }
            }

            return solution;
        }
        
    }


}

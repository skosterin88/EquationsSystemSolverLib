using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EquationsSystemSolverLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Solver_Run_VariablesCountMismatch_ExceptionThrown()
        {
            EquationsSystem eqSystem = new EquationsSystem()
            {
                EquationFunctions = new List<Func<double[], double[], double>>(){
                    new Func<double[],double[],double>((x,alpha) => alpha[0]*x[0] + alpha[1]*x[1] + alpha[2]),
                    new Func<double[],double[],double>((x,alpha) => alpha[0]*x[0] + alpha[1]*x[1] + alpha[2])
                },
                EquationsCoefficients = new List<double[]>(){
                    new double[]{
                        2.0, 3.0, -5.0
                    },
                    new double[]{
                        1.0, 4.0, -5.0
                    }
                },
                VariableNames = new string[]{
                    "x","y"
                }
            };

            ISolverSettings solverSettings = new NewtonRaphsonSettings()
            {
                InitialValues = new double[] { 0.5 },
                MaxIterations = 100,
                Precision = 1e-5
            };

            try
            {
                ISolveMethod solutionMethod = new NewtonRaphsonMethod(eqSystem, solverSettings);
                EquationsSystemSolver solver = new EquationsSystemSolver(solutionMethod);
                solver.Run();
            }
            catch (VariablesCountMismatchException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LinearSystem_Solved()
        {
            EquationsSystem eqSystem = new EquationsSystem()
            {
                EquationFunctions = new List<Func<double[], double[], double>>(){
                    new Func<double[],double[],double>((x,alpha) => alpha[0]*x[0] + alpha[1]*x[1] + alpha[2]),
                    new Func<double[],double[],double>((x,alpha) => alpha[0]*x[0] + alpha[1]*x[1] + alpha[2]),
                },
                EquationsCoefficients = new List<double[]>(){
                    new double[]{
                        2.0, 3.0, -5.0
                    },
                    new double[]{
                        1.0, 4.0, -5.0
                    }
                },
                VariableNames = new string[]{
                    "x","y"
                }
            };

            ISolverSettings solverSettings = new NewtonRaphsonSettings()
            {
                InitialValues = new double[] { 0.5, 0.5 },
                MaxIterations = 100,
                Precision = 1e-5
            };

            ISolveMethod solutionMethod = new NewtonRaphsonMethod(eqSystem, solverSettings);
            EquationsSystemSolver solver = new EquationsSystemSolver(solutionMethod);          
            solver.Run();

            bool isCorrectSolution = solver.Solution.All(x => Math.Abs(x - 1.0) < 0.001);
            Assert.AreEqual(true, isCorrectSolution);
        }
    }
}

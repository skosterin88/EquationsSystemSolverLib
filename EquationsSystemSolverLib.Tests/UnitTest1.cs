using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Parameters;


namespace EquationsSystemSolverLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Solver_Run_VariablesCountMismatch_ExceptionThrown()
        {
            var kernel = new StandardKernel(new ConfigModule());

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

            ISolverSettings solverSettings = kernel.Get<ISolverSettings>();

            solverSettings.InitialValues = new double[] { 0.5 };
            solverSettings.MaxIterations = 100;
            solverSettings.Precision = 1e-5;
            

            try
            {


                ISolveMethod solutionMethod = kernel.Get<ISolveMethod>
                    (new ConstructorArgument("system", eqSystem),
                    new ConstructorArgument("settings", solverSettings));
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
            var kernel = new StandardKernel(new ConfigModule());

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

            ISolverSettings solverSettings = kernel.Get<ISolverSettings>();

            solverSettings.InitialValues = new double[] { 0.5, 0.5 };
            solverSettings.MaxIterations = 100;
            solverSettings.Precision = 1e-5;

            ISolveMethod solutionMethod = kernel.Get<ISolveMethod>
                (new ConstructorArgument("system", eqSystem), 
                new ConstructorArgument("settings", solverSettings));
            EquationsSystemSolver solver = new EquationsSystemSolver(solutionMethod);
            solver.Run();

            bool isCorrectSolution = solver.Solution.All(x => Math.Abs(x - 1.0) < 0.001);
            Assert.AreEqual(true, isCorrectSolution);
        }
    }
}

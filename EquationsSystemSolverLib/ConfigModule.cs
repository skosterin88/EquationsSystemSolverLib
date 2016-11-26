using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace EquationsSystemSolverLib
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISolverSettings>().To<NewtonRaphsonSettings>();
            Bind<ISolveMethod>().To<NewtonRaphsonMethod>();
            Bind<EquationsSystem>().ToSelf();
        }
    }
}

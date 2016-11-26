using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EquationsSystemSolverLib
{
    [Serializable()]
    public class VariablesCountMismatchException : Exception
    {
        public VariablesCountMismatchException() : base() { }
        public VariablesCountMismatchException(string message) : base(message) { }
        public VariablesCountMismatchException(string message, Exception innerException) : base(message, innerException) { }

        protected VariablesCountMismatchException(SerializationInfo info, StreamingContext context) { }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyIO.Core.Exceptions
{
    [Serializable]
    public class DatabaseNotFoundException : Exception
    {
        public DatabaseNotFoundException()
        {
        }

        public DatabaseNotFoundException(string message)
            : base(message)
        {
        }

        public DatabaseNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

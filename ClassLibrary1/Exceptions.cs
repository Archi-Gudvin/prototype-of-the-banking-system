using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassLibrary1
{
    public class NotEnoughSignsException : Exception
    {       
        public NotEnoughSignsException(string message)
            : base(message)
        {}
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.Infastructure
{
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop; // сообщение, которое будет выводиться для некорректного свойства в property
        }
    }
}

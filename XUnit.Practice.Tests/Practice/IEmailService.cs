using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit.Practice.Tests.Practice
{
    public interface IEmailService
    {
        bool SendEmail(string to, string message);
    }
}

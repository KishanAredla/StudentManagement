using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit.Practice.Tests.Practice
{
    public class NotificationManager
    {
        private readonly IEmailService _emailService;

        public NotificationManager(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public bool Notify(string email, string message)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (string.IsNullOrWhiteSpace(message))
                return false;

            return _emailService.SendEmail(email, message);
        }
    }

}

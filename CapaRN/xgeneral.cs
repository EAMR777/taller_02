using System;
using System.Net.Mail;

namespace CapaRN
{
    public static class xgeneral
    {
        public static bool emailIsValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

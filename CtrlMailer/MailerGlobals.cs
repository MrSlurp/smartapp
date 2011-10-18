using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlMailer
{
    internal class MailerGlobals
    {
        public string SMTP_host = "smtp@gmail.com";
        public int SMTP_port = 0;
        public string userMail;
        public string userPassword;
        public bool useSSL = false;

        public bool IsConfigured()
        {
            if (!string.IsNullOrEmpty(SMTP_host)
                || SMTP_port != 0
                || !string.IsNullOrEmpty(userMail)
                || !string.IsNullOrEmpty(userPassword)
                || useSSL != false)
                return true;
            else
                return false;
        }
    }
}

namespace Program
{
    using System.Net.Mail;
    using System.Net;
    using System;

    class EmailServices
    {
        private readonly string SMTPserver;
        private readonly string SMTPusername;
        private readonly string SMTPpassword;
        private readonly string SMTPportTLS;
        private readonly string SMTPportSSL;
        private SmtpClient smtpClient;
        public EmailServices()
        {
            this.SMTPserver = ConfigReader.ReadConfig()["SMTPserver"];
            this.SMTPusername = ConfigReader.ReadConfig()["SMTPusername"];
            this.SMTPpassword = ConfigReader.ReadConfig()["SMTPpassword"];
            this.SMTPportTLS = ConfigReader.ReadConfig()["SMTPportTLS"];
            this.SMTPportSSL = ConfigReader.ReadConfig()["SMTPportSSL"];

            //Login to the SMTP server
            smtpClient = new SmtpClient(SMTPserver, int.Parse(SMTPportTLS));
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(SMTPusername, SMTPpassword);
            smtpClient.UseDefaultCredentials = false;
        }

        public void SendEmail(string destination, string subject, string body)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentException("E-mail de destino não pode ser vazio");
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("Assunto não pode ser vazio");
            if (string.IsNullOrEmpty(body))
                throw new ArgumentException("Corpo do e-mail não pode ser vazio");

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(this.SMTPusername);
                if (destination.Contains(",")) // Multiple destinations
                {
                    var tos = destination.Split(',');
                    foreach (var email in tos)
                        mail.To.Add(email);
                }
                else

                    mail.To.Add(destination);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                smtpClient.Send(mail);
                Console.WriteLine("E-mail enviado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete
{

    public class EmailSettings

    {
        public string MailToAddress = "orders@example.com";

        public string MailFromAddress = "bookshop@example.com";
        public bool UseSsl = true;

        public string Username = "MySmtpUsername";

        public string Password = "MySmtpPassword";

        public string ServerName = "smtp.example.com";

        public int ServerPort = 587;

        public bool WriteAsFile = true;

        public string FileLocation = @"C:\bsu\.net'\ASP\lab";
    }
       public  class EmailOrderProcessor : IOrderProcessor
    {

        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Entities.Cart cart, Entities.ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if(emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("The order has been processed")
                    .AppendLine("-------------------")
                    .AppendLine("products:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Book.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (sum: {2:c}", line.Quantity, line.Book.Title, subtotal);

                }

                body.AppendFormat("Total price: {0:c}", cart.ComputeTotalvalue())
                    .AppendLine("-------------------")
                    .AppendLine("Delivery")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Address)
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.Country)
                    .AppendLine("-------------------")
                    .AppendFormat("Courier delivery: {0}", shippingDetails.GiftWrap ? "yes" : "no");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "New order sent",
                    body.ToString()
                    );

                if(emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}

using SBMSData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SBMSAdmin.Utility
{
    public sealed class Emails
    {
        static Emails() { }
        public static Emails Instance { get; } = new Emails();

        public void BuildAndSendStoreRegistrationEmail(Store store)
        {
            var sb = new StringBuilder();
            sb.Append($"<p>Welcome, {store.ContactPerson}!</p>");
            sb.Append($"<p>Your store <b>{store.Name}</b> (full details below) has been successfully registered for membership on our network.<p>");
            sb.Append("<p><b>Store Details:</b><br />");
            sb.Append($"Name {store.Name}<br />");
            sb.Append($"Telephone {store.Telephone}<br />");
            sb.Append($"Email Address {store.EmailAddress}<br />");
            sb.Append($"Address {store.Address}<br />");
            sb.Append($"Contact Person {store.ContactPerson}<br />");
            sb.Append($"Contact Number {store.ContactNumber}<br />");
            sb.Append($"Terminals {store.Terminals}<br />");
            sb.Append($"Commission {store.Commission}%</p>");

            sb.Append($"<p>If this was you, congratulations, you can now click on the following link and we'll get you started:</p>");

            // TODO: get urls for release, testing uses localhost.
            sb.Append(String.Format(@"<p><a href='http://localhost:62692/Account/Register?action=first&sid={0}'>http://localhost:62692/Account/Register?a=1&sid={0}</a></p>", store.Id));
            sb.Append("<p>If this wasn't you, <a href='http://localhost:62692/Contact'>please email us</a> and let us know. Note that billing information has been shared anywhere.</p>");
            sb.Append("<p>Warm regards,<br />");

            // TODO: place signature "The x Team" or such.
            sb.Append("Signature");

            SendEmail(sb.ToString(), "You registered a store!", store.ContactPerson, store.EmailAddress);
        }

        private void SendEmail(string body, string subject, string contactPerson, string emailAddress)
        {
            var msg = new MailMessage();
            msg.Body = body;
            msg.To.Add(new MailAddress(emailAddress, contactPerson));
            msg.Subject = subject;
            // TODO: Change this!
            msg.From = new MailAddress("logan@euphoria.systems");


            using (SmtpClient smtpClient = new SmtpClient("u4riasrv09.u4ria.co.za", 25))
            {
                NetworkCredential nc = new NetworkCredential("shaun@u4riaemail.co.za", "shaun001");

                smtpClient.Credentials = nc;
                smtpClient.Send(msg);
            }
        }
    }
}
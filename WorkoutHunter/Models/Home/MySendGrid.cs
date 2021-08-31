using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;

namespace WorkoutHunterV2.Models.Home
{
    public class MySendGrid
    {
        public async Task SendEmail()
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ub1213gogo@gmail.com", "Example User");
            var subject = "Sending with Twilio SendGrid is Fun";
            var to = new EmailAddress("ub1213go@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}

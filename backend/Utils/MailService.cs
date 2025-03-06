using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ASPNET_API.Services
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }

    // Dịch vụ gửi mail
    public class MailService : IEmailSender
    {
        private readonly MailSettings mailSettings;

        private readonly ILogger<MailService> logger;

        // mailSetting được Inject qua dịch vụ hệ thống
        // Có inject Logger để xuất log
        public MailService(IOptions<MailSettings> _mailSettings, ILogger<MailService> _logger)
        {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendMailService");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);

                logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            logger.LogInformation("send mail to: " + email);

        }

		public async Task SendEmailsInBatchAsync(IEnumerable<string> emailList, string subject, string htmlMessage)
		{
			using var smtp = new MailKit.Net.Smtp.SmtpClient();
			try
			{
				smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
				smtp.Authenticate(mailSettings.Mail, mailSettings.Password);

				foreach (var email in emailList)
				{
					var message = new MimeMessage();
					message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
					message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
					message.To.Add(MailboxAddress.Parse(email));
					message.Subject = subject;

					var builder = new BodyBuilder();
					builder.HtmlBody = htmlMessage;
					message.Body = builder.ToMessageBody();

					await smtp.SendAsync(message);
					logger.LogInformation("Gửi mail thành công tới: " + email);
				}
			}
			catch (Exception ex)
			{
				logger.LogError("Lỗi trong quá trình gửi mail hàng loạt: " + ex.Message);
			}
			finally
			{
				smtp.Disconnect(true);
			}
		}

	}
}

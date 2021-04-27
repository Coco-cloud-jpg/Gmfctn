using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_;
using Data_.Entities;
using Data_.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Services
{
    
    public class MailService : IMailService
    {
        private SmtpClient Smtp = null;
        private IUnitOfWork UnitOfWork;
        private IConfiguration Config;
        public MailService(IUnitOfWork _UnitOfWork, IConfiguration _Config)
        {
            UnitOfWork = _UnitOfWork;
            Config = _Config;
        }
        public async Task SendRequest(string Email, CancellationToken Cancel)
        {
            var User = await UnitOfWork.UserRepository.DbSet.FirstOrDefaultAsync(User => User.Email == Email, Cancel);

            if (User == null)
                throw new ArgumentException();

            string Key = GenerateKey();
            var Request = new PasswordResetRequest
            {
                Id = new Guid(),
                Email = Email,
                Hash = Key
            };
            await UnitOfWork.PasswordResetRequestRepository.Create(Request, Cancel);
            
            SendMessage(User, Key);

            await UnitOfWork.SaveChangesAsync(Cancel);
        }

        private void  SendMessage(User User, string Key)
        {
            Smtp = GenerateSmtpClient();
            MailAddress From = new MailAddress(Config.GetSection("EmailSettings")["Mail"]);
            MailAddress To = new MailAddress(User.Email);
            MailMessage Message = new MailMessage(From, To);
            Message.Subject = "Password reset";
            Message.Body = $"Dear {User.FirstName} {User.LastName}, here is your key to reset password:\n\t{Key}\n";
            Smtp.Send(Message);
        }
        private string GenerateKey()
        {
            var RandomNumber = new byte[32];
            using (var Rng = RandomNumberGenerator.Create())
            {
                Rng.GetBytes(RandomNumber);
                return Convert.ToBase64String(RandomNumber);
            }
        }
        private SmtpClient GenerateSmtpClient()
        {
            return new SmtpClient
            {
                Host = Config.GetSection("EmailSettings")["Host"],
                Port = Convert.ToInt32(Config.GetSection("EmailSettings")["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(Config.GetSection("EmailSettings")["Mail"],
                    Config.GetSection("EmailSettings")["Pass"])
            };
        }

    }
}

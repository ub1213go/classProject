using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutHunterV2.Models.Home
{
    public class EmailWorker
    {
        public string UID { get; set; }
        public byte[] Key { get; set; }
        public string Com { get; set; }

        public string sendEmail { get; set; }
        public string sendPassword { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public string addressee { get; set; }

        public bool MailSend()
        {
            try
            {
                // ==================================
                // 信封
                MailMessage msg = new MailMessage();

                msg.To.Add(this.addressee); // 收件者

                msg.From = new MailAddress(this.sendEmail, "WorkoutHunter", Encoding.UTF8); // 寄件人

                // 信封內容
                msg.Subject = this.subject; // 標題
                msg.SubjectEncoding = Encoding.UTF8; // 編碼
                msg.Body = this.content;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                // ==================================
                // //////////////////////////////////
                // ==================================
                // Smpt寄信
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(this.sendEmail, this.sendPassword);
                client.Host = "smtp.gmail.com"; // 設定Smtp Server
                client.Port = 25; // 設定Port
                client.EnableSsl = true; // gmail預設開啟驗證
                client.Send(msg);
                client.Dispose();
                msg.Dispose();
                // ==================================
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return false;
            }
            return true;
        }

        // 混和Uid UID + Key 並用base64string呈現
        public bool mixUid()
        {
            if (this.UID == null)
            {
                return false;
            }

            byte[] uid = Encoding.ASCII.GetBytes(this.UID);
            byte[] com = new byte[uid.Length];
            byte[] key = new byte[uid.Length];
            Random R = new Random();
            // 產生byte[] key
            R.NextBytes(key);
            this.Key = key;

            // 產生byte[] com
            for (int i = 0 ; i < com.Length ; i++)
            {
                if (uid[i] + this.Key[i] < 256)
                    com[i] = Convert.ToByte(uid[i] + this.Key[i]);
                else
                    com[i] = Convert.ToByte(uid[i] + this.Key[i] - 256);
            }
            // 產生string com並放入
            this.Com = Convert.ToBase64String(com);

            return true;
        }
        // 分解 Com Com - Key 取得 Uid
        public bool disCom()
        {
            if (this.Key == null || this.Com == null)
            {
                return false;
            }
            byte[] com = Convert.FromBase64String(this.Com);
            byte[] uid = new byte[com.Length];

            for (int i = 0 ; i < com.Length ; i++)
            {
                if (com[i] > this.Key[i])
                    uid[i] = Convert.ToByte(com[i] - this.Key[i]);
                else
                    uid[i] = Convert.ToByte(com[i] - this.Key[i] + 256);
            }
            this.UID = Encoding.ASCII.GetString(uid);

            return true;
        }

        public string RondomSTR()
        {
            byte[] key = new byte[10];
            Random R = new Random();
            // 產生byte[] key
            R.NextBytes(key);
            Key = key;
            return Convert.ToBase64String(key);
        } 
    }


}

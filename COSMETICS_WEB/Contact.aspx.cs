using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // 1. Lấy thông tin từ web.config
                    string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                    int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                    string fromEmail = ConfigurationManager.AppSettings["SmtpUser"];
                    string fromEmailPassword = ConfigurationManager.AppSettings["SmtpPass"];

                    // 2. Lấy thông tin từ form
                    string senderName = txtName.Text.Trim();
                    string senderEmail = txtEmail.Text.Trim();
                    string subject = txtSubject.Text.Trim();
                    string message = txtMessage.Text.Trim();

                    // 3. Tạo đối tượng MailMessage (nội dung email)
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(fromEmail, "Website Skin For You"); // Tên hiển thị khi nhận mail
                    mailMessage.To.Add(fromEmail); // Gửi mail đến chính bạn

                    // Thêm email người gửi vào danh sách trả lời để bạn có thể bấm "Reply" trực tiếp
                    mailMessage.ReplyToList.Add(new MailAddress(senderEmail, senderName));

                    mailMessage.Subject = $"[Liên hệ] - {subject}";
                    mailMessage.Body = $@"
                        <h3>Bạn có tin nhắn mới từ website:</h3>
                        <p><strong>Họ tên:</strong> {senderName}</p>
                        <p><strong>Email:</strong> {senderEmail}</p>
                        <hr>
                        <p><strong>Nội dung:</strong></p>
                        <p>{message}</p>
                    ";
                    mailMessage.IsBodyHtml = true; // Cho phép nội dung email là HTML

                    // 4. Tạo đối tượng SmtpClient và gửi mail
                    var smtpClient = new SmtpClient(smtpHost, smtpPort);
                    smtpClient.EnableSsl = true; // Bắt buộc với Gmail
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);

                    smtpClient.Send(mailMessage);

                    // 5. Hiển thị thông báo thành công
                    lblStatusMessage.Text = "Cảm ơn bạn đã liên hệ! Chúng tôi đã nhận được tin nhắn.";
                    lblStatusMessage.CssClass = "status-message success"; // Thêm class success để có màu xanh
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có sự cố
                    lblStatusMessage.Text = "Đã có lỗi xảy ra. Vui lòng thử lại sau.";
                    lblStatusMessage.CssClass = "status-message error"; // Thêm class error để có màu đỏ
                    // (Trong dự án thực tế, bạn nên ghi lại lỗi này vào file log)
                    // File.AppendAllText(Server.MapPath("~/error_log.txt"), ex.ToString());
                }
            }
        }
    }
}
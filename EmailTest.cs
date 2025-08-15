using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Testing Email Configuration...");
            
            try
            {
                // Read email settings from App.config
                string smtpHost = ConfigurationManager.AppSettings["EmailSettings:SmtpHost"];
                string smtpPortStr = ConfigurationManager.AppSettings["EmailSettings:SmtpPort"];
                string smtpUser = ConfigurationManager.AppSettings["EmailSettings:SmtpUser"];
                string smtpPass = ConfigurationManager.AppSettings["EmailSettings:SmtpPass"];
                string fromEmail = ConfigurationManager.AppSettings["EmailSettings:FromEmail"];
                string fromName = ConfigurationManager.AppSettings["EmailSettings:FromName"];
                
                Console.WriteLine($"SMTP Host: {smtpHost}");
                Console.WriteLine($"SMTP Port: {smtpPortStr}");
                Console.WriteLine($"SMTP User: {smtpUser}");
                Console.WriteLine($"From Email: {fromEmail}");
                Console.WriteLine($"From Name: {fromName}");
                
                // Validate configuration
                if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpPortStr) || 
                    string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
                {
                    Console.WriteLine("ERROR: Missing email configuration settings!");
                    return;
                }
                
                if (!int.TryParse(smtpPortStr, out int smtpPort))
                {
                    Console.WriteLine($"ERROR: Invalid SMTP port: {smtpPortStr}");
                    return;
                }
                
                // Test email sending
                Console.WriteLine("\nTesting email sending...");
                
                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    
                    var message = new MailMessage
                    {
                        From = new MailAddress(fromEmail, fromName),
                        Subject = "ERP System Test Email",
                        Body = "This is a test email from the ERP system to verify email configuration.",
                        IsBodyHtml = false
                    };
                    
                    // Add test recipient (you can change this)
                    Console.Write("Enter test email address: ");
                    string testEmail = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(testEmail))
                    {
                        Console.WriteLine("No email address provided. Exiting.");
                        return;
                    }
                    
                    message.To.Add(testEmail);
                    
                    await client.SendMailAsync(message);
                    Console.WriteLine("✓ Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Email sending failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
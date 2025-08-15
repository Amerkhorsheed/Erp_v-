# PowerShell script to test email configuration
# This script tests the same email settings used by the ERP system

Write-Host "Testing ERP Email Configuration..." -ForegroundColor Green

# Email configuration (same as in App.config)
$smtpServer = "smtp.gmail.com"
$smtpPort = 587
$smtpUser = "ahmadkhorsheed2@gmail.com"
$smtpPass = "ltse cinn gunl iypf"
$fromEmail = "ERP@gmail.com"
$fromName = "ERP System"

# Get test recipient email
$toEmail = Read-Host "Enter test email address"

if ([string]::IsNullOrEmpty($toEmail)) {
    Write-Host "No email address provided. Exiting." -ForegroundColor Red
    exit
}

try {
    Write-Host "Configuring SMTP client..." -ForegroundColor Yellow
    
    # Create SMTP client
    $smtp = New-Object System.Net.Mail.SmtpClient($smtpServer, $smtpPort)
    $smtp.EnableSsl = $true
    $smtp.Credentials = New-Object System.Net.NetworkCredential($smtpUser, $smtpPass)
    
    # Create email message
    $message = New-Object System.Net.Mail.MailMessage
    $message.From = New-Object System.Net.Mail.MailAddress($fromEmail, $fromName)
    $message.To.Add($toEmail)
    $message.Subject = "ERP System Email Test"
    $message.Body = "<div style='font-family: Arial, sans-serif; line-height: 1.6;'><h2>ERP System Email Test</h2><p>This is a test email from the ERP system to verify email configuration.</p><p>If you receive this email, the email configuration is working correctly.</p><p>Test performed on: $(Get-Date)</p><p>Best regards,<br><b>ERP System</b></p></div>"
    $message.IsBodyHtml = $true
    
    Write-Host "Sending test email to: $toEmail" -ForegroundColor Yellow
    
    # Send email
    $smtp.Send($message)
    
    Write-Host "Email sent successfully!" -ForegroundColor Green
    Write-Host "Check the recipient inbox including spam folder." -ForegroundColor Cyan
    
} catch {
    Write-Host "Email sending failed!" -ForegroundColor Red
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    
    if ($_.Exception.InnerException) {
        Write-Host "Inner Exception: $($_.Exception.InnerException.Message)" -ForegroundColor Red
    }
    
    # Common troubleshooting suggestions
    Write-Host "Troubleshooting suggestions:" -ForegroundColor Yellow
    Write-Host "1. Check if Gmail account has 2-factor authentication enabled" -ForegroundColor White
    Write-Host "2. Verify the app password is correct" -ForegroundColor White
    Write-Host "3. Check if Less secure app access is enabled" -ForegroundColor White
    Write-Host "4. Verify internet connection and firewall settings" -ForegroundColor White
} finally {
    # Clean up
    if ($message) { $message.Dispose() }
    if ($smtp) { $smtp.Dispose() }
}

Write-Host "Press any key to exit..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
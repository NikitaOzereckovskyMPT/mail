using ImapX;
using MailApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MailApp.Page
{
    /// <summary>
    /// Логика взаимодействия для SendMessagePage.xaml
    /// </summary>
    public partial class SendMessagePage : UserControl
    {
        public SendMessagePage()
        {
            InitializeComponent();
            TextRange textRange = new(rtx.Document.ContentStart, rtx.Document.ContentEnd);
        }

        public SendMessagePage(Message message)
        {
            InitializeComponent();

            List<ImapX.MailAddress> mailAddresses = message.To;

            string ToAddress = "";

            foreach (var item in mailAddresses)
            {
                ToAddress += item.Address;
            }

            txt_to.Text = ToAddress;

            txt_thema.Text = message.Subject;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            rtx.SetSelectionBold();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_to.Text))
            {
                MessageBox.Show("Не указан получатель письма!", "Почтовый клиент", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var credentials = ImapHelper.GetCredentials();

            HtmlRtfConverter.ToHtml(new TextRange(rtx.Document.ContentStart, rtx.Document.ContentEnd));
            var text = File.ReadAllText("send.html");

            //для очистки
            File.Delete("send.html");

            MailMessage m = new MailMessage(credentials.Email, txt_to.Text, txt_thema.Text, text);
            m.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(credentials.SmtpHost);
            smtpClient.Credentials = new NetworkCredential(credentials.Email, credentials.Pass);
            smtpClient.EnableSsl = true;

            smtpClient.Send(m);

            MessageBox.Show("Сообщение успешно отправлено!", "Почтовый клиент", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            rtx.SetSelectionItalic();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            rtx.SetSelectionStrikethrough();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            rtx.SetSelectionFontSize(random.Next(12, 85));
        }
    }
}

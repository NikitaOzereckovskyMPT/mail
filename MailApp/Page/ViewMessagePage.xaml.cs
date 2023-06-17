using ImapX;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MailApp.Page
{

    public partial class ViewMessagePage : UserControl
    {
        public ViewMessagePage(Message message)
        {
            InitializeComponent();

            List<MailAddress> mailAddresses = message.To;

            string ToAddress = "";

            foreach (var item in mailAddresses)
            {
                ToAddress += item.Address + ", ";
            }

            txt_ToAddress.Text = ToAddress;

            txt_FromAddress.Text = message.From.Address;
            txt_Subject.Text = message.Subject;

            HtmlRtfConverter.ToRtf(message.Body.Html);

            //подобно тому, что было в лекции про RichTextBox
            var text = new TextRange(rtx.Document.ContentStart, rtx.Document.ContentEnd);
            FileStream fs = new FileStream("msg.rtf", FileMode.Open);
            text.Load(fs, DataFormats.Rtf);
            fs.Close();

            //для очистки
            File.Delete("msg.rtf");
        }
    }
}

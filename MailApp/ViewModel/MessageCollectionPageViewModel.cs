using ImapX;
using ImapX.Collections;
using MailApp.Model;
using MailApp.Page;
using System.Threading.Tasks;
using System.Windows;

namespace MailApp.ViewModel
{
    public class MessageCollectionPageViewModel : BaseModel
    {
        private MessageCollection _messages;

        public MessageCollection Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private Visibility _isBusy;
        public Visibility IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private async void LoadMessages(string folder)
        {
            IsBusy = Visibility.Visible;
            await Task.Run(() =>
            {
                Messages = ImapHelper.GetMessagesForFolder(folder);
            });
            IsBusy = Visibility.Collapsed;
        }


        public MessageCollectionPageViewModel(string folder, MainWindowViewModel window)
        {
           LoadMessages(folder);
        }
    }
}

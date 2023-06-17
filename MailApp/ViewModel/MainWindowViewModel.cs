using ImapX;
using ImapX.Collections;
using MailApp.Model;
using MailApp.Page;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MailApp.ViewModel
{
    public class MainWindowViewModel : BaseModel
    {

        private CommonFolderCollection _folders;

        public CommonFolderCollection Folders
        {
            get { return _folders; }
            set { 
                _folders = value;
                OnPropertyChanged(nameof(Folders));
            }
        }

        private UserControl _currentPage;

        public UserControl CurrentPage
        {
            get { return _currentPage; }
            set { 
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private MessageCollectionPage messageCollectionPage;

        public MainWindowViewModel()
        {
            Folders = ImapHelper.GetFolders();
        }

        private Message _selectedMessage;

        public Message SelectedMessage
        {
            get { return _selectedMessage; }
            set { 
                _selectedMessage = value;
                OnPropertyChanged(nameof(SelectedMessage));
            }
        }


        public RelayCommand MessagesPage => new RelayCommand((obj) =>
        {
            string folder = obj as string;

            MessageCollectionPage messageCollection = new MessageCollectionPage();
            MessageCollectionPageViewModel messageCollectionPageViewModel = new MessageCollectionPageViewModel(folder, this);
            messageCollection.DataContext = messageCollectionPageViewModel;

            messageCollectionPage = messageCollection;
            CurrentPage = messageCollectionPage;
        });

        public RelayCommand ViewMessage => new RelayCommand((obj) =>
        {
            var message = obj as Message;
            SelectedMessage = message;
            ViewMessagePage viewMessagePage = new ViewMessagePage(message);
            viewMessagePage.DataContext = this;

            CurrentPage = viewMessagePage;
        });

        public RelayCommand SendMessagePage => new RelayCommand((obj) =>
        {
            SendMessagePage sendMessagePage = new SendMessagePage();
            CurrentPage = sendMessagePage;
        });

        public RelayCommand Back => new RelayCommand((obj) =>
        {
            CurrentPage = messageCollectionPage;
        });

        public RelayCommand ReplyMessage => new RelayCommand((obj) =>
        {
            var message = obj as Message;
            SendMessagePage sendMessagePage = new SendMessagePage(message);
            CurrentPage = sendMessagePage;
        });
    }
}

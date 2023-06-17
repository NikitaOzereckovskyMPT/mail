namespace MailApp.Model
{
    public class LoggedUser : BaseModel
    {
        private string email;
        private string pass;
        private string smtpHost;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                OnPropertyChanged(nameof(Pass));
            }
        }
        public string SmtpHost
        {
            get => smtpHost;
            set
            {
                smtpHost = value;
                OnPropertyChanged(nameof(SmtpHost));
            }
        }
    }
}

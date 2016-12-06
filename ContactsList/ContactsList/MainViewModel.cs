using System.Collections.ObjectModel;
using System.Linq;

namespace ContactsList
{
    public class MainViewModel : ViewModelBase
    {     
        public ObservableCollection<Contact> ContactList { get; private set; }
        public ObservableCollection<Conversation> ConversationList { get; private set; }
        private Contact selectedContact;
        public Contact SelectedContact
        {
            get
            {
                return selectedContact;
            }

            set
            {
                if (selectedContact != value)
                {
                    selectedContact = value; 
                    PopulateConversationList();
                    RaisePropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            ContactList = new ObservableCollection<Contact>(cbdc.Contacts);
            ConversationList = new ObservableCollection<Conversation>();
            SelectedContact = ContactList.FirstOrDefault();
        }

        public void PopulateConversationList()
        {
            ConversationList.Clear();
            if (selectedContact != null)
            {
                /*
                var conversations = from c in cbdc.Conversations
                                    where c.ContactID == SelectedContact.Id
                                    select c;
                */
                var conversations = cbdc.GetConversations(selectedContact);

                foreach (Conversation c in conversations)
                    ConversationList.Add(c);
            }
        }
    }
}

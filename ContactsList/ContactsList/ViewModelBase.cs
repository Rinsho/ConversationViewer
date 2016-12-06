using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsList
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected MockDB cbdc;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
            cbdc = new MockDB();
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class MockDB
    {
        private Dictionary<Contact, List<Conversation>> conversations;
        public List<Contact> Contacts { get; private set; }

        public MockDB()
        {
            Contacts = CreateContacts();
            conversations = CreateConversations(Contacts);
        }

        public List<Conversation> GetConversations(Contact c) =>
            conversations[c];

        private List<Contact> CreateContacts()
        {
            return new List<Contact>()
            {
                new Contact("Jim"),
                new Contact("Bob"),
                new Contact("Mary")
            };
        }

        private Dictionary<Contact, List<Conversation>> CreateConversations(List<Contact> contacts)
        {
            Dictionary<Contact, List<Conversation>> conversationMap = new Dictionary<Contact, List<Conversation>>();
            foreach (Contact c in contacts)
                conversationMap.Add(c, CreateConversationList(c));
            return conversationMap;
        }
        private List<Conversation> CreateConversationList(Contact contact)
        {
            return new List<Conversation>()
            {
                new Conversation($"Hi {contact.Name}!"),
                new Conversation("Pleasant day today.")
            };
        }
    }

    public class Contact
    {
        public string Name { get; private set; }

        public Contact(string contactName)
        {
            Name = contactName;
        }
    }

    public class Conversation
    {
        public string Title { get; private set; }

        public Conversation(string conversationText)
        {
            Title = conversationText;
        }
    }
}

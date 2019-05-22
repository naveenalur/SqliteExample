using System;

namespace SqlLiteSimple.Model
{
    public class ContactList
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public long Number { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public string RecivedData { get; set; }

        public ContactList()
        {
        }

        public ContactList(string name, long phone_no,string recievedData)
        {
            Name = name;
            Number = phone_no;
            RecivedData = recievedData;
            CreationDate = DateTime.Now.ToString();
            UpdateDate = null;
        }
    }
}
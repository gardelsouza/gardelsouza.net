using System;

namespace api.Models
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public bool Viewed { get; set; }
    }
}

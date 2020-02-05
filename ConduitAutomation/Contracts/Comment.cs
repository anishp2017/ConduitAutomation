using System;
using System.Collections.Generic;
using System.Text;

namespace ConduitAutomation.Contracts
{
    public class Comment
    {
        public string Body { get; set; }

        public Person Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

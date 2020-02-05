using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConduitAutomation.Contracts
{
    public class Article
    {
        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }

        public List<Comment> Comments { get; set; }

        public bool Favorited { get; set; }

        public int FavoritesCount { get; set; }

        public List<string> TagList { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

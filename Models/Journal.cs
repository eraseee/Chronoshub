﻿namespace Chronoshub.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Article> Articles { get; set; }
        
    }
}

﻿using System.Collections.Generic;

namespace MiBot.Entities
{
    public class Post
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Url { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MiBot.Entities;
using Microsoft.Bot.Builder.FormFlow.Advanced;

namespace MiBot.Internal
{
    public class BlogSearch
    {
        const string BLOG_URI = "http://ankitbko.github.io/archive";

        public List<Post> GetPostsWithTag(string tag)
        {
            var posts = GetAllPosts();
            return posts.FindAll(p => IsMatch(tag, p.Tags));
        }

        public List<Post> GetAllPosts()
        {
            var blogHtml = GetHtmlFromBlog();
            return blogHtml.DocumentNode.SelectSingleNode("//ul").ChildNodes.Where(t => t.Name == "li")     // Select all posts
                .Select((f, n) =>
                    new Post()
                    {
                        Name = f.SelectSingleNode("a").InnerText,                                           // Select post name
                        Tags = f.SelectSingleNode("ul").ChildNodes.Where(t => t.Name == "li")               // Select Tags
                            .Select(t => t.InnerText).ToList(),                                         // Select tag name
                        Url = f.SelectSingleNode("a[@href]").GetAttributeValue("href", string.Empty) //Select URL
                    }).ToList();

        }

        private static HtmlDocument GetHtmlFromBlog()
        {
            var web = new HtmlWeb();
            return web.Load(BLOG_URI);
        }

        private static bool IsMatch(string tag, IEnumerable<string> tags)
        {
            var terms = tags.SelectMany(t => Language.GenerateTerms(Language.CamelCase(t), 3));
            return terms.Any(t => Regex.IsMatch(tag, t));
        }
    }
}
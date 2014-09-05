#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Somedave.Views.Blog.Posts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using FluentBootstrap;
    using Somedave;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Blog/Posts/new-blog.cshtml")]
    public partial class new_blog : Somedave.BlogPostViewPage<dynamic>
    {
        public new_blog()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Blog\Posts\new-blog.cshtml"
  
    Title = "New Blog";
    Lead = "Look, Ma, no database!";
    Published = new DateTime(2014, 9, 5);
    Tags = new[] { "blog", "meta" };

            
            #line default
            #line hidden
WriteLiteral(@"

<p>It's been a little while coming, but I'm finally launching my new blog. It's built from scratch in ASP.NET MVC. Why go to all the trouble to build a blog engine from scratch when there are a gazillion great engines already out there? That's a very good question, let me break it down for you.</p>

<h1>Own Your Content</h1>
<p>I'll thank Scott Hanselman <a");

WriteLiteral(" href=\"http://www.hanselman.com/blog/YourWordsAreWasted.aspx\"");

WriteLiteral(">for really driving this point home</a>. There\'s a lot to be said for owning your" +
" entire infrastructure from code to database to content. I\'ve blogged on other p" +
"latforms like WordPress and TypePad, but I generally become dissatisfied when I " +
"want to customize things. Want to tweak the CSS? That\'ll be an extra fee. Want t" +
"o add your Twitter feed? That\'ll be an extra add-on to buy or install. How about" +
" some ads? Of course we\'ll serve ads, and we\'ll keep the revenue too. Etc.</p>\r\n" +
"<p>Now this doesn\'t necessarily mean that you should go out and roll an entire b" +
"log engine. In many cases it\'s enough just to get a domain, install one of the m" +
"any excellent blog engines that are out there, and use that. At least you\'ll own" +
" the stack and you won\'t be riding on someone else\'s infrastructure.</p>\r\n\r\n<h1>" +
"A Learning Exercise</h1>\r\n<p>Remember the days when a blog was just a bunch of s" +
"tatic HTML pages hosted on GeoCities? Nowadays social sites need to implement a " +
"variety of features and protocols to be relevant. Comments (including moderation" +
"), RSS feeds, social links, etc. all go in to making a modern blog. I was curiou" +
"s how all of this worked under the hood, so I decided to put my head down and fi" +
"gure it out the best way there is - by doing. Which also means that this blog is" +
" built for me. It\'s not riding on a generic blogging platform and there are prob" +
"ably many ways that I\'ve architected things that wouldn\'t work for other blogs. " +
"That said, I\'d love it if some of what I\'ve build could be re-used.</p>\r\n\r\n<h1>O" +
"pen Source</h1>\r\n<p>I was originally <a");

WriteLiteral(" href=\"http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/\"");

WriteLiteral(@">inspired by Phil Haack to open source my blog</a>. Since then I've seen several other people do this and I love the idea. It lets the blogging community learn from the collective ideas of all the other bloggers out there. See a feature on a blog that you think is cool? Just hop over to the repository and see how it works. To that end, <strong><a");

WriteLiteral(" href=\"https://github.com/somedave/Somedave\"");

WriteLiteral(@">this blog is available on GitHub</a></strong>. Clone me, fork me, go nuts.</p>

<hr />

<p>So that's <em>why</em> I created this new blog, but what about <em>how</em> it works?</p>

<h1>Database, Or Lack Thereof</h1>
<p>The most obvious backend capability of any blog is the storage and retrieval of blog articles. Most blogs have an archive page, a summary or recent articles, tags, etc. and while you could conceivably rig all that up manually it would get unwieldy very quickly, which is where a database comes into play. I've been intrigued by the file-based database concept used by some recent blogging/content/static site engines such as <a");

WriteLiteral(" href=\"http://jekyllrb.com/\"");

WriteLiteral(@">Jekyll</a>. The only thing I don't like about many of those tools is the way that the content is separate from the code. I am obsessed with eliminating magic strings and strong-typing everything. The notion that a blog article would like to another blog article using a plain-text <code>a</code> tag keeps me up at night. What if you wanted to change the link? What if you deleted the post? What I wanted was a way to create my blog database from the file system but still maintain all the power and flexibility that my code gives me.</p>
<p>To satisfy this, I wrote a little library I'm calling ");

            
            #line 26 "..\..\Views\Blog\Posts\new-blog.cshtml"
                                                    Write(Html.ActionLink("RazorDatabase", MVC.Projects.RazorDatabase()));

            
            #line default
            #line hidden
WriteLiteral(" (by the way, if you look in the code for this blog post, that last link looks li" +
"ke <code>");

WriteLiteral(@"@Html.ActionLink(""RazorDatabase"", MVC.Projects.RazorDatabase())</code> - see what I mean about strong links?). RazorDatabase takes all of the Razor views that are derived from a specified <code>WebViewPage</code> class, renders them at compile time, and stores the rendered content and any view properties in an in-memory collection (with caching to disk for quick loading on site startup). I'll write more about RazorDatabase in a future blog post once I've gotten it closer to general availability.</p>

<h1>Writing Posts</h1>
<p>Because the blog posts are just Razor views similar to any other, the easiest way to write them is from within Visual Studio. I could write them externally, but they'd still have to be included in the project and compiled with the site to get the compile-time rendering to work correctly. This suits me just fine - while I've enjoyed Windows Live Writer in the past, I'm also quite comfortable with HTML and don't mind writing up my posts from my IDE.</p>

<h1>Other Content</h1>
<p>Other non-blog pages on the site like ");

            
            #line 32 "..\..\Views\Blog\Posts\new-blog.cshtml"
                                    Write(Html.ActionLink("About", MVC.Home.About()));

            
            #line default
            #line hidden
WriteLiteral(" and ");

            
            #line 32 "..\..\Views\Blog\Posts\new-blog.cshtml"
                                                                                    Write(Html.ActionLink("Likes", MVC.Home.Likes()));

            
            #line default
            #line hidden
WriteLiteral(@" are just regular ASP.NET MVC actions and views. Because the blog posts are also just actions and views (albeit ones that get loaded into an in-memory collection), the two can reference each other without problems. I really like the way I don't have to worry about whether I'm on a content page or a blog post - all the Razor syntax and code I'm used to using is available in either place.</p>

<h1>T4MVC</h1>
<p>Did I mention I hate magic strings? <a");

WriteLiteral(" href=\"http://t4mvc.codeplex.com/\"");

WriteLiteral(@">T4MVC</a> is one of my all-time favorite libraries. It provides strongly typed HTML helpers and exposes other bits of code to help eliminate the magic string problem. I highly recommend you check it out if you're not already familiar with it.</p>

<h1>Deployment</h1>
<p>This site is automatically deployed on <a");

WriteLiteral(" href=\"http://azure.microsoft.com/en-us/services/websites/\"");

WriteLiteral(@">Azure Websites</a> every time I commit to the master branch in the repository. I love the way this works. No publish process, no uploading - just code, push, and be done.</p>

<hr />

<p>Now that I've got full control of my platform and I've spent all this time creating it, <a");

WriteLiteral(" href=\"http://www.hanselman.com/blog/YourBlogIsTheEngineOfCommunity.aspx\"");

WriteLiteral(">I guess I\'m out of excuses for not blogging</a>. Expect to see more frequent pos" +
"ts from me going forward (or at least that\'s the plan).</p>\r\n");

        }
    }
}
#pragma warning restore 1591
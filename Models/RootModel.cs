namespace RssFeedDashbaord.Models
{
   


    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class Committer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class Metadata
    {
        public Author Author { get; set; }
        public Committer Committer { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }

    public class File
    {
        public string Sha { get; set; }
        public string Filename { get; set; }
        public string Status { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int Changes { get; set; }
        public string BlobUrl { get; set; }
        public string RawUrl { get; set; }
        public string ContentsUrl { get; set; }
        public string Patch { get; set; }
        public object Summary { get; set; }
        public object Title { get; set; }
    }

    public class Commit
    {
        public string Sha { get; set; }
        public Metadata Metadata { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public List<File> Files { get; set; }
    }

    public class Root
    {
        public Commit Commit { get; set; }
        public string HtmlUrl { get; set; }
        public string PageTitle { get; set; }
        public string Title { get; set; }
        public object ChangesText { get; set; }
        public List<SmartChange> SmartChanges { get; set; }
    }
    public class SmartChange
    {
        public string Summary { get; set; }
        public string Title { get; set; }
    }
}
// Comment.cs
public class Comment
{
    private string commenterName;
    private string text;

    public Comment(string commenterName, string text)
    {
        this.commenterName = commenterName;
        this.text = text;
    }

    public string CommenterName => commenterName;
    public string Text => text;
}

// Video.cs
public class Video
{
    private string title;
    private string author;
    private int length;
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        this.title = title;
        this.author = author;
        this.length = length;
        comments = new List<Comment>();
    }

    public string Title => title;
    public string Author => author;
    public int Length => length;

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

// Program.cs
class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("How to Program", "Alice", 300);
        video1.AddComment(new Comment("John", "Great video!"));
        video1.AddComment(new Comment("Sarah", "Very informative."));
        video1.AddComment(new Comment("Mike", "Thanks for sharing!"));

        videos.Add(video1);

        Video video2 = new Video("Cooking 101", "Bob", 600);
        video2.AddComment(new Comment("Anna", "Yummy recipes."));
        video2.AddComment(new Comment("Tom", "I love cooking!"));

        videos.Add(video2);

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"Comment by {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}

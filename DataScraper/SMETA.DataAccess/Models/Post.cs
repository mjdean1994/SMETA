using Tweetinvi.Models;

namespace SMETA.DataAccess.Models
{
    public class Post
    {
        public Post(ITweet tweet)
        {
            DisplayName = tweet.CreatedBy.Name;
            Username = tweet.CreatedBy.ScreenName;
            Text = tweet.Text;
        }

        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
    }
}

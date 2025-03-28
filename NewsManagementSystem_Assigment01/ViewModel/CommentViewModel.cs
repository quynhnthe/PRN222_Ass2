namespace NewsManagementSystem_Assigment01.ViewModel
{
    public class CommentViewModel
    {
        public string NewsArticleId { get; set; } = null!;
        public short UserId { get; set; }
        public string Content { get; set; } = null!;
    }

}

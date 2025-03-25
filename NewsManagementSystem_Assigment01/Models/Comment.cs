using System;
using System.Collections.Generic;

namespace NewsManagementSystem_Assigment01.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string NewsArticleId { get; set; } = null!;

    public short UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual NewsArticle NewsArticle { get; set; } = null!;

    public virtual SystemAccount User { get; set; } = null!;
}

namespace ebay.domain.Entities;

public class RatingEntity
{
    public int Id { get; private set; }

    public int RaterId { get; private set; }
    public int RatedUserId { get; private set; }
    public int RatingScore { get; private set; }
    public string? Comment { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public bool? Deleted { get; private set; }

    public RatingEntity(int raterId, int ratedUserId, int score, string? comment)
    {
        RaterId = raterId;
        RatedUserId = ratedUserId;
        RatingScore = score;
        Comment = comment;
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public void UpdateComment(string? comment)
    {
        Comment = comment;
    }

    public void SoftDelete()
    {
        Deleted = true;
    }
}

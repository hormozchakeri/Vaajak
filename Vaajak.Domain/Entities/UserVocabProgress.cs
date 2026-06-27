namespace Vaajak.Domain.Entities;

public class UserVocabProgress
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid VocabId { get; set; }
    public Guid PackageId { get; set; }

    // Review history
    public string LastRating { get; set; } = string.Empty; // "Hard", "Good", "Easy"
    public int ReviewCount { get; set; }
    public DateTime LastReviewedAt { get; set; }

    // SM-2 spaced repetition fields
    public int Interval { get; set; }          // days until next review
    public int Repetitions { get; set; }       // consecutive successful reviews
    public double EaseFactor { get; set; } = 2.5;
    public DateTime? NextReviewAt { get; set; } // null = new card, never reviewed

    public Vocab Vocab { get; set; } = null!;
}

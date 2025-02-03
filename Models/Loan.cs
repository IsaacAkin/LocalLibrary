public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookIsbn { get; set; }
    public DateOnly DueDate { get; set; }
}
namespace Concurrency.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Phone { get; set; }
        public DateTime? Birthday { get; set; }

        public byte[] RowVersion { get; set; }
    }
}

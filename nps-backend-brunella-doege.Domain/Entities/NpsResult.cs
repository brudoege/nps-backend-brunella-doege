namespace nps_backend_brunella_doege.Domain.Entities
{
    public class NpsResult : IEntity
    {
        public Guid Id { get; set; }
        public Guid SystemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Comments { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
    }
}
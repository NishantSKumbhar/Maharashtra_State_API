namespace PatanWalks.Models.Domain
{
    public class Division
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? DivisionImageUrl { get; set; }          // Nullable
    }
}

namespace ChatSystem_Domain.Model.Base
{
    class Base_E
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; } 
    }
}

namespace YEG.Core.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }

        public BaseEntity()
        {
            
        }

        public BaseEntity(Guid id) : this()
        {
            Id = id;
        }
    }
}

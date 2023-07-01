namespace YEG.Core.Domain.Entities.Security
{
    public class OperationClaim : BaseEntity
    {
        public string Name { get; set; }
        public OperationClaim()
        {
        }
        public OperationClaim(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}

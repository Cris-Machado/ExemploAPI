namespace PointNow.API.Domain.Services
{
    public class User
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; } = string.Empty;
        public virtual string Email { get; protected set; } = string.Empty;
        public virtual bool EmailConfirmed { get; protected set; }
        public virtual string Password { get; protected set; } = string.Empty;
        public virtual string PhoneNumber { get; protected set; } = string.Empty;
    }
}

namespace DemoContact.Infrastructure
{
    public class SessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext!.Session;
        }

        public int? UserId 
        { 
            get { return _session.GetInt32(nameof(UserId)); }
            set 
            {
                if (!value.HasValue)
                    return;

                _session.SetInt32(nameof(UserId), value.Value);
            } 
        }

        public bool IsValid()
        {
            return _session.Keys.Contains(nameof(UserId));
        }

        public void Abandon()
        {
            _session.Clear();
        }
    }
}

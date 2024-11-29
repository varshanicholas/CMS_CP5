namespace CMS.Repository
{
    public interface ILoginRepository
    {
        public Task<(bool isValid, int roleId, int staffId)> ValidateLoginAsync(string username, string password);
    }
}

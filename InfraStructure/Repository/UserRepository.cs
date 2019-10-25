using System.Linq;
using Data;
using Data.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace InfraStructure.Repository
{
    public interface IUserRepository : IBaseRespository<User>
    {
        bool VerifyUser(string username, string password);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(CableIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public bool VerifyUser(string username, string password)
        {
            var user = DbSet.AsNoTracking().FirstOrDefault(c => c.Email == username);
            if (user == null) return false;

            return Crypto.VerifyHashedPassword(user.PasswordHash, password);
        }
    }

}

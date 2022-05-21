

using API.Data;
using BLL.Modals;

namespace API.Repository
{
    public class MemberRepository : Interface.IMembers
    {
        readonly AppDbContext _dbContext = new();

        public MemberRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Member> GetMembers()
        {
            try
            {
                return _dbContext.Members.ToList();
            }
            catch
            {
                throw;
            }
        }
        public void AddMember(Member member)
        {
            try
            {
                _dbContext.Members.Add(member);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Member DeleteMember(int id)
        {
            try
            {
                var member = _dbContext.Members.Find(id);

                if (member != null)
                {
                    _dbContext.Members.Remove(member);
                    _dbContext.SaveChanges();
                    return member;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public Member GetMemberDetails(int id)
        {
            try
            {
                var member = _dbContext.Members.Find(id);
                if (member != null)
                {
                    return member;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMember(Member member)
        {
            try
            {
                _dbContext.Update(member);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public bool IsExists(int id)
        {
            try
            {
                return _dbContext.Members.Any(m => m.ID == id);
            }
            catch
            {
                throw;
            }
        }
    }
}

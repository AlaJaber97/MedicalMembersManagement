namespace API.Repository.Interface
{
    public interface IMembers
    {
        public List<BLL.Modals.Member> GetMembers();
        public BLL.Modals.Member GetMemberDetails(int id);
        public void AddMember(BLL.Modals.Member member);
        public void UpdateMember(BLL.Modals.Member member);
        public BLL.Modals.Member DeleteMember(int id);
        public bool IsExists(int id);
    }
}

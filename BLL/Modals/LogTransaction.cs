
namespace BLL.Modals
{
    public class LogTransaction
    {
        public Guid ID { get; set; }
        public string Action { get; set; }
        public DateTime ActionAt { get; set; }
    }
}

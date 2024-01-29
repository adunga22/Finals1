using WebApplication1.Data.Models.Base;

namespace WebApplication1.Data.Models
{
    public class Order : EntityBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ProducName { get; set; }
    }
}
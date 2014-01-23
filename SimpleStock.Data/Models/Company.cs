using System.Collections.Generic;
namespace SimpleStock.Data.Models
{
    public class Company
    {
        public Company()
        {
            Users = new HashSet<User>();
            Stores = new HashSet<Store>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}

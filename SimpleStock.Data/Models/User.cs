using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleStock.Data.Security;

namespace SimpleStock.Data.Models
{
    public class User
    {
        public User()
        {
            Stores = new HashSet<Store>();
        }
    
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }

		[Required]
		public byte[] Salt { get; private set; }

		[Required, DataType(DataType.Password)]
		public byte[] PasswordHash { get; private set; }

		[NotMapped]
		public string Password
		{
			set
			{
				Salt = SaltedHash.GenerateSalt();
				PasswordHash = SaltedHash.GenerateSaltedHash(value, this.Salt);
			}
		}

		public bool IsPasswordValid(string plaintextPassword)
		{
			if (Email == null)			
				return false;
			

			if (Salt == null || PasswordHash == null)
				return false;

			return SaltedHash.IsPasswordValid(plaintextPassword, Salt, PasswordHash);
		}
    
        public virtual Company Company { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}

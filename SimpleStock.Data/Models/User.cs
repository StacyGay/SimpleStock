using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using SimpleStock.Data.Security;

namespace SimpleStock.Data.Models
{
	[DataContract]
    public class User
    {
        public User()
        {
            Stores = new HashSet<Store>();
        }
    
		[DataMember]
        public int Id { get; set; }
		[DataMember]
        public string Email { get; set; }
		[DataMember]
        public string FirstName { get; set; }
		[DataMember]
        public string LastName { get; set; }
		[DataMember]
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

		[DataMember]
        public virtual Company Company { get; set; }
		[DataMember]
        public virtual ICollection<Store> Stores { get; set; }
    }
}

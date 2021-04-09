using System;

namespace Customer
{
    public class CustomerModel : IEquatable<CustomerModel>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PropertyType { get; set; }

        public string Project { get; set; }

        public String StartDate { get; set; }

        public string Phone { get; set; }

        public bool Equals(CustomerModel other)
        {
            if (other is null){
                return false;
            }
            
            return this.Phone == other.Phone;
        }

        public override bool Equals(object obj) => Equals(obj as CustomerModel);
        public override int GetHashCode() => Phone.GetHashCode();
    }
}

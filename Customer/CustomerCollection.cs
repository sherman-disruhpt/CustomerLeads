using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Customer
{
    public class CustomerCollection : IEnumerable<CustomerModel>
    {
        private readonly List<CustomerModel> _customers = new List<CustomerModel>();

        public int Count => _customers.Count;

        public void Add(CustomerModel customers)
        {
            _customers.Add(customers);
        }

        public void AddRange(IEnumerable<CustomerModel> customers)
        {
            _customers.AddRange(customers);
        }

        public IEnumerator<CustomerModel> GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_customers).GetEnumerator();
        }

        public IEnumerable<CustomerModel> Sort(CustomerPredicate predicate)
        {
            switch (predicate)
            {
                case CustomerPredicate.SortByProject:
                    return this.SortByProject();
                case CustomerPredicate.SortByLastName:
                    return this.SortByLastName();
                case CustomerPredicate.SortByPropertyType:
                    return this.SortByPropertyType();
                case CustomerPredicate.SortByPropertyTypeThenProject:
                    return this.SortByPropertyTypeThenProject();
                case CustomerPredicate.SortByStartDate:
                    return this.SortByStartDate();
                default:
                    return this._customers;
            }
        }

        private IEnumerable<CustomerModel> SortByPropertyTypeThenProject()
        {
            return from c in this._customers
                   orderby c.PropertyType, c.Project
                   select c;
        }

        private IEnumerable<CustomerModel> SortByPropertyType()
        {
            return from c in this._customers
                   orderby c.PropertyType
                   select c;
        }

        private IEnumerable<CustomerModel> SortByStartDate()
        {
            return from c in this._customers
                   orderby c.StartDate
                   select c;
        }

        private IEnumerable<CustomerModel> SortByLastName()
        {
            return from c in this._customers
                   orderby c.LastName descending
                   select c;
        }

        private IEnumerable<CustomerModel> SortByProject()
        {
            return from c in this._customers
                   orderby c.Project
                   select c;
        }

    }
}
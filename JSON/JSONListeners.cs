using System;
using System.Collections.Generic;
using System.Text;

using MicroLite.Listeners;

namespace JSON
{
    // Create a listener to capture the projection values

    public class CustomerListener : Listener
    {
        public override void BeforeInsert(object instance)
        {
            var customer = instance as Customer;

            if (customer != null)
            {
                this.Project(customer);
            }
        }

        public override void BeforeUpdate(object instance)
        {
            var customer = instance as Customer;

            if (customer != null)
            {
                this.Project(customer);
            }
        }

        private void Project(Customer customer)
        {
            customer.GivenName = customer.Data.GivenName;
            customer.FamilyName = customer.Data.FamilyName;
            customer.PostCode = customer.Data.Address.PostCode;
        }
    }
}

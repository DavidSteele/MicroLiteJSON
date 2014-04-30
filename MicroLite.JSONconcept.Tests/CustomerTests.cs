using System;
using System.Collections.Generic;
using System.Text;

using MicroLite.TypeConverters;
using MicroLite.Listeners;

using MicroLite.Core;

using Moq;
using Xunit;

namespace MicroLite.JSONconcept.Tests
{
    public class TestHarness
    {
        public void Test()
        {
            // Register a custom implementation of the JSONTypeConverter for each business object aggregate root (e.g. the top level object)

            TypeConverter.Converters.Add(new JSONTypeConverter<CustomerData>());


            // Register each listener

            Listener.Listeners.Add(new CustomerListener());
        }
    }

    public class CustomerTests
    {

        [MicroLite.Mapping.Table("Customers")]
        private class Customer
        {
            public Customer()
            {
            }

            [MicroLite.Mapping.Column("CustomerId")]
            [MicroLite.Mapping.Identifier(MicroLite.Mapping.IdentifierStrategy.DbGenerated)]
            public int Id
            {
                get;
                set;
            }

            [MicroLite.Mapping.Column("GivenName")]
            public string GivenName
            {
                get;
                set;
            }

            [MicroLite.Mapping.Column("FamilyName")]
            public string FamilyName
            {
                get;
                set;
            }

            [MicroLite.Mapping.Column("PostCode")]
            public string PostCode
            {
                get;
                set;
            }

            [MicroLite.Mapping.Column("Data")]
            public CustomerData Data
            {
                get;
                set;
            }
        }

    }

}

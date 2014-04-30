using System;
using System.Collections.Generic;
using System.Text;

using MicroLite.Mapping;

namespace JSON
{
    [Table(schema: "dbo", name: "JSONCustomers")]
    public class CustomerRaw
    {
/*
        [Identifier(MicroLite.Mapping.IdentifierStrategy.DbGenerated)]
        [Column("CustomerID")]
        public int Id { get; set; }
*/

        [Identifier(MicroLite.Mapping.IdentifierStrategy.Assigned)]
        [Column("JSONData")]
        public string Data { get; set; }
    }

    [Table(schema: "dbo", name: "JSONCustomers")]
    public class Customer
    {
        [Identifier(MicroLite.Mapping.IdentifierStrategy.DbGenerated)]
        [Column("CustomerID")]
        public int Id { get; set; }

        [Column("JSONData")]
        public CustomerData Data { get; set; }

        // These properties are for query purposes only and are a projection copy of the values from JSONData
        // They only exist to support queries by anything other than CustomerId
        [Column("GivenName")]
        public string GivenName { get; set; }
        [Column("FamilyName")]
        public string FamilyName { get; set; }
        [Column("PostCode")]
        public string PostCode { get; set; }
    }

    public class CustomerData
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string TownCity { get; set; }
        public string PostCode { get; set; }
    }
}

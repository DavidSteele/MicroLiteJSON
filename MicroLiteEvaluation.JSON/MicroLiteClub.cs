using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MicroLite.Mapping;

namespace MicroLiteEvaluation
{
    public enum MicroLiteStatus
    {
        New = 0,
        Active = 1,
        Retired = 2
    }

    [Table(schema: "dbo", name: "Clubs")]
    public class MicroLiteClub
    {
        [Identifier(MicroLite.Mapping.IdentifierStrategy.DbGenerated)]
        [Column("ClubID")] // need this for Delete but not Insert !!!
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Postcode")]
        public string Postcode { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Telephone")]
        public string Telephone { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }

        [Column("StatusID")]
        public MicroLiteStatus Status { get; set; }

        //[Ignore]
        public string Dummy { get; set; }
    }
}

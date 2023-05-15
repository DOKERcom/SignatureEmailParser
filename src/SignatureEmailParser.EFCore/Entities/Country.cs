using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SignatureEmailParser.EFCore.Entities
{
    public class Country : BaseEntity
    {
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}

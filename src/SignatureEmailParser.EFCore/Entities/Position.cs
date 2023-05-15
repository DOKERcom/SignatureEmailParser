using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SignatureEmailParser.EFCore.Entities
{
    public class Position : BaseEntity
    {
        [Column(TypeName = "varchar(120)")]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}

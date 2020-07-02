using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "ServiceLogs", Schema = "dbo")]
    public class ServiceLog
    {
        [Column(TypeName = "bigint")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LogId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string LogShortName { get; set; } // for querying database

        [Column(TypeName = "varchar(512)")]
        [Required]
        public string LogDescription { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "ExcludedFileFormats", Schema = "dbo")]
    public class ExcludedFileFormat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long FormatId { get; set; }

        [Required]
        [MaxLength(length: 10, ErrorMessage = "Greater than 10 characters is not alllowed")]
        [Column(TypeName = "varchar(10)")]
        public string FormatExtension { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "ComputeRules", Schema = "dbo")]
    public class ComputeRule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long ComputeRuleId { get; set; }

        [Required]
        [MaxLength(length: 100, ErrorMessage = "Greater than 100 characters is not alllowed")]
        [Column(TypeName = "varchar(300)")]
        public string HttpSuccessCodes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
    }
}
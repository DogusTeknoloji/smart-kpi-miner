using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "SearchIndices", Schema = "dbo")]
    public class SearchIndex
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long IndexId { get; set; }

        [Required]
        [MaxLength(length: 50, ErrorMessage = "Greater than 50 characters is not alllowed")]
        [Column(TypeName = "varchar(50)")]
        public string IndexName { get; set; }

        [Required]
        [MaxLength(length: 50 , ErrorMessage = "Greater than 50 characters is not allowed")]
        [Column(TypeName = "varchar(50)")]
        public string IndexPattern { get; set; }

        [Required]
        [MaxLength(length: 250, ErrorMessage = "Greater than 250 characters is not alllowed")]
        [Column(TypeName = "varchar(250)")]
        public string UrlAddress { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        public virtual List<KPIMetric> KPIMetrics { get; set; }
    }
}
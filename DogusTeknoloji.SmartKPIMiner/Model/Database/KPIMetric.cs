using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "KPIMetrics", Schema = "dbo")]
    public class KPIMetric
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long MetricId { get; set; }

        [Required]
        [MaxLength(length: 50, ErrorMessage = "Greater than 50 characters is not alllowed")]
        [Column(TypeName = "varchar(50)")]
        public string ServerName { get; set; }

        [Required]
        [MaxLength(length: 250, ErrorMessage = "Greater than 250 characters is not alllowed")]
        [Column(TypeName = "varchar(250)")]
        public string SiteName { get; set; }

        [Required]
        [MaxLength(length: 512, ErrorMessage = "Greater than 512 characters is not alllowed")]
        [Column(TypeName = "varchar(512)")]
        public string PageUrl { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int? TotalRequestCount { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int? SuccessRequestCount { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int? FailedRequestCount { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double? AverageResponseTime { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double? SuccessAverageResponseTime { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double? FailedAverageResponseTime { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double? SuccessPercentage { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double? FailedPercentage { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LogDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "bigint")]
        [ForeignKey("SearchIndex")]
        public long IndexId { get; set; }
        public SearchIndex SearchIndex { get; set; }

        [Column(TypeName = "bigint")]
        [ForeignKey("Rule")]
        public long ComputeRuleId { get; set; }

        public ComputeRule ComputeRule { get; set; }
    }
}
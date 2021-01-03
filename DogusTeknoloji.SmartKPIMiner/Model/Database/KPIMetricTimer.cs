using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "KPIMetricTimers", Schema = "dbo")]
    public class KPIMetricTimer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long MetricTimerId { get; set; }

        [Column(TypeName = "bigint")]
        [ForeignKey("SearchIndex")]
        public long IndexId { get; set; }
        public SearchIndex SearchIndex { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RowModifyDateLog { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastInsertDate { get; set; }
    }
}

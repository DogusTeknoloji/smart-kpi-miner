using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "SimilarAppFeeds", Schema = "dbo")]
    public class SimilarAppFeed
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long SimilarAppId { get; set; }

        [Required]
        [MaxLength(length: 150, ErrorMessage = "Greater than 150 characters is not alllowed")]
        [Column(TypeName = "varchar(150)")]
        public string ApplicationName { get; set; }

        [ForeignKey("RootAppFeed")]
        [Column(TypeName = "int")]
        public long RootAppId { get; set; }
        public RootAppFeed RootAppFeed { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogusTeknoloji.SmartKPIMiner.Model.Database
{
    [Table(name: "RootAppFeeds", Schema = "dbo")]
    public class RootAppFeed
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public long FeedId { get; set; }

        [Required]
        [MaxLength(length: 150, ErrorMessage = "Greater than 150 characters is not alllowed")]
        [Column(TypeName = "varchar(150)")]
        public string ApplicationName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        public virtual List<SimilarAppFeed> SimilarAppFeeds { get; set; }
    }
}

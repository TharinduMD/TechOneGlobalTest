using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Core
{
    public class City : BasicEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,7)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18,7)")]
        public decimal Longitude { get; set; }

    }
}

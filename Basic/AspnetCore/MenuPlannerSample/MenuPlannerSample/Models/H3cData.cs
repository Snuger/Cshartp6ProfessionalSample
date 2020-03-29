using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlannerSample.Models
{
    public class H3cData
    {
        [Key,MaxLength(36)]
        public string Id { get; set; }

        [MaxLength(100)]
        public string Brand { get; set; }

        [MaxLength(15)]
        public string ModelNum { get; set; }

        public decimal? Price { get; set; }
    }
}

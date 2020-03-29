using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlannerSample.Models
{
    public class SossaManager
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        public string Address { get; set; }
    }
}

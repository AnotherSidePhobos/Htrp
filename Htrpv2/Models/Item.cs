using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Htrpv2.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsItThere { get; set; }
        public bool IsItFood { get; set; }
        public int UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

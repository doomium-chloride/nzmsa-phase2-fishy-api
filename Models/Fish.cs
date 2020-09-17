using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FishyGame.Models
{
    public class Fish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fishID { get; set; }
        public string title { get; set; }
        public string item { get; set; }
        public string colour { get; set; }
        public string eye { get; set; }
        public int parent { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created { get; set; }
        public string del { get; set; }
    }
}

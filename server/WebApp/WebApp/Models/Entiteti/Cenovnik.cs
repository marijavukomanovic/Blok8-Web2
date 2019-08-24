using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Cenovnik
    {
        
        public int Id { get; set; }
        public DateTime VazenjeOd { get; set; }
        public DateTime VazenjeDo { get; set; }
    }
}
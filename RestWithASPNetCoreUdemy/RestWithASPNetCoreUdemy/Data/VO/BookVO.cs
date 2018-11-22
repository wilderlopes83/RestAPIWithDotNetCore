using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNetCoreUdemy.Model.Base;

namespace RestWithASPNetCoreUdemy.Data.VO
{
    public class BookVO
    {
        public long Id {get;set;}
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
    }
}

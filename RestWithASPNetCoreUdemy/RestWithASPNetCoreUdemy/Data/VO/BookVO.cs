using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using RestWithASPNetCoreUdemy.Model.Base;

namespace RestWithASPNetCoreUdemy.Data.VO
{
    //exemplo de utilização de DataContract.
    //serve para definir como o serviço irá expor o objeto de retorno
    //incluindo nome de cada atributo, ordem de exibição etc.
    [DataContract]
    public class BookVO
    {
        [DataMember(Order=1, Name="Código")]
        public long Id {get;set;}

        [DataMember(Order=2, Name="Título")]
        public string Title { get; set; }
        
        [DataMember(Order=3)]
        public string Author { get; set; }

        [DataMember(Order=4)]
        public decimal Price { get; set; }

        [DataMember(Order=5)]
        public DateTime LaunchDate { get; set; }
    }
}

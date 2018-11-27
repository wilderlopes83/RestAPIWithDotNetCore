using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Data.VO;
using Tapioca.HATEOAS;

namespace RestWithASPNetCoreUdemy.Hypermedia
{
    public class PersonEnricher : ObjectContentResponseEnricher<PersonVO>
    {
        public PersonEnricher()
        {
        }

        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/v1/person";
            var url = new { controller = path, id = content.Id };

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = urlHelper.Link("Defaultapi", url),
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
         
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = urlHelper.Link("Defaultapi", url),
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
         
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = urlHelper.Link("Defaultapi", url),
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost         
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = urlHelper.Link("Defaultapi", url),
                Rel = RelationType.self,
                Type = "int"
            });            

            return null;
        }
    }
}
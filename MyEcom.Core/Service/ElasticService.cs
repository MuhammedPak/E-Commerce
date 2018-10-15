using MyEcom.Common.RedisDto;
using MyEcom.Core.Repository;
using MyEcom.Core.Service.Interfaces;
using MyEcom.Domain.Abstract;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service
{
    public class ElasticService : IElasticService
    {
        public void CreateIndex()

        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node)
                .DefaultIndex("newhotel");

            var client = new ElasticClient(settings);
            var del = client.DeleteIndex("newhotel");
            var newIndex = client.CreateIndex("newhotel", p => p
               .Settings(q => q
                    .NumberOfReplicas(0)
                    .NumberOfShards(1))
                .Mappings(m => m
                    .Map<ElasticDto>(k => k
                    .AutoMap())));
            var model = GetDto();
            foreach (var item in model)
            {
                var indexResponse = client.IndexDocument(item);
            }
        }

        public List<ElasticDto> Search(string key)
        {

            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node)
                .DefaultIndex("newhotel");

            var client = new ElasticClient(settings);
            var searchResponse = client.Search<ElasticDto>(s => s
               .Index("newhotel")
                                .AllTypes()
                                .Query(q => q
                                    .Prefix(f => f.Field(x => x.Name).Value(key))
                                )
                            );

            return searchResponse.Documents.ToList();
        }
        public List<ElasticDto> GetDto()
        {
            using (BaseRepository<Hotel> db = new BaseRepository<Hotel>())
            {
                return db.Query<Hotel>().Select(a => new ElasticDto
                {    Id=a.Id,
                    Name=a.Name
                    
                }).ToList();
            }
            
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;
using mboard.Models;

namespace mboard.Controllers
{
    public class Crud
    {
        GraphClient gc = new GraphClient(new Uri("http://localhost:7474/db/data"));
        public void Create<T>(T model)
        {
            gc.Connect();
            string label = typeof(T).Name;
            gc.Cypher
                .Create("(node:" + label + " {param})")
                .WithParam("param", model)
                .ExecuteWithoutResults();
        }
        public T Read<T>(int nodeId)
        {
            gc.Connect();
            string label = typeof(T).Name;
            var query = gc.Cypher
                .Match("(node:" + label + ")")
                .Where((ICrud node) => node.Id == nodeId)
                .Return(node => node.As<T>())
                .Results;
            T result = query.First();
            return result;
        }
        public void Delete<T>(int nodeId)
        {
            gc.Connect();
            string label = typeof(T).Name;
            gc.Cypher
                .Match("(node:" + label + ")")
                .Where((ICrud node) => node.Id == nodeId)
                .Delete("node")
                .ExecuteWithoutResults();
        }
    }
}
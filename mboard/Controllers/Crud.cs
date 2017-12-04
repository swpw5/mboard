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
        /*public void CreateRelations<Relationship>(Relationship relModel)
        {
            gc.Connect();
            string label = typeof(Relationship).Name;
                .Match("(firstNode:" + Relationship + ")", "(secondNode:)")
                .Where(() => .Id == 123)
                .AndWhere(() => .Id == 456)
                .Create("-[:" + label + "]->")
                .ExecuteWithoutResults();
        }*/
        public List<T> ReadRelated<T>(int nodeId, string relationType)
        {
            gc.Connect();
            string label = typeof(T).Name;
            var query = gc.Cypher
                .Match("(parentNode)-[" + relationType + "]->(node:" + label + "))")
                .Where((ICrud parentNode) => parentNode.Id == nodeId)
                .Return(node => node.As<T>())
                .Results;
            List<T> result = query.ToList();
            return result;
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
        public void DeleteRelated<T>(int nodeId, string relationType)
        {
            gc.Connect();
            string label = typeof(T).Name;
            gc.Cypher
                .Match("(parentNode)-[" + relationType + "]->(node:" + label + "))")
                .Where((ICrud parentNode) => parentNode.Id == nodeId)
                .Delete("node")
                .ExecuteWithoutResults();
        }
        public void UpdateSingle<T, R>(int nodeId, R modelProp)
        {
            gc.Connect();
            string label = typeof(T).Name;
            string propLabel = typeof(R).Name;
            gc.Cypher
                .Match("(node:" + label + ")")
                .Where((ICrud node) => node.Id == nodeId)
                .Set(label+"."+propLabel+"={prop}")
                .WithParam("prop", modelProp)
                .ExecuteWithoutResults();
        }
        public void UpdateAll<T>(int nodeId, T model)
        {
            gc.Connect();
            string label = typeof(T).Name;
            gc.Cypher
                .Match("(node:" + label + ")")
                .Where((ICrud node) => node.Id == nodeId)
                .Set(label + "={model}")
                .WithParam("model", model)
                .ExecuteWithoutResults();
        }
    }
}
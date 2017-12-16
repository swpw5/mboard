using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4j.Driver.V1;
using Neo4jClient;
using mboard.Models;
using Newtonsoft.Json;

namespace mboard.Models
{
    public class NeoDbContext
    {
        //private static GraphClient gc = new GraphClient(new Uri("http://localhost:7474/db/data"));

        private static GraphClient gc = new GraphClient(new Uri("http://localhost:7474/db/data"), username: "neo4j", password: "alpha");

        public static GraphClient GC { get { return gc; } }

        public static NeoDbContext Create()
        {
            return new NeoDbContext();
        }
        public void CreateNode(INodeModel model)
        {
            gc.Connect();
            string label = model.GetType().Name;
            gc.Cypher
                .Merge("(node:" + label + " {Id: {id} })")
                .OnCreate()
                .Set("node = {model}")
                .WithParams(new { id = model.Id, model })
                .ExecuteWithoutResults();
        }
        public void CreateNodeWithRelation<RelationT>(INodeModel model, string RelNode, RelationT Rel)
        {
            gc.Connect();
            string label = model.GetType().Name;
            string labelRel = typeof(RelationT).Name;
            gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == RelNode)
                .CreateUnique("(node)<-[:" + labelRel + " {param}" + "]-(createdNode:" + label + " {data})")
                .WithParam("data", model)
                .WithParam("param", Rel)
                .ExecuteWithoutResults();
        }
        public void CreateNodeWithRelation(INodeModel model, string RelNodeId, string Rel)
        {
            gc.Connect();
            string label = model.GetType().Name;
            gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == RelNodeId)
                .CreateUnique("(node)<-[:" + Rel + "]-(createdNode:" + label + " {data})")
                .WithParam("data", model)
                .ExecuteWithoutResults();
        }
        public void CreateRelation(string FirstNodeId, string SecondNodeId, string RelationshipType)
        {
            gc.Connect();
            gc.Cypher
                .Match("(firstNode)", "(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == FirstNodeId)
                .CreateUnique("(firstNode)-[:" + RelationshipType + "]->(secondNode)")
                .ExecuteWithoutResults();
        }
        public void CreateRelation<RelationType>(string FirstNodeId, string SecondNodeId, RelationType rel)
        {
            gc.Connect();
            gc.Cypher
                .Match("(firstNode)", "(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == SecondNodeId)
                .CreateUnique("(firstNode)-[:" + rel.GetType().Name + " {param}]->(secondNode)")
                .WithParam("param", rel)
                .ExecuteWithoutResults();
        }
        public void CreateRelation<RelationType>(Relation<RelationType> rel)
        {
            gc.Connect();
            gc.Cypher
                .Match("(firstNode)", "(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == rel.FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == rel.SecondNodeId)
                .CreateUnique("(firstNode)-[:" + rel.Rel.GetType().Name + " {param}]->(secondNode)")
                .WithParam("param", rel.Rel)
                .ExecuteWithoutResults();
        }

        public IEnumerable<NodeResultType> ReadRelatedNodes<RelationType, NodeResultType>(string nodeId)
        {
            gc.Connect();
            string labelRelation = typeof(RelationType).Name;
            string label = typeof(NodeResultType).Name;
            var query = gc.Cypher
                .Match("(parentNode)-[rel:" + labelRelation + "]-(node: " + label + ")")
                .Where((INodeModel parentNode) => parentNode.Id == nodeId)
                .Return(node => node.As<NodeResultType>())
                .Results;
            IEnumerable<NodeResultType> result = query.AsEnumerable();
            return result;
        }
        public IEnumerable<RelationWithNode<NodeResultType, RelationType>> ReadRelatedNodesWithRelations<RelationType, NodeResultType>(string nodeId)
        {
            gc.Connect();
            string labelRelation = typeof(RelationType).Name;
            var query = gc.Cypher
                .Match("(parentNode)-[rel:" + labelRelation + "]->(node)")
                .Where((INodeModel parentNode) => parentNode.Id == nodeId)
                .Return((node, rel) => new RelationWithNode<NodeResultType, RelationType> { NodeData = node.As<NodeResultType>(), RelationData = rel.As<RelationType>() })
                .Results;
            IEnumerable<RelationWithNode<NodeResultType, RelationType>> result = query.AsEnumerable();
            return result;
        }
        public NodeType ReadNode<NodeType>(string uniqueProperty, string value)
        {
            gc.Connect();
            string label = typeof(NodeType).Name;
            var query = gc.Cypher
                .Match("(node:" + label + " {" + uniqueProperty + ": {val} })")
                .WithParam("val", value)
                .Return(node => node.As<NodeType>())
                .Results;
            NodeType result = query.First();
            return result;
        }
        public NodeType ReadNode<NodeType>(string nodeId)
        {
            gc.Connect();
            var query = gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == nodeId)
                .Return(node => node.As<NodeType>())
                .Results;
            NodeType result = query.First();
            return result;
        }
        public IEnumerable<NodeType> ReadNodeType<NodeType>()
        {
            gc.Connect();
            string label = typeof(NodeType).Name;
            var query = gc.Cypher
                .Match("(node:" + label + ")")
                .Return(node => node.As<NodeType>())
                .Results;
            IEnumerable<NodeType> result = query.AsEnumerable();
            return result;
        }
        public NodeType ReadNode<NodeType>(INodeModel Node)
        {
            gc.Connect();
            var query = gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == Node.Id)
                .Return(node => node.As<NodeType>())
                .Results;
            NodeType result = query.First();
            return result;
        }
        public void DeleteNode(string nodeId)
        {
            gc.Connect();
            gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == nodeId)
                .Delete("node")
                .ExecuteWithoutResults();
        }
        public void DeleteNodeWithRelations(string nodeId)
        {
            gc.Connect();
            gc.Cypher
                .Match("(node)-[rel]-()")
                .Where((INodeModel node) => node.Id == nodeId)
                .Delete("rel, node")
                .ExecuteWithoutResults();
        }
        public void DeleteRelatedNode<NodeType>(string nodeId, string relationType)
        {
            gc.Connect();
            gc.Cypher
                .Match("(parentNode)-[" + relationType + "]->(node))")
                .Where((INodeModel parentNode) => parentNode.Id == nodeId)
                .Delete("node")
                .ExecuteWithoutResults();
        }
        public void UpdateSinglePropNode(string nodeId, string nodeProperty, string nodePropName)
        {
            gc.Connect();
            gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == nodeId)
                .Set("node." + nodePropName + " = {prop}")
                .WithParam("prop", nodeProperty)
                .ExecuteWithoutResults();
        }
        public void UpdateAllPropNode<NodeType>(string nodeId, NodeType model)
        {
            gc.Connect();
            gc.Cypher
                .Match("(node)")
                .Where((INodeModel node) => node.Id == nodeId)
                .Set("node = {model}")
                .WithParam("model", model)
                .ExecuteWithoutResults();
        }
        public RelationType ReadRelationData<RelationType>(string idFirstNode, string idSecondNode)
        {
            string label = typeof(RelationType).Name;
            gc.Connect();
            var query = gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == idFirstNode)
                .AndWhere((INodeModel secondNode) => secondNode.Id == idSecondNode)
                .Return(rel => rel.As<RelationType>())
                .Results;
            RelationType result = query.First();
            return result;
        }
        public Relation<RelationType> ReadRelation<RelationType>(string idFirstNode, string idSecondNode)
        {
            string label = typeof(RelationType).Name;
            gc.Connect();
            var query = gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == idFirstNode)
                .AndWhere((INodeModel secondNode) => secondNode.Id == idSecondNode)
                .Return(rel => rel.As<RelationType>())
                .Results;
            RelationType queryResult = query.First();
            Relation<RelationType> result = new Relation<RelationType> { FirstNodeId = idFirstNode, SecondNodeId = idSecondNode, Rel = queryResult };
            return result;
        }
        public bool CheckRelationExist<RelationType>(string idFirstNode, string idSecondNode)
        {
            string label = typeof(RelationType).Name;
            gc.Connect();
            var query = gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == idFirstNode)
                .AndWhere((INodeModel secondNode) => secondNode.Id == idSecondNode)
                .Return(rel => rel.As<RelationType>())
                .Results;
            bool result = (query.First() != null) ? true : false;
            return result;
        }
        public bool CheckRelationExist<RelationType>(Relation<RelationType> Rel)
        {
            string label = Rel.Rel.GetType().Name;
            gc.Connect();
            var query = gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == Rel.FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == Rel.SecondNodeId)
                .Return(rel => rel.As<RelationType>())
                .Results;
            bool result = (query.First() != null) ? true : false;
            return result;
        }
        public void UpdateRelationSingleProperty<RelationType>(string FirstNodeId, string SecondNodeId, string RelationPropertyName, string RelationProperty)
        {
            gc.Connect();
            string label = typeof(RelationType).Name;
            gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == SecondNodeId)
                .Set("rel." + RelationPropertyName + " = {param}")
                .WithParam("param", RelationProperty)
                .ExecuteWithoutResults();
        }
        public void DeleteRelation<RelationType>(Relation<RelationType> Rel)
        {
            gc.Connect();
            string label = typeof(RelationType).Name;
            gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == Rel.FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == Rel.SecondNodeId)
                .Delete("rel")
                .ExecuteWithoutResults();

        }
        public void DeleteRelation<RelationType>(string FirstNodeId, string SecondNodeId)
        {
            gc.Connect();
            string label = typeof(RelationType).Name;
            gc.Cypher
                .Match("(firstNode)-[rel:" + label + "]-(secondNode)")
                .Where((INodeModel firstNode) => firstNode.Id == FirstNodeId)
                .AndWhere((INodeModel secondNode) => secondNode.Id == SecondNodeId)
                .Delete("rel")
                .ExecuteWithoutResults();
        }
        public void DeleteAllRelationType<RelationType>(string NodeId)
        {
            gc.Connect();
            string label = typeof(RelationType).Name;
            gc.Cypher
                .Match("(node)-[rel:" + label + "]-()")
                .Where((INodeModel node) => node.Id == NodeId)
                .Delete("rel")
                .ExecuteWithoutResults();
        }
        public void DeleteAllRelation(string NodeId)
        {
            gc.Connect();
            gc.Cypher
                .Match("(node)-[rel]-()")
                .Where((INodeModel node) => node.Id == NodeId)
                .Delete("rel")
                .ExecuteWithoutResults();
        }

        public int CountRelation<RelationT>(string NodeId)
        {
            gc.Connect();
            string label = typeof(RelationT).Name;
            var query = gc.Cypher
                .OptionalMatch("(node)-[rel:" + label + "]-(nodeRel)")
                .Where((INodeModel node) => node.Id == NodeId)
                .Return(nodeRel => nodeRel.Count())
                .Results;
            gc.Connect();
            int result = (int)query.First();
            return result;
        }
        public BoardModelView PopulateBoardModelView(string parentNodeId)
        {
            gc.Connect();
            var query1 = gc.Cypher
                .Match("(parentNode:Board)-[:BoardHave]-(node:Pin)-[rel:PinNoteConnection]-(childNode:Note)")
                .Where((INodeModel parentNode) => parentNode.Id == parentNodeId)
                .Return((node, childNode, rel) => new RelationWithNodes<Pin, Note, PinNoteConnection> { FirstNodeData = node.As<Pin>(), SecNodeData = childNode.As<Note>(), RelationData = rel.As<PinNoteConnection>() })
                .Results;
            List<RelationWithNodes<Pin, Note, PinNoteConnection>> PinNoteCon = query1.ToList();
            var query2 = gc.Cypher
                .Match("(parentNode:Board)-[rel:BoardHave]-(node:Pin)-[rel:PinConnection]-(toNode:Pin)")
                .Where((INodeModel parentNode) => parentNode.Id == parentNodeId)
                .Return((node, rel, toNode) => new Relation<PinConnection> { FirstNodeId = node.Id().ToString(), SecondNodeId = toNode.Id().ToString(), Rel = rel.As<PinConnection>() })
                .Results;
            List<Relation<PinConnection>> PinToPin = query2.ToList();
            var query3 = gc.Cypher
                .Match("(node:Board)")
                .Where((INodeModel parentNode) => parentNode.Id == parentNodeId)
                .Return((node) => node.As<Board>())
                .Results;
            Board board = query3.First();
            BoardModelView result = new BoardModelView { Board = board, PinNote = PinNoteCon };
            return result;
        }
    }
}
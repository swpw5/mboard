using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public interface INodeModel
    {
        string Id { get; set; }
    }
    public interface IRelation
    {
        string Id { get; set; }
    }
    public class Relation<T>
    {
        public string FirstNodeId { get; set; }
        public string SecondNodeId { get; set; }
        public T Rel { get; set; }
    }
    public class RelationWithNode<NodeT, RelationT>
    {
        public NodeT NodeData { get; set; }
        public RelationT RelationData { get; set; }
    }
    public class SimpleRelation
    {
    }
    public class RelationWithNodes<NodeT, SecNodeT, RelationT>
    {
        public NodeT FirstNodeData { get; set; }
        public SecNodeT SecNodeData { get; set; }
        public RelationT RelationData { get; set; }
    }
}
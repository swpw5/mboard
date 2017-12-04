namespace mboard.Models
{
    public interface IRelationship
    {
        int IdFirst { get; set; }
        int IdSecond { get; set; }
    }
}
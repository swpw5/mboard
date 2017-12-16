namespace mboard.mvc.Models
{
    public class PVector
    {
        public float X { get; set; }
        public float Y  { get; set; }

        public PVector(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
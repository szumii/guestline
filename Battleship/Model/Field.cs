namespace Battleship.Model
{
    public class Field
    {
        public Coordinates Coordinates { get; set; }
        public FieldType FieldType { get; set; }
        public Ship Ship { get; set; }
    }
}
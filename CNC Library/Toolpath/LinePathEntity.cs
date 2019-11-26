using ICNCLib;
namespace CNCLib
{
    internal class LinePathEntity : PathEntity
    {
        public bool RapidMove { get; set; }
        public bool PlungeMove { get; set; }




        public LinePathEntity(BlockType type)
        {
            base.Type = type;
            RapidMove = type == BlockType.RAPID;

        }

    }
}

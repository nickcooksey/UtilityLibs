namespace ICNCLib
{
    public interface IMachinePosition
    {
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        double Adeg { get; set; }
        double Bdeg { get; set; }
        double Cdeg { get; set; }
        void BuildFromString(string line);
        string AsNcString( INcMachine ncMachine, IMachinePosition prevPosition,BlockType blockType, MoveType moveType);
       
        double DistanceTo(IMachinePosition machinePosition);
    }
}

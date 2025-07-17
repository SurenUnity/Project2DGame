namespace Services.Level
{
    public interface ILevelItem
    {
        int Level { get; }
        int PairCount { get; }
        string[] CardIds { get; }
        int Columns { get; }
        int Rows { get; }
        int TriesCount { get; }
    }
}
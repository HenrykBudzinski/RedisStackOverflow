namespace RedisStackOverflow.Entities
{
    public struct LocationPosition
    {
        public LocationPosition(
            uint x, uint y)
        {
            PosX = x;
            PosY = y;
        }

        public uint PosX { get; set; }
        public uint PosY { get; set; }
    }
}

namespace RedisStackOverflow.Entities
{
    public struct LocationSize
    {
        public LocationSize(
            uint h, uint w)
        {
            Height = h;
            Width = w;
        }

        public uint Height { get; set; }
        public uint Width { get; set; }
    }
}

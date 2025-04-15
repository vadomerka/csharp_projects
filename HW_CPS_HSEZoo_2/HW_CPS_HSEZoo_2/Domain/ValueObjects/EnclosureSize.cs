namespace HW_CPS_HSEZoo_2.Domain.ValueObjects
{
    internal struct EnclosureSize
    {
        public EnclosureSize() {
            Length = 0;
            Width = 0;
            Height = 0;
        }
        public EnclosureSize(int length, int width, int height) {
            Length = length;
            Width = width;
            Height = height;
        }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

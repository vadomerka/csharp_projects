namespace HW_CPS_HSEZoo_2.Domain.ValueObjects
{
    public struct EnclosureSize
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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (((EnclosureSize)obj).Length != Length) return false;
            if (((EnclosureSize)obj).Width != Width) return false;
            if (((EnclosureSize)obj).Height != Height) return false;
            return true;
        }
    }
}

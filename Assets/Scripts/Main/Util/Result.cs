namespace Main.Util
{
    public readonly struct Result<T>
    {
        public readonly T Data;
        public readonly bool Valid;

        public Result(T data, bool valid)
        {
            Data = data;
            Valid = valid;
        }
    }
}

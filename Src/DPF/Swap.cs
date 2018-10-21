namespace DPF
{
    public static class Swap
    {
        public static void SwapWithTemp(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        public static void SwapXOR(ref int x, ref int y)
        {
            x = x ^ y;
            y = x ^ y;
            x = x ^ y;
        }
    }
}

namespace CustomExtensions
{
    public static class CustomExtensions
    {
        public static object[] Shuffle(this object[] arr)
        {
            System.Random rng = new System.Random();
            for (int i = 0; i < arr.Length; i++)
            {
                object tmp = arr[i];
                int r = rng.Next(i, arr.Length);
                arr[i] = arr[r];
                arr[r] = tmp;
            }
            return arr;
        }
    }
}

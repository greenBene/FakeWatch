namespace CustomExtensions
{
    public static class CustomExtensions
    {
        public static void Shuffle(this object[] arr)
        {
            System.Random rng = new System.Random();
            for (int i = 0; i < arr.Length; i++)
            {
                var tmp = arr[i];
                int r = rng.Next(i, arr.Length);
                arr[i] = arr[r];
                arr[r] = tmp;
            }
        }

        public static string ToTimeString(this float f) {
            return ((int)(f / 60)).ToString("D2") + ":" + ((int)(f % 60)).ToString("D2");
        }
    }
}

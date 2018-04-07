using System;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Generic;

static class Utils
{
  public static List<T> Shuffle<T>(this List<T> list)
  {
    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
    int n = list.Count;
    while (n > 1)
    {
      byte[] box = new byte[1];
      do provider.GetBytes(box);
      while (!(box[0] < n * (Byte.MaxValue / n)));
      int k = (box[0] % n);
      n--;
      T value = list[k];
      list[k] = list[n];
      list[n] = value;
    }
    return list;
  }
}

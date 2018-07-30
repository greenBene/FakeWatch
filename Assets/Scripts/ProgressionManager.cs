using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgressionManager {

    static int newsCount = 0;
    static PoissonRNG Ran = null;

    public static void RestartCounter() {
        newsCount = 0;
    }

    public static int GetProgression(int deltaNewsCount = 1) {
        newsCount += deltaNewsCount;
        if (Ran == null) {
            Ran = new PoissonRNG();
        }
        return Ran.Next(4/newsCount, Mathf.Max(4 - newsCount, 0), 3);
    }
}

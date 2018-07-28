using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgressionManager {

    static int newsCount = 0;

    public static void RestartCounter() {
        newsCount = 0;
    }

    public static int GetProgression(int deltaNewsCount = 1) {
        newsCount += deltaNewsCount;
        return 4/newsCount;
    }
}

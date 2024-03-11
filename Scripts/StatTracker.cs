using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatTracker {
    private static int kills = 0;
    private static int partsMerged = 0;
    private static int maxParts = 0;
    private static int currNumParts;

    public static void kill() {
        kills++;
    }

    public static void mergePart() {
        partsMerged++;
        currNumParts++;
        updateMaxParts();
    }

    public static void losePart() {
        currNumParts--;
    }

    private static void updateMaxParts() {
        if (currNumParts > maxParts) {
            maxParts = currNumParts;
        }
    }

    public static int getKills() {
        return kills;
    }
}

using System;
using UnityEngine.SceneManagement;

public static class LevelManager {
    public static int MAX_LEVEL_INDEX = 2;
    public static void LoadNextLevel() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;
        if (nextLevel > MAX_LEVEL_INDEX) nextLevel = 0;
        SceneManager.LoadScene(nextLevel);
    }

    public static void LoadLevel(int buildIndex) {
        SceneManager.LoadScene(buildIndex);
    }

    public static void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using UnityEngine.SceneManagement;

public static class SceneManagement
{
    public static void LoadScene(string sceneName) =>
        SceneManager.LoadScene(sceneName);
}

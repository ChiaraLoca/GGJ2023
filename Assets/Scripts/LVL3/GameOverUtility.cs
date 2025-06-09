using UnityEngine.SceneManagement;

namespace Level3
{
    public class GameOverUtility
    {
        public static void GameOver()
        {
            SceneManager.LoadScene("level3");
        }
    }
}
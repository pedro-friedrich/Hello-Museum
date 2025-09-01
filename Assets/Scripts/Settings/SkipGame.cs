using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipGame : MonoBehaviour
{
    public void Skip()
    {   
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameState++;
        }

        SceneManager.LoadScene("Game");
    }
}

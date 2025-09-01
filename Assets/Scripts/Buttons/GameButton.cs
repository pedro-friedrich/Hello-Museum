using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour, IButtonParameter
{
    private string targetGame;
    public GameObject player;

    public void ReceiveParameter(object parameter)
    {
        targetGame = parameter as string;

        if (targetGame == null)
        {
            Debug.LogWarning("Invalid Scene passed to GameButton.");
        }
    }

    public void Click()
    {
        if (targetGame != null)
        {
            GameManager.Instance.playerPosition = player.transform.position;
            GameManager.Instance.playerRotation = player.transform.rotation.eulerAngles;
            GameManager.Instance.fuses = Inventory.Instance.fuses;
            GameManager.Instance.hasFlashlight = Inventory.Instance.hasFlashlight;
            GameManager.Instance.firstStart = false;
            SceneManager.LoadScene(targetGame);
        }
        else
        {
            Debug.LogWarning("No Scene assigned to GameButton.");
        }
    }
}

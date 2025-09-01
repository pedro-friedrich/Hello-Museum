using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public int fuses;
    public int gameState;
    public bool hasFlashlight;

    [HideInInspector] public bool firstStart = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState(int state)
    {
        gameState = state;
    }
}

using UnityEditor;
using UnityEngine;

public class MinigameObject : MonoBehaviour
{
    public float playRange;
    public LayerMask playerLayer;
    public bool playerHit;
    public string gameScene;
    public int state;

    private Collider[] hit;

    void Start()
    {
        transform.Find("Light").gameObject.SetActive(false);
    }
    void Update()
    {
        if (state <= GameManager.Instance.gameState)
        {
            transform.Find("Light").gameObject.SetActive(true);
            
            ShowMinigame();
        }
    }

    void ShowMinigame()
    {
        hit = Physics.OverlapSphere(transform.position, playRange, playerLayer);

        if (hit.Length > 0 && !playerHit)
        {
            playerHit = true;

            ButtonController.Instance.ActivateBtn("Game", gameScene);
        }
        else if (hit.Length == 0 && playerHit)
        {
            playerHit = false;

            ButtonController.Instance.DeactivateBtn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playRange);
    }
}

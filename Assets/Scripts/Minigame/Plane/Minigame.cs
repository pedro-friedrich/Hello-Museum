using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlaneMinigame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject endgamePanel;
    public TextMeshProUGUI endgameText;
    public Text missionText;
    public Transform livesObj;
    public int lives;
    public List<Transform> slots;
    public List<GameObject> enemies;
    public float planeFlightSpeed;
    public int spawnRate;

    private List<GameObject> lifes = new List<GameObject>();
    private bool playing;
    private int score = 0;

    public bool Playing { get => playing; set => playing = value; }

    void Start()
    {
        foreach (Transform life in livesObj)
        {
            lifes.Add(life.gameObject);
        }
    }

    IEnumerator EnemySpawn()
    {
        while (playing)
        {
            yield return new WaitForSeconds(2);
            int spawnIndex = Random.Range(0, 5);
            Transform chosenSlot = slots[spawnIndex];

            switch (spawnIndex)
            {
                case 0:
                    missionText.text = "Enemy coming from the Left";
                    break;
                case 1:
                    missionText.text = "Enemy coming from the Center";
                    break;
                case 2:
                    missionText.text = "Enemy coming from the Right";
                    break;
                case 3:
                    missionText.text = "Enemy coming from Above";
                    break;
                case 4:
                    missionText.text = "Enemy coming from Below";
                    break;
            }
            yield return new WaitForSeconds(2);

            int enemyIndex = Random.Range(0, 3);
            GameObject enemyPrefab = enemies[enemyIndex];
            GameObject newEnemy = Instantiate(enemyPrefab, chosenSlot.position, Quaternion.identity);
            newEnemy.transform.SetParent(chosenSlot);

            EnemyMovement movement = newEnemy.GetComponent<EnemyMovement>();
            if (spawnIndex <= 2)
            {
                newEnemy.transform.rotation = Quaternion.Euler(0, 0, 180);
                movement.direction = Vector3.down;
            }
            else
            {
                newEnemy.transform.rotation = Quaternion.Euler(0, 0, -90);
                movement.direction = Vector3.right;
            }
            yield return new WaitForSeconds(2);
            score++;
            missionText.text = "";
            Destroy(newEnemy);

            if (score >= 6)
            {
                Win();
            }
        }
    }

    public void Hit()
    {
        if (score > 0)
        {
            score--;
        }
        lives--;
        lifes[lifes.Count - 1].SetActive(false);
        lifes.RemoveAt(lifes.Count - 1);
        if (lives <= 0)
        {
            Lose();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        score = 0;
        lives = 3;
        foreach (Transform life in livesObj)
        {
            life.gameObject.SetActive(true);
            lifes.Add(life.gameObject);
        }
        playing = true;
        menuPanel.SetActive(false);
        endgamePanel.SetActive(false);

        StartCoroutine(EnemySpawn());
    }

    public void EndGame(bool victory)
    {
        endgamePanel.SetActive(true);

        if (victory)
        {
            endgameText.text = "Vitória!";
        }
        else
        {
            endgameText.text = "Derrota";
        }
    }

    void Win()
    {
        playing = false;
        EndGame(true);

        if (GameManager.Instance.gameState == 8)
        {
            GameManager.Instance.gameState++;
        }
    }

    void Lose()
    {
        playing = false;
        EndGame(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TankMinigame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject endgamePanel;
    public TextMeshProUGUI endgameText;
    public Text missionText;
    public Transform livesObj;
    public Transform spawn1;
    public Transform spawn2;
    public List<GameObject> shapes;
    public int lives;
    [HideInInspector] public int targetIndex = 10;
    [HideInInspector] public int score = 0;

    private List<GameObject> lifes = new List<GameObject>();
    private bool playing;
    private int lastTargetIndex = 10;

    public bool Playing { get => playing; set => playing = value; }

    void Start()
    {
        foreach (Transform life in livesObj)
        {
            lifes.Add(life.gameObject);
        }
    }

    IEnumerator ShapeSpawn()
    {
        while (playing)
        {
            Transform chosenSlot;
            int shapeIndex = Random.Range(0, 5);
            int spawnIndex = Random.Range(0, 2);
            if (spawnIndex == 1)
            {
                chosenSlot = spawn1;
            }
            else
            {
                chosenSlot = spawn2;
            }

            //yield return new WaitForSeconds(1);

            GameObject shapePrefab = shapes[shapeIndex];
            GameObject shape = Instantiate(shapePrefab, chosenSlot.position, Quaternion.identity);
            shape.transform.SetParent(chosenSlot);

            ShapeMovement movement = shape.GetComponent<ShapeMovement>();
            if (spawnIndex == 1)
            {
                movement.direction = Vector3.right;
            }
            else
            {
                movement.direction = Vector3.left;
            }
            yield return new WaitForSeconds(3);
            Destroy(shape);
        }
    }

    public void NextShape()
    {
        missionText.text = "";
        while (targetIndex == lastTargetIndex)
        {     
            targetIndex = Random.Range(0, 5);
        }
        lastTargetIndex = targetIndex;

        switch (targetIndex)
        {
            case 0:
                missionText.text = "Hit the triangle";
                break;
            case 1:
                missionText.text = "Hit the diamond";
                break;
            case 2:
                missionText.text = "Hit the star";
                break;
            case 3:
                missionText.text = "Hit the circle";
                break;
            case 4:
                missionText.text = "Hit the square";
                break;
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        lives = 3;
        score = 0;

        foreach (Transform life in livesObj)
        {
            life.gameObject.SetActive(true);
            lifes.Add(life.gameObject);
        }
        playing = true;
        menuPanel.SetActive(false);
        endgamePanel.SetActive(false);

        NextShape();
        StartCoroutine(ShapeSpawn());
    }

    public void Hit(bool hit)
    {
        if (hit)
        {
            score++;

            if (score >= 4)
            {
                Win();
            }
            NextShape();
        }
        else
        {
            lives--;
            lifes[lifes.Count - 1].SetActive(false);
            lifes.RemoveAt(lifes.Count - 1);
            if (lives <= 0)
            {
                Lose();
            }
        }

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

        if (GameManager.Instance.gameState == 22)
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float threatMeter;
    int threatLvl = 1;
    public GameObject blockPrefab;
    public Transform player;
    GameObject Enemies;
    Transform enemies;

    [Header("Score")]
    int score;
    public TextMeshProUGUI scoreText;
    int highScore;
    // Start is called before the first frame update
    void Awake()
    {
        Enemies = GameObject.FindGameObjectWithTag("Orginization");
        enemies = Enemies.transform;
        scoreText.text = string.Empty;
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateThreats();
        SpawnBlocks();
        
    }

    void UpdateThreats()
    {
        threatMeter = Time.deltaTime;
        if (threatMeter > 30)
        {
            threatMeter = 0;
            threatLvl += 1;
        }
    }
    void SpawnBlocks()
    {
       int chance = Random.Range(0, 100);
        if (chance <= threatLvl*3)
        {
            CreateEnemies();
            return;
        }
    }
    void CreateEnemies()
    {
        GameObject block = Instantiate(blockPrefab);
        block.transform.SetParent(enemies);
        block.transform.position = new Vector3(player.transform.position.x + (Random.Range(-10,10)), player.transform.position.y + (Random.Range(-10, 10)), player.transform.position.z);
        block.transform.localScale = new Vector3(Random.Range(.25f, 1), Random.Range(.25f, 1), Random.Range(.25f, 1));
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}

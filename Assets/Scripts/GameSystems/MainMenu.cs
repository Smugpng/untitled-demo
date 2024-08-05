using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    GameObject[] blocks;
    public float timer;
    public TextMeshProUGUI HighScore;
    // Start is called before the first frame update
    private void Start()
    {
        HighScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }
    private void Update()
    {
        blocks = GameObject.FindGameObjectsWithTag("block");
        timer += Time.deltaTime;
        DestroyBlocks();
    }
    void DestroyBlocks()
    {
        if (timer >= 30)
        {
            timer = 0;
            for (int i = 0; i < blocks.Length; i++)
            {
                Destroy(blocks[i]);
            }
            
        }
    }
    
    public void Play()
    {
        SceneManager.LoadScene(1);
        
    }
    
 
}

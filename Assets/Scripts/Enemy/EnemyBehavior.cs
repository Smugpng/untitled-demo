using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    private GameManager gameManager;
    Transform hisTransform;
    float moveSpeed;
    BoxCollider2D box;
    public LayerMask walls;
    private SpriteRenderer spriteRenderer;

    [Header("On Death")]
    [SerializeField] private GameObject cubePrefab;
    public bool isSpawn;
    public int amountOfSpawn;
    public Transform Enemies;
   

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        amountOfSpawn = Random.Range(2, 5);
        box = GetComponent<BoxCollider2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = Random.ColorHSV();
        if (!isSpawn)
        {
            moveSpeed = Random.Range(.005f, .05f);
            box.excludeLayers = walls;
        }
        else
        {
            moveSpeed = .01f;
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        MoveBlock();
    }

    private void MoveBlock()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed);
    }
    public void HitDelay()
    {
        Invoke("Hit", .25f);
    }

    private void Hit()
    {
        if (!isSpawn)
        {
            gameManager.AddPoints(50);
            for (int x = 0; x < amountOfSpawn; x++)
            {
                
                CreateSpawn();
            }
           
        }
        else
        {
            gameManager.AddPoints(25);
        }
        Destroy(this.gameObject);
    }
    void CreateSpawn()
    {
        GameObject smallEnemy = Instantiate(cubePrefab);
        smallEnemy.transform.localScale = transform.localScale/amountOfSpawn;
        smallEnemy.transform.position = this.transform.position * Random.Range(.75f,1);
        smallEnemy.transform.SetParent(Enemies);
        Rigidbody2D rb = smallEnemy.GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-5, 5);
        float randomY = Random.Range(-5, 5);
        rb.AddForce(new Vector2(randomX, randomY)/2, ForceMode2D.Impulse);
    }
  
   
}

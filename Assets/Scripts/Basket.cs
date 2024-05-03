using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public float moveSpeed;
    public float boundaryX = 2.5f;
    private FoodSpawner foodSpawner;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        foodSpawner = FindObjectOfType<FoodSpawner>();
    }


    void Update()
    {
       
    }
    
    public void MoveLeft()
    {
        if(transform.position.z <= 271.11f)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        
    }

    public void MoveRight()
    {
        if(transform.position.z >= 260.92f)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            gameManager.IncreaseCoins();
            gameManager.IncreaseScore();
            foodSpawner.RemoveFood(collision.gameObject);
        }
    }
}
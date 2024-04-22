using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    private List<GameObject> foodList = new List<GameObject>();
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameObject spawnedFood = Instantiate(foodPrefab, SpawnFood(), Quaternion.identity);
        foodList.Add(spawnedFood);
    }

    // Update is called once per frame
    void Update()
    {
        if (foodList.Count < 1 && !gameManager.gamePaused)
        {
            GameObject spawnedFood = Instantiate(foodPrefab, SpawnFood(), Quaternion.identity);
            foodList.Add(spawnedFood);
        }
    }

    private Vector3 SpawnFood()
    {
        Vector3 spawnPosition = new Vector3(465.72f, 17f, Random.Range(261f, 269f));
        return spawnPosition;
    }

    public void RemoveFood(GameObject food)
    {
        foodList.Remove(food);
        Destroy(food);
    }
}

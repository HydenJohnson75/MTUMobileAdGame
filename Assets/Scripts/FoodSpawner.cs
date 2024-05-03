using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    
    private List<GameObject> foodList = new List<GameObject>();
    private GameManager gameManager;
    [SerializeField]
    private Mesh appleMesh;
    [SerializeField]
    private Mesh cherryMesh;
    [SerializeField]
    private Mesh strawberryMesh;
    [SerializeField]
    private Mesh avocadoMesh;
    [SerializeField]
    private Mesh bananaMesh;
    [SerializeField]
    private Mesh watermelonMesh;
    [SerializeField]
    private Mesh pearMesh;
    [SerializeField]
    private Mesh peachMesh;
    [SerializeField]
    private Mesh lemonMesh;
    [SerializeField]
    private Mesh cookieMesh;
    [SerializeField]
    private Mesh burgerMesh;
    [SerializeField]
    private Mesh donutMesh;
    private GameObject spawnedFood;

    private long foodValue;
    private long foodRewardCoins;
    private long foodScore;


    public List<FoodMesh>FoodMeshes = new List<FoodMesh>();

    public class FoodMesh
    {
        public Mesh _foodMesh;
        public string foodName;
        public bool unlocked;
    }

    private void Awake()
    {
        FoodMesh apple = new FoodMesh();
        apple._foodMesh = appleMesh;
        apple.foodName = "Apple";

        FoodMesh cherry = new FoodMesh();
        cherry._foodMesh = cherryMesh;
        cherry.foodName = "Cherry";

        FoodMesh strawberry = new FoodMesh();
        strawberry._foodMesh = strawberryMesh;
        strawberry.foodName = "Strawberry";

        FoodMesh avocado = new FoodMesh();
        avocado._foodMesh = avocadoMesh;
        avocado.foodName = "Avocado";

        FoodMesh banana = new FoodMesh();
        banana._foodMesh = bananaMesh;
        banana.foodName = "Banana";

        FoodMesh watermelon = new FoodMesh();
        watermelon._foodMesh = watermelonMesh;
        watermelon.foodName = "Watermelon";

        FoodMesh pear = new FoodMesh();
        pear._foodMesh = pearMesh;
        pear.foodName = "Pear";

        FoodMesh peach = new FoodMesh();
        peach._foodMesh = peachMesh;
        peach.foodName = "Peach";

        FoodMesh lemon = new FoodMesh();
        lemon._foodMesh = lemonMesh;
        lemon.foodName = "Lemon";

        FoodMesh cookie = new FoodMesh();
        cookie._foodMesh = cookieMesh;
        cookie.foodName = "Cookie";

        FoodMesh burger = new FoodMesh();
        burger._foodMesh = burgerMesh;
        burger.foodName = "Burger";

        FoodMesh donut = new FoodMesh();
        donut._foodMesh = donutMesh;
        donut.foodName = "Donut";

        FoodMeshes.Add(apple);
        FoodMeshes.Add(cherry);
        FoodMeshes.Add(strawberry);
        FoodMeshes.Add(avocado);
        FoodMeshes.Add(banana);
        FoodMeshes.Add(watermelon);
        FoodMeshes.Add(pear);
        FoodMeshes.Add(peach);
        FoodMeshes.Add(lemon);
        FoodMeshes.Add(cookie);
        FoodMeshes.Add(burger);
        FoodMeshes.Add(donut);
        spawnedFood = Instantiate(foodPrefab, SpawnFood(), Quaternion.identity);
        foodList.Add(spawnedFood);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();




    }

    // Update is called once per frame
    void Update()
    {
        if (foodList.Count < 1 && !gameManager.gamePaused)
        {
            spawnedFood = Instantiate(foodPrefab, SpawnFood(), Quaternion.identity);
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

    public long GetFoodCoins()
    {
        return foodValue;
    }

    public long GetFoodRewardCoins()
    {
        return foodRewardCoins;
    }

    public long GetFoodScore()
    {
        return foodScore;
    }   

    public void ChangeMesh(string foodName)
    {

        foreach(FoodMesh foodMesh in FoodMeshes)
        {
            if (foodMesh.foodName == foodName)
            {
                foodPrefab.GetComponent<MeshFilter>().mesh = foodMesh._foodMesh;
                SetCoinsScore(foodName);
            }
        }
    }


    public void SetCoinsScore(string foodName)
    {
        switch (foodName)
        {
            case "Apple":
                foodValue = 5;
                foodScore = 1;
                foodRewardCoins = foodValue * 3;
                break;
            case "Cherry":
                foodValue = 87;
                foodScore = 5;
                foodRewardCoins = foodValue * 3;
                break;
            case "Strawberry":
                foodValue = 888;
                foodScore = 7;
                foodRewardCoins = foodValue * 3;
                break;
            case "Avocado":
                foodValue = 6543;
                foodScore = 10;
                foodRewardCoins = foodValue * 3;
                break;
            case "Banana":
                foodValue = 45899;
                foodScore = 12;
                foodRewardCoins = foodValue * 3;
                break;
            case "Watermelon":
                foodValue = 794594;
                foodScore = 20;
                foodRewardCoins = foodValue * 3;
                break;
            case "Pear":
                foodValue = 832563;
                foodScore = 22;
                foodRewardCoins = foodValue * 3;
                break;
            case "Peach":
                foodValue = 5225776;
                foodScore = 23;
                foodRewardCoins = foodValue * 3;
                break;
            case "Lemon":
                foodValue = 8234567;
                foodScore = 30;
                foodRewardCoins = foodValue * 3;
                break;
            case "Cookie":
                foodValue = 20000000;
                foodScore = 50;
                foodRewardCoins = foodValue * 3;
                break;
            case "Burger":
                foodValue = 40000000;
                foodScore = 100;
                foodRewardCoins = foodValue * 3;
                break;
            case "Donut":
                foodValue = 999999999;
                foodScore = 300;
                foodRewardCoins = foodValue * 3;
                break;
        }
    }
}

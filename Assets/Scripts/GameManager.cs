using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IStoreListener
{
    private LevelPlayScript levelPlayScript;
    private GoogleAdMobScript googleAdMobScript;
    private GoogleSignInManager googleSignInManager;
    [SerializeField]
    private FoodSpawner foodSpawner;
    public bool gamePaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;
    [SerializeField]
    private TMP_Text coinsText;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private PostProcessVolume camPPV;

    [SerializeField]
    private Sprite inactiveCircle;
    [SerializeField]
    private Sprite activeCircle;
    [SerializeField]
    private GameObject firstSelector;
    [SerializeField]
    private GameObject secondSelector;
    [SerializeField]
    private GameObject thirdSelector;
    [SerializeField]
    private GameObject fourthSelector;

    [SerializeField]
    private GameObject firstHolder;
    [SerializeField]
    private GameObject secondHolder;
    [SerializeField]
    private GameObject thirdHolder;
    [SerializeField]
    private GameObject fourthHolder;
    [SerializeField]
    private AchievementScript achievementManager;
    [SerializeField]
    private LeaderboardScript leaderboardManager;

    private List<ItemPageSelector> itemPages = new List<ItemPageSelector>();

    [SerializeField]
    private GameObject leftArrowSelect;
    [SerializeField]
    private GameObject rightArrowSelect;

    private int currentSelectorIndex;

    private bool isProcessing = false;
    private bool isFocus = false;



    private class ItemPageSelector
    {
        public int index;
        public GameObject selectionImg;
        public GameObject pageHolder;
    }

    private long coins;
    private long highScore;
    private long currentScore = 0;

    private int avocadoUnlocked;
    private int appleUnlocked;
    private int bananaUnlocked;
    private int watermelonUnlocked;
    private int strawberryUnlocked;
    private int cherryUnlocked;
    private int peachUnlocked;
    private int pearUnlocked;
    private int lemonUnlocked;
    private int CookieUnlocked;
    private int DonutUnlocked;
    private int BurgerUnlocked;

    private int isAvocadoSelected;
    private int isAppleSelected;
    private int isBananaSelected;
    private int isWatermelonSelected;
    private int isStrawberrySelected;
    private int isCherrySelected;
    private int isPeachSelected;
    private int isPearSelected;
    private int isLemonSelected;
    private int isCookieSelected;
    private int isDonutSelected;
    private int isBurgerSelected;


    [SerializeField]
    private Button AppleUseButton;
    [SerializeField]
    private Button AppleSelectedButton;
    [SerializeField]
    private Button CherryBuyButton;
    [SerializeField]
    private Button CherryUseButton;
    [SerializeField]
    private Button CherrySelectedButton;
    [SerializeField]
    private Button StrawberryBuyButton;
    [SerializeField]
    private Button StrawberryUseButton;
    [SerializeField]
    private Button StrawberrySelectedButton;
    [SerializeField]
    private Button AvocadoBuyButton;
    [SerializeField]
    private Button AvocadoUseButton;
    [SerializeField]
    private Button AvocadoSelectedButton;
    [SerializeField]
    private Button BananaBuyButton;
    [SerializeField]
    private Button BananaUseButton;
    [SerializeField]
    private Button BananaSelectedButton;
    [SerializeField]
    private Button WatermelonBuyButton;
    [SerializeField]
    private Button WatermelonUseButton;
    [SerializeField]
    private Button WatermelonSelectedButton;
    [SerializeField]
    private Button PearBuyButton;
    [SerializeField]
    private Button PearUseButton;
    [SerializeField]
    private Button PearSelectedButton;
    [SerializeField]
    private Button PeachBuyButton;
    [SerializeField]
    private Button PeachUseButton;
    [SerializeField]
    private Button PeachSelectedButton;
    [SerializeField]
    private Button LemonBuyButton;
    [SerializeField]
    private Button LemonUseButton;
    [SerializeField]
    private Button LemonSelectedButton;
    [SerializeField]
    private Button CookieBuyButton;
    [SerializeField]
    private Button CookieUseButton;
    [SerializeField]
    private Button CookieSelectedButton;
    [SerializeField]
    private Button BurgerBuyButton;
    [SerializeField]
    private Button BurgerUseButton;
    [SerializeField]
    private Button BurgerSelectedButton;
    [SerializeField]
    private Button DonutBuyButton;
    [SerializeField]
    private Button DonutUseButton;
    [SerializeField]
    private Button DonutSelectedButton;

    public FoodProduct cookie;
    public FoodProduct burger;
    public FoodProduct donut;

    IStoreController storeController;

    [System.Serializable]
    public class FoodProduct
    {
        public string Name;
        public string Id;
        public string desc;
        public float price;
    }

    private void Awake()
    {
        

        //SaveLoadGame(false); 
    }

    private bool ConvertIntToBool(int value)
    {
        if(value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int ConvertBoolToInt(bool value) 
    { 
        if (value)
        {
            return 1; 
        } 
        else
        {
            return 0; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        googleSignInManager = FindObjectOfType<GoogleSignInManager>();

        SetupBuilder();

        CheckForReciept(cookie.Id);
        CheckForReciept(burger.Id);
        CheckForReciept(donut.Id);

        Debug.Log(googleSignInManager.loadedGameData.coins);

        coins = googleSignInManager.loadedGameData.coins;

        Debug.Log("GAME MAN COINS: " + coins.ToString());
        Debug.Log("GOOGLE COINS: " + googleSignInManager.loadedGameData.coins.ToString());
        coinsText.text = coins.ToString();
        highScore = googleSignInManager.loadedGameData.highScore;

        appleUnlocked = googleSignInManager.loadedGameData.appleUnlocked;
        avocadoUnlocked = googleSignInManager.loadedGameData.avocadoUnlocked;
        bananaUnlocked = googleSignInManager.loadedGameData.bananaUnlocked;
        watermelonUnlocked = googleSignInManager.loadedGameData.watermelonUnlocked;
        strawberryUnlocked = googleSignInManager.loadedGameData.strawberryUnlocked;
        cherryUnlocked = googleSignInManager.loadedGameData.cherryUnlocked;
        peachUnlocked = googleSignInManager.loadedGameData.peachUnlocked;
        pearUnlocked = googleSignInManager.loadedGameData.pearUnlocked;
        lemonUnlocked = googleSignInManager.loadedGameData.lemonUnlocked;
        CookieUnlocked = googleSignInManager.loadedGameData.CookieUnlocked;
        DonutUnlocked = googleSignInManager.loadedGameData.DonutUnlocked;
        BurgerUnlocked = googleSignInManager.loadedGameData.BurgerUnlocked;

        isAppleSelected = googleSignInManager.loadedGameData.isAppleSelected;
        isAvocadoSelected = googleSignInManager.loadedGameData.isAvocadoSelected;
        isBananaSelected = googleSignInManager.loadedGameData.isBananaSelected;
        isWatermelonSelected = googleSignInManager.loadedGameData.isWatermelonSelected;
        isStrawberrySelected = googleSignInManager.loadedGameData.isStrawberrySelected;
        isCherrySelected = googleSignInManager.loadedGameData.isCherrySelected;
        isPeachSelected = googleSignInManager.loadedGameData.isPeachSelected;
        isPearSelected = googleSignInManager.loadedGameData.isPearSelected;
        isLemonSelected = googleSignInManager.loadedGameData.isLemonSelected;
        isCookieSelected = googleSignInManager.loadedGameData.isCookieSelected;
        isDonutSelected = googleSignInManager.loadedGameData.isDonutSelected;
        isBurgerSelected = googleSignInManager.loadedGameData.isBurgerSelected;
        isAppleSelected = 1;
        currentSelectorIndex = 0;
        levelPlayScript = FindObjectOfType<LevelPlayScript>();
        googleAdMobScript = FindObjectOfType<GoogleAdMobScript>();
        ItemPageSelector firstPage = new ItemPageSelector();
        firstPage.index = 0;
        firstPage.selectionImg = firstSelector;
        firstPage.pageHolder = firstHolder;

        ItemPageSelector secondPage = new ItemPageSelector();
        secondPage.index = 1;
        secondPage.selectionImg = secondSelector;
        secondPage.pageHolder = secondHolder;

        ItemPageSelector thirdPage = new ItemPageSelector();
        thirdPage.index = 2;
        thirdPage.selectionImg = thirdSelector;
        thirdPage.pageHolder = thirdHolder;

        ItemPageSelector fourthPage = new ItemPageSelector();
        fourthPage.index = 3;
        fourthPage.selectionImg = fourthSelector;
        fourthPage.pageHolder = fourthHolder;

        itemPages.Add(firstPage);
        itemPages.Add(secondPage);
        itemPages.Add(thirdPage);
        itemPages.Add(fourthPage);

        Debug.Log("CHERRY UNLOCKED: " + cherryUnlocked.ToString());
        Debug.Log("CHERRY MAIN UNLOCKED: " + googleSignInManager.loadedGameData.cherryUnlocked.ToString());

        achievementManager.GrantAchievement(GPGSIds.achievement_an_apple_a_day);

        if (cherryUnlocked == 1)
        {
            CherryBuyButton.gameObject.SetActive(false);

        }
        if(strawberryUnlocked == 1)
        {
            StrawberryBuyButton.gameObject.SetActive(false);
        }
        if(avocadoUnlocked == 1)
        {
            AvocadoBuyButton.gameObject.SetActive(false);
        }
        if(bananaUnlocked == 1)
        {
            BananaBuyButton.gameObject.SetActive(false);
        }
        if(watermelonUnlocked == 1)
        {
            WatermelonBuyButton.gameObject.SetActive(false);
        }
        if(pearUnlocked == 1)
        {
            PearBuyButton.gameObject.SetActive(false);
        }
        if(peachUnlocked == 1)
        {
            PeachBuyButton.gameObject.SetActive(false);
        }
        if(lemonUnlocked == 1)
        {
            LemonBuyButton.gameObject.SetActive(false);
        }
        if(CookieUnlocked == 1)
        {
            CookieBuyButton.gameObject.SetActive(false);
        }
        if(BurgerUnlocked == 1)
        {
            BurgerBuyButton.gameObject.SetActive(false);
        }
        if(DonutUnlocked == 1)
        {
            DonutBuyButton.gameObject.SetActive(false);
        }

        if(isAppleSelected == 1)
        {
            AppleSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);

            CherrySelectedButton.gameObject.SetActive(false);
            StrawberrySelectedButton.gameObject.SetActive(false);
            AvocadoSelectedButton.gameObject.SetActive(false);
            BananaSelectedButton.gameObject.SetActive(false);
            WatermelonSelectedButton.gameObject.SetActive(false);
            PearSelectedButton.gameObject.SetActive(false);
            PeachSelectedButton.gameObject.SetActive(false);
            LemonSelectedButton.gameObject.SetActive(false);
            CookieSelectedButton.gameObject.SetActive(false);
            BurgerSelectedButton.gameObject.SetActive(false);
            DonutSelectedButton.gameObject.SetActive(false);
            foodSpawner.ChangeMesh("Apple");
        }
        if(isCherrySelected == 1)
        {
            CherrySelectedButton.gameObject.SetActive(true);
            AppleUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);

            AppleSelectedButton.gameObject.SetActive(false);
            StrawberrySelectedButton.gameObject.SetActive(false);
            AvocadoSelectedButton.gameObject.SetActive(false);
            BananaSelectedButton.gameObject.SetActive(false);
            WatermelonSelectedButton.gameObject.SetActive(false);
            PearSelectedButton.gameObject.SetActive(false);
            PeachSelectedButton.gameObject.SetActive(false);
            LemonSelectedButton.gameObject.SetActive(false);
            CookieSelectedButton.gameObject.SetActive(false);
            BurgerSelectedButton.gameObject.SetActive(false);
            DonutSelectedButton.gameObject.SetActive(false);
            foodSpawner.ChangeMesh("Cherry");
        }
        if(isStrawberrySelected == 1)
        {
            StrawberrySelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Strawberry");
        }
        if(isAvocadoSelected == 1)
        {
            AvocadoSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Avocado");
        }
        if(isBananaSelected == 1)
        {
            BananaSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Banana");
        }
        if(isWatermelonSelected == 1)
        {
            WatermelonSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Watermelon");
        }
        if(isPearSelected == 1)
        {
            PearSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Pear");
        }
        if(isPeachSelected == 1)
        {
            PeachSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Peach");
        }
        if(isLemonSelected == 1)
        {
            LemonSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Lemon");

        }
        if(isCookieSelected == 1)
        {
            CookieSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Cookie");
        }
        if(isBurgerSelected == 1)
        {
            BurgerSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            DonutUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Burger");
        }
        if(isDonutSelected == 1)
        {
            DonutSelectedButton.gameObject.SetActive(true);
            CherryUseButton.gameObject.SetActive(true);
            StrawberryUseButton.gameObject.SetActive(true);
            AvocadoUseButton.gameObject.SetActive(true);
            BananaUseButton.gameObject.SetActive(true);
            WatermelonUseButton.gameObject.SetActive(true);
            PearUseButton.gameObject.SetActive(true);
            PeachUseButton.gameObject.SetActive(true);
            LemonUseButton.gameObject.SetActive(true);
            CookieUseButton.gameObject.SetActive(true);
            BurgerUseButton.gameObject.SetActive(true);
            foodSpawner.ChangeMesh("Donut");
        }


    }

    // Update is called once per frame
    void Update()
    {

        if(coins >= 10)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_i_smell_money);
        }
        if(coins >= 100)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_100_smackeroos);
        }
        if(coins >= 1000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_aint_it_grand);
        }
        if(coins >= 10000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_stacking_up);
        }
        if(coins >= 100000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_a_100_big_ones);
        }
        if(coins >= 1000000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_a_small_loan);
        }
        if(coins >= 10000000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_now_were_talking_money);
        }
        if(coins >= 100000000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_youre_insane);
        }
        if(coins >= 1000000000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_can_i_have_loan);
        }
        if(coins >= 10000000000)
        {
            achievementManager.GrantAchievement(GPGSIds.achievement_might_as_well_retire);
        }
    }

    private void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(cookie.Id, ProductType.NonConsumable);
        builder.AddProduct(burger.Id, ProductType.NonConsumable);
        builder.AddProduct(donut.Id, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    private void CheckForReciept(string id)
    {
        if(storeController !=null)
        {
            var product = storeController.products.WithID(id);
            if(product != null)
            {
                if(product.hasReceipt)
                {
                    if(product.transactionID == cookie.Id)
                    {
                        CookieBuyButton.gameObject.SetActive(false);
                    }
                    if (product.transactionID == burger.Id)
                    {
                        BurgerBuyButton.gameObject.SetActive(false);
                    }
                    if (product.transactionID == donut.Id)
                    {
                        DonutBuyButton.gameObject.SetActive(false);
                    }
                }         
            }
        }
    }

    public void ShareTextInAnroid()
    {

        string message = "I just scored 5 on Food Fetchers";
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        currentActivity.Call("startActivity", intentObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            highScore = currentScore;

            leaderboardManager.AddHighScoreToLeaderboard(GPGSIds.leaderboard_high_scores,(int)highScore);

            SetAllSaveData();
            
            googleSignInManager.SaveLoadGame(true);

            currentScore = 0;

            float randomNum = RandomNumber();
            if (randomNum < 0.5f)
            {
                googleAdMobScript.LoadInterstitial();
                
            }
            if (randomNum >= 0.5f)
            {
                levelPlayScript.LoadInterAd();
            }
            
            gamePaused = true;
            foodSpawner.RemoveFood(collision.gameObject);
        }
    }

    public void IncreaseCoins()
    {

        Debug.Log("COINS AMOUNT: " + foodSpawner.GetFoodCoins());
        coins += foodSpawner.GetFoodCoins();

        Debug.Log("COINS : " + coins);
        coinsText.text = coins.ToString();
    }

    public void RewardCoins()
    {
        coins += foodSpawner.GetFoodRewardCoins();
        coinsText.text = coins.ToString();
    }

    public void IncreaseScore()
    {
        currentScore += foodSpawner.GetFoodScore();
    }   

    public void ShowRewardAd()
    {
        float randomNum = RandomNumber();
        if (randomNum < 0.5f)
        {
            googleAdMobScript.LoadReward();

        }
        if (randomNum >= 0.5f)
        {
            levelPlayScript.ShowRewardAd();
        }
    }

    public void SwitchPageRight()
    {
        if(currentSelectorIndex+1 == 3)
        {
            rightArrowSelect.SetActive(false);
            leftArrowSelect.SetActive(true);
        }

        leftArrowSelect.SetActive(true);
        ItemPageSelector currentPage = itemPages[currentSelectorIndex];

        currentPage.selectionImg.GetComponent<Image>().sprite = inactiveCircle;
        currentPage.pageHolder.SetActive(false);

        ItemPageSelector nextPage = itemPages[currentSelectorIndex + 1];

        nextPage.selectionImg.GetComponent<Image>().sprite = activeCircle;
        nextPage.pageHolder.SetActive(true);
        currentSelectorIndex = nextPage.index;

    }

    public void SwitchPageleft()
    {
        if (currentSelectorIndex -1 == 0)
        {
            rightArrowSelect.SetActive(true);
            leftArrowSelect.SetActive(false);
        }
        rightArrowSelect.SetActive(true);

        ItemPageSelector currentPage = itemPages[currentSelectorIndex];

        currentPage.selectionImg.GetComponent<Image>().sprite = inactiveCircle;
        currentPage.pageHolder.SetActive(false);

        ItemPageSelector nextPage = itemPages[currentSelectorIndex - 1];

        nextPage.selectionImg.GetComponent<Image>().sprite = activeCircle;
        nextPage.pageHolder.SetActive(true);
        currentSelectorIndex = nextPage.index;
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            playButton.SetActive(true);
            pauseButton.SetActive(false);
            camPPV.enabled = true;
            firstSelector.GetComponent<Image>().sprite = activeCircle;
            if(currentSelectorIndex == 0)
            {
                leftArrowSelect.SetActive(false);
                rightArrowSelect.SetActive(true);
            }
            if(currentSelectorIndex == 3)
            {
                rightArrowSelect.SetActive(false);
                leftArrowSelect.SetActive(true);
            }
        }
        else
        {
            pauseMenu.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            playButton.SetActive(false);
            pauseButton.SetActive(true);
            camPPV.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void UnlockFood(string foodName)
    {
        switch(foodName)
        {
            case "Cherry":
                if (coins >= 100)
                {
                    cherryUnlocked = 1;
                    coins -= 100;
                    coinsText.text = coins.ToString();
                    CherryBuyButton.gameObject.SetActive(false);
                    CherryUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_a_cherry_on_top);
                }
                break;
            case "Strawberry":
                if (coins >= 1000)
                {
                    strawberryUnlocked = 1;
                    coins -= 1000;
                    coinsText.text = coins.ToString();
                    StrawberryBuyButton.gameObject.SetActive(false);
                    StrawberryUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_very_berry);
                }
                break;
            case "Avocado":
                if(coins >= 10000)
                {
                    avocadoUnlocked = 1;
                    coins -= 10000;
                    coinsText.text = coins.ToString();
                    AvocadoBuyButton.gameObject.SetActive(false);
                    AvocadoUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_holy_guacamole);
                }
                break;
            case "Banana":
                if(coins >= 100000)
                {
                    bananaUnlocked = 1;
                    coins -= 100000;
                    coinsText.text = coins.ToString();
                    BananaBuyButton.gameObject.SetActive(false);
                    BananaUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_dont_slip);
                }
                break;
            case "Watermelon":
                if(coins >= 1000000)
                {
                    watermelonUnlocked = 1;
                    coins -= 1000000;
                    coinsText.text = coins.ToString();
                    WatermelonBuyButton.gameObject.SetActive(false);
                    WatermelonUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_water_meloooooonnnnnnnnn);
                }
                break;
            case "Pear":
                if (coins >= 10000000)
                {
                    pearUnlocked = 1;
                    coins -= 10000000;
                    coinsText.text = coins.ToString();
                    PearBuyButton.gameObject.SetActive(false);
                    PearUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_a_pair_of_pears);
                }
                break;
            case "Peach":
                if(coins >= 100000000)
                {
                    peachUnlocked = 1;
                    coins -= 100000000;
                    coinsText.text = coins.ToString();
                    PeachBuyButton.gameObject.SetActive(false);
                    PeachUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_peachy);
                }
                break;
            case "Lemon":
                if (coins >= 1000000000)
                {
                    lemonUnlocked = 1;
                    coins -= 1000000000;
                    coinsText.text = coins.ToString();
                    LemonBuyButton.gameObject.SetActive(false);
                    LemonUseButton.gameObject.SetActive(true);
                    achievementManager.GrantAchievement(GPGSIds.achievement_when_life_gives_you_lemons);
                }
                break;
            case "Cookie":
                storeController.InitiatePurchase(cookie.Id);
                break;
            case "Burger":
                storeController.InitiatePurchase(burger.Id);
                break;
            case "Donut":
                storeController.InitiatePurchase(donut.Id);
                break;
        }
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        if(product.definition.id == cookie.Id)
        {
            CookieUnlocked = 1;
            CookieBuyButton.gameObject.SetActive(false);
            CookieUseButton.gameObject.SetActive(true);

            SetAllSaveData();
            googleSignInManager.SaveLoadGame(true);
        }

        if(product.definition.id == burger.Id)
        {
            BurgerUnlocked = 1;
            BurgerBuyButton.gameObject.SetActive(false);
            BurgerUseButton.gameObject.SetActive(true);
            googleSignInManager.SaveLoadGame(true);

            SetAllSaveData();
            googleSignInManager.SaveLoadGame(true);
        }

        if(product.definition.id == donut.Id)
        {
            DonutUnlocked = 1;
            DonutBuyButton.gameObject.SetActive(false);
            DonutUseButton.gameObject.SetActive(true);
            googleSignInManager.SaveLoadGame(true);

            SetAllSaveData();
            googleSignInManager.SaveLoadGame(true);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void ChangeFood(string foodName)
    {
        switch (foodName)
        {
            case "Apple":
                isAppleSelected = 1;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Apple");

                AppleUseButton.gameObject.SetActive(false);
                AppleSelectedButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Cherry":
                isCherrySelected = 1;
                isAppleSelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Cherry");

                CherryUseButton.gameObject.SetActive(false);
                CherrySelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Strawberry":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 1;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Strawberry");

                StrawberryUseButton.gameObject.SetActive(false);
                StrawberrySelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Avocado":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 1;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Avocado");

                AvocadoUseButton.gameObject.SetActive(false);
                AvocadoSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Banana":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 1;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Banana");

                BananaUseButton.gameObject.SetActive(false);
                BananaSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Watermelon":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 1;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Watermelon");

                WatermelonUseButton.gameObject.SetActive(false);
                WatermelonSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Pear":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 1;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Pear");

                PearUseButton.gameObject.SetActive(false);
                PearSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Peach":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 1;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Peach");

                PeachUseButton.gameObject.SetActive(false);
                PeachSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Lemon":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 1;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Lemon");

                LemonUseButton.gameObject.SetActive(false);
                LemonSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Cookie":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 1;
                isBurgerSelected = 0;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Cookie");

                CookieUseButton.gameObject.SetActive(false);
                CookieSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Burger":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 1;
                isDonutSelected = 0;
                foodSpawner.ChangeMesh("Burger");

                BurgerUseButton.gameObject.SetActive(false);
                BurgerSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                DonutUseButton.gameObject.SetActive(true);
                break;
            case "Donut":
                isAppleSelected = 0;
                isCherrySelected = 0;
                isStrawberrySelected = 0;
                isAvocadoSelected = 0;
                isBananaSelected = 0;
                isWatermelonSelected = 0;
                isPearSelected = 0;
                isPeachSelected = 0;
                isLemonSelected = 0;
                isCookieSelected = 0;
                isBurgerSelected = 0;
                isDonutSelected = 1;
                foodSpawner.ChangeMesh("Donut");

                DonutUseButton.gameObject.SetActive(false);
                DonutSelectedButton.gameObject.SetActive(true);
                AppleUseButton.gameObject.SetActive(true);
                CherryUseButton.gameObject.SetActive(true);
                StrawberryUseButton.gameObject.SetActive(true);
                AvocadoUseButton.gameObject.SetActive(true);
                BananaUseButton.gameObject.SetActive(true);
                WatermelonUseButton.gameObject.SetActive(true);
                PearUseButton.gameObject.SetActive(true);
                PeachUseButton.gameObject.SetActive(true);
                LemonUseButton.gameObject.SetActive(true);
                CookieUseButton.gameObject.SetActive(true);
                BurgerUseButton.gameObject.SetActive(true);
                break;
        }
    }

    private void SetAllSaveData()
    {
        googleSignInManager.savedGameData.coins = coins;
        googleSignInManager.savedGameData.highScore = highScore;
        googleSignInManager.savedGameData.appleUnlocked = appleUnlocked;
        googleSignInManager.savedGameData.cherryUnlocked = cherryUnlocked;
        googleSignInManager.savedGameData.strawberryUnlocked = strawberryUnlocked;
        googleSignInManager.savedGameData.avocadoUnlocked = avocadoUnlocked;
        googleSignInManager.savedGameData.bananaUnlocked = bananaUnlocked;
        googleSignInManager.savedGameData.watermelonUnlocked = watermelonUnlocked;
        googleSignInManager.savedGameData.pearUnlocked = pearUnlocked;
        googleSignInManager.savedGameData.peachUnlocked = peachUnlocked;
        googleSignInManager.savedGameData.lemonUnlocked = lemonUnlocked;
        googleSignInManager.savedGameData.CookieUnlocked = CookieUnlocked;
        googleSignInManager.savedGameData.BurgerUnlocked = BurgerUnlocked;
        googleSignInManager.savedGameData.DonutUnlocked = DonutUnlocked;

        googleSignInManager.savedGameData.isAppleSelected = isAppleSelected;
        googleSignInManager.savedGameData.isCherrySelected = isCherrySelected;
        googleSignInManager.savedGameData.isStrawberrySelected = isStrawberrySelected;
        googleSignInManager.savedGameData.isAvocadoSelected = isAvocadoSelected;
        googleSignInManager.savedGameData.isBananaSelected = isBananaSelected;
        googleSignInManager.savedGameData.isWatermelonSelected = isWatermelonSelected;
        googleSignInManager.savedGameData.isPearSelected = isPearSelected;
        googleSignInManager.savedGameData.isPeachSelected = isPeachSelected;
        googleSignInManager.savedGameData.isLemonSelected = isLemonSelected;
        googleSignInManager.savedGameData.isCookieSelected = isCookieSelected;
        googleSignInManager.savedGameData.isBurgerSelected = isBurgerSelected;
        googleSignInManager.savedGameData.isDonutSelected = isDonutSelected;
    }

    private float RandomNumber()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }

    public void SaveLoadGame(bool isSaving)
    {
        //googleSignInManager.OpenSave(isSaving);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }



    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchases Failed");
    }


}

using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;

public class GoogleSignInManager : MonoBehaviour
{

    public bool isSignedIn;
    private bool isSaving;
    private string fileName = "NewSavedData";
    private bool isLoading;
    public UserGameData loadedGameData = new UserGameData();
    public UserGameData savedGameData = new UserGameData();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        savedGameData.coins = 0;
        savedGameData.highScore = 0;
        savedGameData.cherryUnlocked = 0;
        savedGameData.strawberryUnlocked = 0;
        savedGameData.avocadoUnlocked = 0;
        savedGameData.bananaUnlocked = 0;
        savedGameData.watermelonUnlocked = 0;
        savedGameData.pearUnlocked = 0;
        savedGameData.peachUnlocked = 0;
        savedGameData.lemonUnlocked = 0;
        savedGameData.CookieUnlocked = 0;
        savedGameData.BurgerUnlocked = 0;
        savedGameData.DonutUnlocked = 0;

        savedGameData.isAppleSelected = 1;
        savedGameData.isCherrySelected = 0;
        savedGameData.isStrawberrySelected = 0;
        savedGameData.isAvocadoSelected = 0;
        savedGameData.isBananaSelected = 0;
        savedGameData.isWatermelonSelected = 0;
        savedGameData.isPearSelected = 0;
        savedGameData.isPeachSelected = 0;
        savedGameData.isLemonSelected = 0;
        savedGameData.isCookieSelected = 0;
        savedGameData.isBurgerSelected = 0;
        savedGameData.isDonutSelected = 0;

        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void SaveLoadGame(bool _isSaving)
    {

        if (_isSaving)
        {
            isSaving = true;
        }
        else
        {
            isLoading = true;
        }

        OpenSavedGame(fileName);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            isSignedIn = true;

            SaveLoadGame(true);

            SaveLoadGame(false);
            
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            isSignedIn = false;
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now.ToString());
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Game saved successfully");

            isSaving = false;
            
        }
        else
        {
            Debug.Log("Error saving game: " + status);
            // handle error
        }
    }


    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }
    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved game opened");

            if(isSaving)
            {
                UserGameData data = savedGameData;
               

                string jsonString = JsonUtility.ToJson(data);
                byte[] savedData = Encoding.ASCII.GetBytes(jsonString);
                Debug.Log("DATA: " + savedData[0]);
                TimeSpan totalPlaytime = new TimeSpan(1, 0, 0);
                SaveGame(game, savedData, totalPlaytime);
                isSaving = false;
            }

            if(isLoading)
            {
                LoadGameData(game);
            }
        }
        else
        {
            Debug.Log("Error opening saved game");
        }
    }


    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Game Load Succefull");
            isLoading = false;
            Debug.Log("Data length: " + data.Length);

            string jsonString = Encoding.UTF8.GetString(data);

            // Deserialize the JSON string into an Object object
            loadedGameData = JsonUtility.FromJson<UserGameData>(jsonString);

            // Do something with the userData object
            Debug.Log("Coins: " + loadedGameData.coins);

            if(SceneManager.GetActiveScene().name == "LogInScene")
            {
                SceneManager.LoadScene("SampleScene");
            }

        }
        else
        {

            Debug.Log("Data length: " + data.Length);
            Debug.Log("Game Load failed");
            Debug.Log(status.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class UserGameData
    {
        public long coins = 0;
        public long highScore = 0;
        public int appleUnlocked = 1;
        public int cherryUnlocked = 0;
        public int strawberryUnlocked = 0;
        public int avocadoUnlocked = 0;
        public int bananaUnlocked = 0;
        public int watermelonUnlocked = 0;
        public int pearUnlocked = 0;
        public int peachUnlocked = 0;
        public int lemonUnlocked = 0;
        public int CookieUnlocked = 0;
        public int BurgerUnlocked = 0;
        public int DonutUnlocked = 0;

        public int isAppleSelected = 1;
        public int isCherrySelected = 0;
        public int isStrawberrySelected = 0;
        public int isAvocadoSelected = 0;
        public int isBananaSelected = 0;
        public int isWatermelonSelected = 0;
        public int isPearSelected = 0;
        public int isPeachSelected = 0;
        public int isLemonSelected = 0;
        public int isCookieSelected = 0;
        public int isBurgerSelected = 0;
        public int isDonutSelected = 0;
    }
}

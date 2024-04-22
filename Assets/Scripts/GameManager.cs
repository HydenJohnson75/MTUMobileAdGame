using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelPlayScript levelPlayScript;
    private FoodSpawner foodSpawner;
    public bool gamePaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;
    [SerializeField]
    private TMP_Text scoreText;

    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        foodSpawner = FindObjectOfType<FoodSpawner>();
        levelPlayScript = FindObjectOfType<LevelPlayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            levelPlayScript.LoadInterAd();
            gamePaused = true;
            foodSpawner.RemoveFood(collision.gameObject);
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
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
        }
        else
        {
            pauseMenu.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            Time.timeScale = 1;
        }
    }

}

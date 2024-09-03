using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToReloadScene;
    [SerializeField] private GameObject PauseMenuCanvas;



    [Space, SerializeField] private UnityEvent onStartGame;
    [SerializeField] private UnityEvent onGameOver, onIncreaseScore;
    private bool isPaused = false;


    public int score
    {
        get;
        private set;
    }

    public bool isGameOver
    {
        get;
        private set;
    }

    // Singleton!
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);
    }

    public void StartGame()
    {
        Debug.Log("GameManager :: StartGame()");

        onStartGame?.Invoke();





    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        Debug.Log("GameManager :: GameOver()");

        isGameOver = true;

        onGameOver?.Invoke();

        PlayerPrefs.SetInt("LastScore", score);

        if (score> PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2"));
            PlayerPrefs.SetInt("HighScore2", PlayerPrefs.GetInt("HighScore"));
            PlayerPrefs.SetInt("HighScore", score);
        }
        else
        {
            if (score > PlayerPrefs.GetInt("HighScore2"))
            {
                PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2"));
                PlayerPrefs.SetInt("HighScore2", score);
            }
            else
            {
                if (score > PlayerPrefs.GetInt("HighScore3"))
                {
                    PlayerPrefs.SetInt("HighScore3", score);
                }
            }
        }

        PlayerPrefs.Save();

        StartCoroutine(ReloadScene());
    }

    public void IncreaseScore()
    {
        Debug.Log("GameManager :: IncreaseScore()");

        score++;

        onIncreaseScore?.Invoke();
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(timeToReloadScene);

        Debug.Log("GameManager :: ReloadScene()");

        SceneManager.LoadScene(0);
    }

    public void PauseGame() //función de pausa
    {
        if (isPaused) //esto despausa
        {
            Time.timeScale = 1.0f;
            isPaused = false;
            PauseMenuCanvas.gameObject.SetActive(false); //entonces quitamos el pause canvas
        }
        else //esto pausa
        {
            Time.timeScale = 0.0f;
            isPaused = true;
            PauseMenuCanvas.SetActive(true); //entonces activamos el pause canvas


        }
    }




}

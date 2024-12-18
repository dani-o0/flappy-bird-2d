using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    STATE_MENU,
    STATE_PLAYING,
    STATE_GAMEOVER
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState gameState = GameState.STATE_MENU;
    private int score = 0;

    private GameObject objPlayer;
    
    private GameObject objMenu;
    private GameObject objPlaying;
    private GameObject objGameOver;

    // Menu screen
    private TMP_Text bestText;
    private Button btnPlay;
    
    // Playing screen
    private TMP_Text scoreText;
    
    // GameOver screen
    private TMP_Text endScoreText;
    private TMP_Text endBestText;
    private Button btnRestart;
    private Button btnMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateObjectReferences();
        
        switch (gameState)
        {
            case GameState.STATE_MENU:
            {
                objMenu.SetActive(true);
                objPlaying.SetActive(false);
                objGameOver.SetActive(false);
                
                objPlayer.SetActive(false);

                bestText.text = "Best: " + GetBestScore();
                
                break;
            }
            case GameState.STATE_PLAYING:
            {
                objMenu.SetActive(false);
                objPlaying.SetActive(true);
                objGameOver.SetActive(false);
                
                objPlayer.SetActive(true);
                
                break;
            }
            case GameState.STATE_GAMEOVER:
            {
                objMenu.SetActive(false);
                objPlaying.SetActive(false);
                objGameOver.SetActive(true);

                endScoreText.text = "Score: " + score;
                endBestText.text = "Best: " + GetBestScore();
                break;
            }
        }
    }

    public void StartPlaying()
    {
        gameState = GameState.STATE_PLAYING;
    }

    public void RestartGame()
    {
        gameState = GameState.STATE_PLAYING;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMenu()
    {
        gameState = GameState.STATE_MENU;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int iScore)
    {
        if (!IsPlaying())
            return;
        
        score += iScore;
        scoreText.text = score.ToString();
    }

    public void SetScore(int iScore)
    {
        score = iScore;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsPlaying()
    {
        return gameState == GameState.STATE_PLAYING;
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    private int GetBestScore()
    {
        int storedScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > storedScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            return score;
        }
        else
        {
            return storedScore;
        }
    }

    private void UpdateObjectReferences()
    {
        if (!objPlayer)
            objPlayer = GameObject.Find("Player");
        
        if (!objMenu)
            objMenu = GameObject.Find("MenuScreen");
        if (!objPlaying)
            objPlaying = GameObject.Find("PlayingScreen");
        if (!objGameOver)
            objGameOver = GameObject.Find("GameOverScreen");

        // Menu screen
        if (!bestText) 
            bestText = GameObject.Find("BestScore").GetComponent<TMP_Text>();
        if (!btnPlay)
            btnPlay = GameObject.Find("Play").GetComponent<Button>();
        if (!HasOnClickListeners(btnPlay))
            btnPlay.onClick.AddListener(StartPlaying);
        
        // Playing screen
        if (!scoreText)
            scoreText = objPlaying.GetComponentInChildren<TMP_Text>();
        
        // GameOver screen
        if (!endScoreText)
            endScoreText = GameObject.Find("EndScore").GetComponent<TMP_Text>();
        if (!endBestText)
            endBestText = GameObject.Find("EndBestScore").GetComponent<TMP_Text>();
        if (!btnRestart)
            btnRestart = GameObject.Find("Restart").GetComponent<Button>();
        if (!HasOnClickListeners(btnRestart))
            btnRestart.onClick.AddListener(RestartGame);
        if (!btnMenu)
            btnMenu = GameObject.Find("Menu").GetComponent<Button>();
        if (HasOnClickListeners(btnMenu))
            btnMenu.onClick.AddListener(ReturnMenu);
    }
    
    private bool HasOnClickListeners(Button button)
    {
        UnityEvent onClickEvent = button.onClick;

        return onClickEvent.GetPersistentEventCount() > 0;
    }
}
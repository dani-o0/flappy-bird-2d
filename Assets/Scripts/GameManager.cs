using TMPro;
using UnityEngine;
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

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip playMusic;

    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip looseClip;

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

    void Start()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }

    public bool playMenuMusic = true;
    public bool playPlayingMusic = false;
    public bool playEndAudio;
    
    void Update()
    {
        UpdateObjectReferences();

        if (playMenuMusic)
        {
            Invoke("PlayMenuMusic", 0.5f);
        }

        if (playPlayingMusic)
        {
            Invoke("PlayPlayingMusic", 0.5f);
        }
        
        switch (gameState)
        {
            case GameState.STATE_MENU:
            {
                objMenu.SetActive(true);
                objPlaying.SetActive(false);
                objGameOver.SetActive(false);
                
                objPlayer.SetActive(false);
                
                int bestScore = PlayerPrefs.GetInt("BestScore", 0);

                bestText.text = "Best: " + bestScore;
                
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
                
                int bestScore = PlayerPrefs.GetInt("BestScore", 0);

                if (score >= bestScore)
                {
                    if (playEndAudio)
                    {
                        SoundManager.Instance.PlaySfx(winClip);
                        playEndAudio = false;
                    }
                    PlayerPrefs.SetInt("BestScore", score);
                }
                else
                {
                    if (playEndAudio)
                    {
                        SoundManager.Instance.PlaySfx(looseClip);
                        playEndAudio = false;
                    }
                }

                endScoreText.text = "Score: " + score;
                endBestText.text = "Best: " + bestScore;
                
                break;
            }
        }
    }

    public void StartPlaying()
    {
        gameState = GameState.STATE_PLAYING;
        playPlayingMusic = true;
    }

    public void RestartGame()
    {
        gameState = GameState.STATE_PLAYING;
        score = 0;
        playPlayingMusic = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMenu()
    {
        gameState = GameState.STATE_MENU;
        score = 0;
        playMenuMusic = true;
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

    private void PlayMenuMusic()
    {
        SoundManager.Instance.PlayMusic(menuMusic);
        playMenuMusic = false;
    }

    private void PlayPlayingMusic()
    {
        SoundManager.Instance.PlayMusic(playMusic);
        playPlayingMusic = false;
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
        {
            btnPlay = GameObject.Find("Play").GetComponent<Button>();
            btnPlay.onClick.AddListener(delegate{StartPlaying();});
        }
        
        // Playing screen
        if (!scoreText)
            scoreText = objPlaying.GetComponentInChildren<TMP_Text>();
        
        // GameOver screen
        if (!endScoreText)
            endScoreText = GameObject.Find("EndScore").GetComponent<TMP_Text>();
        if (!endBestText)
            endBestText = GameObject.Find("EndBestScore").GetComponent<TMP_Text>();
        if (!btnRestart)
        {
            btnRestart = GameObject.Find("Restart").GetComponent<Button>();
            btnRestart.onClick.AddListener(delegate{RestartGame();});
        }
        if (!btnMenu)
        {
            btnMenu = GameObject.Find("Menu").GetComponent<Button>();
            btnMenu.onClick.AddListener(delegate{ReturnMenu();});
        }
    }
}
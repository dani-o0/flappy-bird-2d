using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    private bool isGameOver = false;

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

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            Debug.Log($"Puntuación: {score}");
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("¡Game Over!");
        // Mostrar pantalla de Game Over o reiniciar juego
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
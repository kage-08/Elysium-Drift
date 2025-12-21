using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int totalScore;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(EnemyRank rank)
    {
        totalScore += rank switch
        {
            EnemyRank.S => 100,
            EnemyRank.A => 50,
            EnemyRank.B => 20,
            _ => 10
        };
    }
}

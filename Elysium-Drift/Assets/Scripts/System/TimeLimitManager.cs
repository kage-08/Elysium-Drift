using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLimitManager : MonoBehaviour
{
    public float timeLimit = 180f;

    void Update()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0f)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}

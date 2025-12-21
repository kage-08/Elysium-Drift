using UnityEngine;

public class KillCountManager : MonoBehaviour
{
    public static KillCountManager Instance;
    public int killCount;

    void Awake()
    {
        Instance = this;
    }

    public void AddKill()
    {
        killCount++;
    }
}

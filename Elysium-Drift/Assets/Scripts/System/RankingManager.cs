using UnityEngine;
using System.IO;

[System.Serializable]
public class RankingEntry
{
    public int score;
}

public class RankingManager : MonoBehaviour
{
    string path => Application.persistentDataPath + "/ranking.json";

    public void Save(int score)
    {
        RankingEntry entry = new RankingEntry { score = score };
        File.WriteAllText(path, JsonUtility.ToJson(entry));
    }
}

using UnityEngine;

public enum EnemyRank { C, B, A, S }

public class EnemyStatus : MonoBehaviour
{
    public EnemyRank rank = EnemyRank.C;
    public int HP = 50;
    public int STR = 10;
}

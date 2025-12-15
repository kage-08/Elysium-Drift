using UnityEngine;

public class MiniMapPlayerArrow : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        if (!player) return;
        transform.localRotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
    }
}

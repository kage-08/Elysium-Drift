using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float height = 50f;

    void LateUpdate()
    {
        if (!player) return;

        Vector3 pos = player.position;
        pos.y += height;
        transform.position = pos;
    }
}

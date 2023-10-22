using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 Offset;
    public Vector2 SmoothTime = new(0.5f, 0.01f);

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector2 targetPosition = (Vector2)player.position + Offset;
        float x = Mathf.Lerp(transform.position.x, targetPosition.x, SmoothTime.x);
        float y = Mathf.Lerp(transform.position.y, targetPosition.y, SmoothTime.y);
        transform.position = new Vector3(x, y, -10);
    }
}

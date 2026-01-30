using UnityEngine;

public class RotateSword : MonoBehaviour
{
    public Transform player;
    public float radius = 1f;

    void Update()
    {
        if (player == null) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - player.position;
        dir.z = 0;
        transform.position = player.position + dir.normalized * radius;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

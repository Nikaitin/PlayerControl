using Unity.VisualScripting;
using UnityEngine;

public class RotateSword : MonoBehaviour
{
    public Transform player;
    public float radius = 1f;
    public float timer = 0;
    //timer for swinging cooldown
    public float swingtime = 0.3f;
    bool swinging = false;
    //swinging angle
    private float currentAngle;
    private bool swungLeft = true;

    // We need a variable to store the smoothed angle
    private float currentMouseAngle = 0f;

    void Update()
    {
        if (player == null) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - player.position;
        dir.z = 0;
        transform.position = player.position + dir.normalized * radius;

        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;
            timer = 0;
            swungLeft = swungLeft ? false : true;
        }

        if (swinging)
        {
            timer += Time.deltaTime;

            // Calculate percentage of swing completion (0 to 1)
            float t = timer / swingtime;

            // Animate from Start Angle to (Start Angle - 180)
            // Mathf.LerpAngle handles the math smoothly
            if (swungLeft)
            {
                currentAngle = Mathf.Lerp(0, -180f, t);
            }
            else
            {
                currentAngle = Mathf.Lerp(180f, 360f, t);
            }
            if (timer >= swingtime)
            {
                swinging = false;
            }
        }

        float targetMouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentMouseAngle = Mathf.LerpAngle(currentMouseAngle, targetMouseAngle, Time.deltaTime * 5f);

        transform.rotation = Quaternion.Euler(0, 0, currentAngle + currentMouseAngle);

    }
}

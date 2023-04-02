using UnityEngine;

public class GPSDirectionArrow : MonoBehaviour
{
    public GPSController gpsController;
    public Transform playerMarker;
    public Transform targetMarker;

    private void Update()
    {
        Vector2 playerPos = new Vector2(playerMarker.position.x, playerMarker.position.z);
        Vector2 targetPos = new Vector2(targetMarker.position.x, targetMarker.position.z);

        float angle = Mathf.Atan2(targetPos.y - playerPos.y, targetPos.x - playerPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}

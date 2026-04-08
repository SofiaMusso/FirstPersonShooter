using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public Transform player;
    public Transform pointer;
    private Vector3 initialRotation;

    private void Start()
    {
        initialRotation = pointer.eulerAngles;
    }

    private void LateUpdate()
    {
        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;
        transform.position = playerPos;

        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;

        if (forward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(forward);

            // Mantieni l'inclinazione originale (tipo X = 90)
            pointer.rotation = targetRotation * Quaternion.Euler(90f, 0f, 180f);
        }
    }
}

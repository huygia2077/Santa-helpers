using UnityEngine;
using Photon.Pun;

public class CameraBehaviour : MonoBehaviour
{
    public float smoothTime = 0.15f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private Transform target;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        FindLocalPlayer();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            FindLocalPlayer();
            return;
        }

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private void FindLocalPlayer()
    {
        foreach (PhotonView view in FindObjectsByType<PhotonView>(FindObjectsSortMode.None))
        {
            if (view.IsMine && view.GetComponent<Player>() != null)
            {
                target = view.transform;
                break;
            }
        }
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 cameraOffset;
    private Vector3 velocity=Vector3.zero;
    public float smoothTime=0.3F;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetedPosition = targetObject.transform.position+cameraOffset;
        transform.position=Vector3.SmoothDamp(transform.position,targetedPosition,ref velocity,smoothTime);
    }
}

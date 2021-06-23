using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public static CameraBehavior instance;

    private void Awake()
    {
        instance = this;
        transform.parent = null;
    }

    [Header("Setup")]
    public Transform target;
    public float cameraFollowSpeed;
    public Vector3 cameraTargetOffset;

    void FixedUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, cameraFollowSpeed) + cameraTargetOffset.x, target.transform.position.y + cameraTargetOffset.y, target.transform.position.z + cameraTargetOffset.z);
        }
    }

}

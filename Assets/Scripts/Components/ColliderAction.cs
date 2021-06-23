using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAction : MonoBehaviour
{
    [Header("Setup")]
    public GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (objectToEnable) objectToEnable.SetActive(true);
        }
    }
}

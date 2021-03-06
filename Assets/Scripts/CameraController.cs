using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public GameObject playerObject;

    void Update()
    {
        if (playerObject != null)
        {
            if (playerObject.gameObject.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(playerObject.gameObject.transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}

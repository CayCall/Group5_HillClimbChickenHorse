using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        var transformPosition = mainCamera.transform.position;
        transformPosition.x = player.transform.position.x;
        transformPosition.y = player.transform.position.y;

        mainCamera.transform.position = transformPosition;
    }
}

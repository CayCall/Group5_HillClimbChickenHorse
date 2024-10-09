using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
   [SerializeField] private GameObject UICanvas;

    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        var transformPosition = UICanvas.transform.position;
        transformPosition.x = player.transform.position.x;
        transformPosition.y = player.transform.position.y;

        UICanvas.transform.position = transformPosition;
    }
}

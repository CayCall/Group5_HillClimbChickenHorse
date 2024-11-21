using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositionTracker : MonoBehaviour
{
    [SerializeField] private Transform positionTracker;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.MovePosition(positionTracker.position);
    }
}

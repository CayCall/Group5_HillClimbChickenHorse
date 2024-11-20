using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBonParentObj : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            StartCoroutine(Gounded());
        }
    }

    private IEnumerator Gounded()
    {
        yield return new WaitForSeconds(1f);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

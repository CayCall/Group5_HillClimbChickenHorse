using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterControlerBasic : MonoBehaviour
    {
        public Rigidbody2D rB2D;
        public PlayerInputs pI;

        public float speed;
        public float maxAcceleration;

        private void Start()
        {
            rB2D = GetComponent<Rigidbody2D>();
            pI = GetComponent<PlayerInputs>();
        }

        private void Update()
        {
            if (rB2D.velocity.magnitude < speed)
            {
                rB2D.AddForce(new Vector2(pI.throttle * maxAcceleration * Time.deltaTime,0), ForceMode2D.Force);
            }
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CC
{
    public class CharacterControlerBasic : MonoBehaviour
    {
        public Rigidbody2D rB2D;
        public PlayerInputs pI;

        public float speed;
        public float maxAcceleration;
        public float wrongWheelMultiplier;
        
        public enum WheelPosition
        {
            Front,
            Back
        }

        public WheelPosition wheelPosition;

        private bool grounded;
        private void Start()
        {
            rB2D = GetComponent<Rigidbody2D>();
            pI = GetComponent<PlayerInputs>();
        }

        private void Update()
        {
            if (grounded)
            {
                switch (wheelPosition)
                {
                    case WheelPosition.Front:
                        if (rB2D.velocity.magnitude < speed && pI.throttle < 0)
                        {
                            rB2D.AddForce(new Vector2(pI.throttle * maxAcceleration * Time.deltaTime,0), ForceMode2D.Force);
                        }
                        else if (rB2D.velocity.magnitude < speed)
                        {
                            rB2D.AddForce(new Vector2(pI.throttle * wrongWheelMultiplier * maxAcceleration * Time.deltaTime,0), ForceMode2D.Force);
                        }
                        return;
                    case WheelPosition.Back:
                        if (rB2D.velocity.magnitude < speed && pI.throttle > 0)
                        {
                            rB2D.AddForce(new Vector2(pI.throttle * maxAcceleration * Time.deltaTime,0), ForceMode2D.Force);
                        }
                        else if (rB2D.velocity.magnitude < speed)
                        {
                            rB2D.AddForce(new Vector2(pI.throttle * wrongWheelMultiplier * maxAcceleration * Time.deltaTime,0), ForceMode2D.Force);
                        }
                        return;
                }
            }
            else
            {
                
            }
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            grounded = false;
        }
    }
}
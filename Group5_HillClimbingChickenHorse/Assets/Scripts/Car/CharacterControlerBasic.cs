using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CC
{
    public class CharacterControlerBasic : MonoBehaviour
    {
        public Rigidbody2D rB2D;
        public PlayerInputs pI;

        public float speed;
        public float maxAcceleration;
        public float wrongWheelMultiplier;
        public float rotAcc;
        public float rotationSpeed;  // Speed of rotation while in the air

        public GameObject centerObject;

        public enum WheelPosition
        {
            Front,
            Back
        }

        public WheelPosition wheelPosition;

        public bool grounded;

        private Gamepad pad;

        private Coroutine stopRumbleAfterCoroutine;

        private bool groundHit = false;
        private float groundHitRumble;

        private void Start()
        {
            rB2D = GetComponent<Rigidbody2D>();
            pI = GetComponent<PlayerInputs>();
        }

        private void Update()
        {
            Rumble(0, (pI.negative + pI.positive) / 2, 0.25f);
            if (grounded)
            {
                ApplyGroundForces();
                if (groundHit)
                {
                    groundHitRumble = 1f;
                    groundHit = false;
                }
            }
            else
            {
                RotateInAir();  // Rotate while in air
                groundHit = false;
                groundHitRumble = 0f;
            }
        }

        private void ApplyGroundForces()
        {
            switch (wheelPosition)
            {
                case WheelPosition.Front:
                    if (rB2D.velocity.magnitude < speed && pI.throttle < 0)
                    {
                        rB2D.AddForce(new Vector2(pI.throttle * maxAcceleration * Time.deltaTime, 0), ForceMode2D.Force);
                    }
                    else if (rB2D.velocity.magnitude < speed)
                    {
                        rB2D.AddForce(new Vector2(pI.throttle * wrongWheelMultiplier * maxAcceleration * Time.deltaTime, 0), ForceMode2D.Force);
                    }
                    return;
                case WheelPosition.Back:
                    if (rB2D.velocity.magnitude < speed && pI.throttle > 0)
                    {
                        rB2D.AddForce(new Vector2(pI.throttle * maxAcceleration * Time.deltaTime, 0), ForceMode2D.Force);
                    }
                    else if (rB2D.velocity.magnitude < speed)
                    {
                        rB2D.AddForce(new Vector2(pI.throttle * wrongWheelMultiplier * maxAcceleration * Time.deltaTime, 0), ForceMode2D.Force);
                    }
                    return;
            }
        }
        
        

        private void RotateInAir()
        {
            rotationSpeed = pI.throttle * rotAcc * Time.deltaTime;
            
            transform.RotateAround(centerObject.transform.position, Vector3.forward, rotationSpeed);
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                grounded = true;
                
            }
        } 
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                groundHit = true;
                groundHitRumble = 1f;  // Set the intensity for the landing

                // Trigger low-frequency rumble only on ground hit
                Rumble(1, 0, 0.25f);


            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            grounded = false;
        }

        public void Rumble(float LowFrequency, float HighFrequency, float Duration)
        {
            var pad = Gamepad.current;

            if (pad != null)
            {
                pad.SetMotorSpeeds(LowFrequency, HighFrequency);

                stopRumbleAfterCoroutine = StartCoroutine(StopRumble(Duration, pad));
            }
        }

        public IEnumerator StopRumble(float Duration, Gamepad pad)
        {
            float elapsedTime = 0f;
            while (elapsedTime < Duration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            groundHitRumble = 0f;
            pad.SetMotorSpeeds(0f, 0f);
        }
    }
}

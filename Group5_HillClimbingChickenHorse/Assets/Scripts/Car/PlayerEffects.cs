using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public ParticleSystem brakeDust;
    public ParticleSystem sparks1; //wheel1 particles  
    public ParticleSystem sparks2; //wheel2 particles
    public ParticleSystem exhaustParticles; //Exhaust
    public PlayerInputs playerInputs;
    public float throttleThreshold = 0.5f;
    public float brakeThreshold = -0.5f;

    private void Start()
    {
        playerInputs = FindObjectOfType<PlayerInputs>();
        if (playerInputs == null)
        {
            Debug.LogError("PlayerInputs component not found!");
        }

        //exhaust particle must not play at the start
        exhaustParticles.Stop();
    }

    void Update()
    {
        if (playerInputs != null)
        {
            HandleEffects();
        }
    }

    private void HandleEffects()
    {
        //Brake
        if (brakeDust != null && playerInputs.throttle < brakeThreshold)
        {
            if (!brakeDust.isEmitting)
            {
                brakeDust.Play();
            }
        }
        else if (brakeDust != null && brakeDust.isEmitting)
        {
            brakeDust.Stop();
        }

        //Wheel Sparks
        if (sparks1 != null && sparks2 != null && playerInputs.throttle > throttleThreshold)
        {
            if (!sparks1.isEmitting)
            {
                sparks1.Play();
                sparks2.Play();
            }
        }
        else if (sparks1 != null && sparks1.isEmitting && sparks2 != null && sparks2.isEmitting)
        {
            sparks1.Stop();
            sparks2.Stop();
        }

        //exhaust
        if (playerInputs.throttle > 0.1f)
        {
            if (!exhaustParticles.isPlaying)
            {
                exhaustParticles.Play();
            }
        }
        else
        {
            if (exhaustParticles.isPlaying)
            {
                exhaustParticles.Stop();
            }
        }
    }
}


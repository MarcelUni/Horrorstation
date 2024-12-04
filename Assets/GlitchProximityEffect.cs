using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchProximityEffect : MonoBehaviour
{
    public Material glitchMaterial;
    public float glitchStartRadius;
    public float glitchStartValue;
    public float maxGlitchApplied;

    private Transform player;
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        
        if (DistanceToPlayer() < glitchStartRadius)
        {
            float value = Mathf.Clamp(maxGlitchApplied / DistanceToPlayer(), 0, maxGlitchApplied);
            glitchMaterial.SetFloat("_GlitchAmount", value);
        }
        else
        {
            glitchMaterial.SetFloat("_GlitchAmount", 0);
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
    public static PlayerInvincibility instance;
    
    public float invincibilityTimer = 0;
    [SerializeField] private Material regularMaterial, invincibilityMaterial;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        instance = this;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer < 0)
            {
                Physics.IgnoreLayerCollision(9, 11, false);
                invincibilityTimer = 0;
                meshRenderer.material = regularMaterial;
            }
        }
    }

    public void StartInvincibility()
    {
        Physics.IgnoreLayerCollision(9, 11, true);
        invincibilityTimer = 3f;
        meshRenderer.material = invincibilityMaterial;
    }

    public bool IsInvincible()
    {
        return invincibilityTimer > 0;
    }
}

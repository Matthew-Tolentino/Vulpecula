﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{

    public enum TERRAIN_TYPES {STONE, GRASS, NULL};

    [SerializeField]
    private TERRAIN_TYPES defaultTerrain = TERRAIN_TYPES.STONE;

    private TERRAIN_TYPES currentTerrain = TERRAIN_TYPES.STONE;

    private FMOD.Studio.EventInstance footsteps;

    public void SetTerrainType(TERRAIN_TYPES type)
    {
        currentTerrain = type;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTerrainType(defaultTerrain);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SelectAndPlayFootstep();
        //}
    }

    // For testing
    private void OnMouseDown()
    {
        SelectAndPlayFootstep();
    }

    [SerializeField]
    private float terrainRayLength = 10.0f;

    private const int nStoneVariants = 10;
    private const int nGrassVariants = 7;

    void DetermineTerrain()
    {
        //currentTerrain = defaultTerrain;

        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, terrainRayLength);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Stone"))
            {
                currentTerrain = TERRAIN_TYPES.STONE;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = TERRAIN_TYPES.GRASS;
            }
        }
    }

    public void PlayFootstep()
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps_Discrete");
        footsteps.setParameterByName("Terrain", (float)currentTerrain);

        int nVariants = 0;
        switch(currentTerrain)
        {
            case TERRAIN_TYPES.STONE:
                nVariants = nStoneVariants;
                break;
            case TERRAIN_TYPES.GRASS:
                nVariants = nGrassVariants;
                break;
        }

        footsteps.setParameterByName("StepVariationNumber", (float)Random.Range(0, nVariants));
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footsteps.start();
        footsteps.release();
    }

    public void SelectAndPlayFootstep()
    {
        DetermineTerrain();
        PlayFootstep();
    }
}

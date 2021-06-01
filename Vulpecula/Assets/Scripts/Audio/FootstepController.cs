using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{

    public enum TERRAIN_TYPES {STONE, GRASS, NULL};

    [SerializeField]
    private TERRAIN_TYPES defaultTerrain = TERRAIN_TYPES.STONE;

    [SerializeField]
    private List<TERRAIN_TYPES> texToTerrainList;

    private TERRAIN_TYPES currentTerrain = TERRAIN_TYPES.STONE;

    private FMOD.Studio.EventInstance footsteps;

    public void SetTerrainType(TERRAIN_TYPES type)
    {
        currentTerrain = type;
    }

    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainData = terrain.terrainData;
        terrainPos = terrain.transform.position;

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

    private Terrain terrain;
    private TerrainData terrainData;
    private Vector3 terrainPos;

    void DetermineTerrain()
    {
        //currentTerrain = defaultTerrain;

        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, terrainRayLength);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.name == "Terrain")
            {
                if (texToTerrainList.Count > 0)
                {
                    int Index = GetMainTexture(rayhit.point);
                    currentTerrain = texToTerrainList[Index];
                }
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Stone"))
            {
                currentTerrain = TERRAIN_TYPES.STONE;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = TERRAIN_TYPES.GRASS;
                break;
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

    float[] GetTextureMix(Vector3 worldPos)
    {
         // returns an array containing the relative mix of textures
         // on the main terrain at this world position.
     
         // The number of values in the array will equal the number
         // of textures added to the terrain.
     
         // calculate which splat map cell the worldPos falls within (ignoring y)
         int mapX = (int)(((worldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth );
         int mapZ = (int)(((worldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight );
     
         // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
         float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1 );

        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];
     
         for (int n = 0; n<cellMix.Length; n ++ )
         {
             cellMix[n] = splatmapData[0, 0, n];
         }
     
         return cellMix;
    }
 
 
     int GetMainTexture(Vector3 worldPos)
     {
        // returns the zero-based index of the most dominant texture
        // on the main terrain at this world position.
        float[] mix = GetTextureMix(worldPos );

        float maxMix = 0;
        int maxIndex = 0;
     
         // loop through each mix value and find the maximum
         for (int n = 0; n<mix.Length; n ++ )
         {
             if (mix[n] > maxMix )
             {
                 maxIndex = n;
                 maxMix = mix[n];
             }
         }
     
         return maxIndex;
     }
}

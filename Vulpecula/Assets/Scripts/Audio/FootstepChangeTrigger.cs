using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FootstepChangeTrigger : MonoBehaviour
{

    [SerializeField]
    private FootstepController footstepController;

    [SerializeField]
    private FootstepController.TERRAIN_TYPES onEnterTypeToSet = FootstepController.TERRAIN_TYPES.NULL;
    [SerializeField]
    private FootstepController.TERRAIN_TYPES onExitTypeToSet = FootstepController.TERRAIN_TYPES.NULL;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && onEnterTypeToSet != FootstepController.TERRAIN_TYPES.NULL)
        {
            footstepController.SetTerrainType(onEnterTypeToSet);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && onExitTypeToSet != FootstepController.TERRAIN_TYPES.NULL)
        {
            footstepController.SetTerrainType(onExitTypeToSet);
        }
    }
}

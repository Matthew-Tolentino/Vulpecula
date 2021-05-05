using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
	public bool loseSpirits;
    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
        	if (loseSpirits){
        		var pull = obj.GetComponent<SpiritHandler>();
        		while(pull.SpiritList.Count > 0){
        			pull.loseSpirit();
        		}
        	}

            // Clear inventory to show correct items needed in next level
            ItemManager.instance.ResetInventory();
            // Set inventory for 1st level
            // Wont work with mulitple level will need to revise
            ItemManager.instance.SetInventory(1);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

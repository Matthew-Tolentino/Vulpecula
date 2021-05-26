using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
	private GameObject player = null;

     public void OnTriggerEnter(Collider obj)
    {
    	if (obj.gameObject.CompareTag("Player"))
        {
        	player = obj.gameObject;
            Invoke("delayedFunction", 0.5f);
        }
    }

    private void delayedFunction()
    {
    	StartCoroutine("delayedFunction2");
    }

    IEnumerator delayedFunction2()
    {
    	while(DialogueManager.instance.isOpen)
    	{
    		yield return null;
    	}
    	var pull = player.GetComponent<SpiritHandler>();
		pull.loseSpirit();

		 // Clear inventory to show correct items needed in next level
        ItemManager.instance.ResetInventory();
        // Set inventory for 1st level
        // Wont work with mulitple level will need to revise
        ItemManager.instance.SetInventory(1);
 
        StartCoroutine(GameManager.instance.NextScene());
    }
}

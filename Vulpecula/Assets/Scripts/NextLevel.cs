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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}

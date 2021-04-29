using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHandler : MonoBehaviour
{
	public List<GameObject> SpiritList;
	public float rotDegree;
    public float rotSpeed;

    public bool triggerLamp;

    private int numFloaters;
    public int selectedSpirit;
    public bool ability;


    public Vector3 rockgoto;
    public GameObject targetRockLoc;
    public bool nearButton;

    public bool strongSpirit;


    void Start()
    {
    	SpiritList = new List<GameObject>();
    	rotDegree = 0f;
    	numFloaters = 0;
    	selectedSpirit = -1;
    	ability = false;
        triggerLamp = false;
        nearButton = false;
        targetRockLoc = null;
        strongSpirit = false;
    }



    void Update()
    {
    	rotDegree += rotSpeed * Time.fixedDeltaTime;

    	if (InputManager.instance.KeyDown("SpiritAbility")) callAbility();
    	if (InputManager.instance.KeyDown("SpiritDec")) decrementSelect();
    	if (InputManager.instance.KeyDown("SpiritInc")) incrementSelect();
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritFloating(numFloaters++);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);

            // Enable Passive Abilities
            if (pull.type == "Bunny") ; // ENABLE MAP


        }
        else if (collision.gameObject.tag == "Spirit_Land")
        {
            var pull = collision.gameObject.GetComponent<SpriritMovement_Land>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritLand();

            // Enable Passive Abilities
            if (pull.type == "Strong") strongSpirit = true;


        }
        else return;

        // call sounds
        if (collision.gameObject.GetComponent<SpiritSounds>() != null)
        {
            foreach (var emitter in collision.gameObject.GetComponent<SpiritSounds>().collectSounds)
            {
                emitter.Play();
            }
        }

        SpiritList.Add(collision.gameObject);
        if (ability) callAbility();

        if (selectedSpirit == -1) selectedSpirit = 0;
        else selectedSpirit = SpiritList.Count - 1;
        nameSelected();

    }



    public void loseSpirit()
    {
        if (SpiritList.Count == 0) return;
        int removeIndex = SpiritList.Count - 1;

        if (SpiritList[removeIndex].tag == "Spirit_Floating")
        {
            var pull = SpiritList[removeIndex].GetComponent<SpiritMovement_Floating>();
            pull.ReleaseSpiritFloating();
            Physics.IgnoreCollision(SpiritList[removeIndex].GetComponent<Collider>(), GetComponent<Collider>(), false);
            --numFloaters;

            // Disable Passive Abilities
            if (pull.type == "Bunny") ; // DISABLE MAP
        }
        else if (SpiritList[removeIndex].tag == "Spirit_Land")
        {
            var pull = SpiritList[removeIndex].GetComponent<SpriritMovement_Land>();
            pull.ReleaseSpiritLand();

            // Disable Passive Abilities
            if (pull.type == "Strong") strongSpirit = false;
        }

        SpiritList.RemoveAt(removeIndex);
        if (removeIndex == selectedSpirit) {
        	--selectedSpirit;
        	if (ability) callAbility();
        }
        nameSelected();
    }



    private void callAbility()
    {
        if (selectedSpirit < 0) return;

        // call sounds
        if (SpiritList[selectedSpirit].GetComponent<SpiritSounds>() != null)
        {
            foreach (var emitter in SpiritList[selectedSpirit].GetComponent<SpiritSounds>().actionTriggerSounds)
            {
                emitter.Play();
            }
        }

        if (SpiritList[selectedSpirit].tag == "Spirit_Land")
    	{
    		var pull = SpiritList[selectedSpirit].GetComponent<SpriritMovement_Land>();
    		if (pull.type == "Sit") 
    		{
    			if (!ability)
    			{
    				Vector3 got;
                    if (nearButton) got = rockgoto;
                    else got = transform.position + transform.forward * 4f;
                    pull.abilityMove(got, targetRockLoc);
                    ability = true;
                    Physics.IgnoreLayerCollision(8, 10, true);
                    
    			}
    			else
    			{
    				pull.ObtainSpiritLand();
                    targetRockLoc = null;
                    ability = false;
                    Physics.IgnoreLayerCollision(8, 10, false);
    			}
    			return;
    		}

    		else return;
    	}

    	else if (SpiritList[selectedSpirit].tag == "Spirit_Floating")
    	{
    		var pull = SpiritList[selectedSpirit].gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.type == "Lamp") triggerLamp = true;
    	}
    }



    private void incrementSelect()
    {
    	if (selectedSpirit == -1) return;
    	if (ability) {
        	callAbility();
        	ability = !ability;
        }
    	if (selectedSpirit == SpiritList.Count - 1) selectedSpirit = 0;
    	else ++selectedSpirit;
    	nameSelected();

    }
    private void decrementSelect()
    {
    	if (selectedSpirit == -1) return;
    	if (ability) {
        	callAbility();
        	ability = !ability;
        }
    	if (selectedSpirit == 0) selectedSpirit = SpiritList.Count - 1;
    	else --selectedSpirit;
    	nameSelected();
    }

    private void nameSelected(){
    	var send = GetComponent<PlayerUI>();
    	string type = "";
    	if (selectedSpirit == -1) type = "none";
    	else if (SpiritList[selectedSpirit].tag == "Spirit_Land")
    	{
    		var pull = SpiritList[selectedSpirit].GetComponent<SpriritMovement_Land>();
    		if (pull.type == "Sit") type = "Rock";
    	}
    	else if (SpiritList[selectedSpirit].tag == "Spirit_Floating")
    	{
    		var pull = SpiritList[selectedSpirit].gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.type == "Bunny") type = "Bunny";
            if (pull.type == "Lamp") type = "Lamp";
    	}
    	if (type == "") type = "undefined";

    	// send.SelectSpirit(type);
        GameManager.instance.setSelectedSpirit(type);
        GameManager.instance.SetSpiritToolTip(type);
        if (selectedSpirit != -1){
            var pull = SpiritList[selectedSpirit].GetComponent<SpiritGlowControl>();
            if (pull != null) pull.activate = true;
        }
    }


}
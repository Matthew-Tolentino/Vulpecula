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

    public bool StrongActive;

    public int[] selectableSpirits;
    private int selectIndex;
    private int currentIndex;



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

        StrongActive = false;

        selectIndex = -1;
        currentIndex = -1;

        selectableSpirits = new int[3];
        selectableSpirits[0] = -1;
        selectableSpirits[1] = -1;
        selectableSpirits[2] = -1;
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
    	string type = "";
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritFloating(numFloaters++);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);

            // Enable Passive Abilities
            if (pull.type == "Bunny") GameManager.instance.setMinimapVisable(true);

            if (pull.type == "Lamp") type = pull.type;
            


        }
        else if (collision.gameObject.tag == "Spirit_Land")
        {
            var pull = collision.gameObject.GetComponent<SpriritMovement_Land>();
            if (pull.state != "Spawn") return;
            if (pull.type == "Rock" || pull.type == "Strong") type = pull.type;
            pull.ObtainSpiritLand();



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
        

        // Add to new selector
        if (type != "") {
        	++selectIndex;
        	selectableSpirits[selectIndex] = selectedSpirit;
        	currentIndex = selectIndex;
        }

        nameSelected();

    }



    public void loseSpirit()
    {
        while(SpiritList.Count != 0) loseElement();

        // Handle new Selector
        selectableSpirits[0] = -1;
        selectableSpirits[1] = -1;
        selectableSpirits[2] = -1;
        selectIndex = -1;
        currentIndex = -1;

        nameSelected();
    }

    private void loseElement()
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
            if (pull.type == "Bunny") GameManager.instance.setMinimapVisable(false);
        }
        else if (SpiritList[removeIndex].tag == "Spirit_Land")
        {
            if (removeIndex == selectedSpirit && ability) callAbility();
            var pull = SpiritList[removeIndex].GetComponent<SpriritMovement_Land>();
            pull.ReleaseSpiritLand();

            // Disable Passive Abilities

        }

        SpiritList.RemoveAt(removeIndex);
        if (removeIndex == selectedSpirit) {
        	--selectedSpirit;
        	//if (ability) callAbility();
        }
    }



    private void callAbility()
    {
        if (currentIndex == -1 || selectableSpirits[currentIndex] < 0) return;

        // call sounds
        if (SpiritList[selectableSpirits[currentIndex]].GetComponent<SpiritSounds>() != null)
        {
            foreach (var emitter in SpiritList[selectableSpirits[currentIndex]].GetComponent<SpiritSounds>().actionTriggerSounds)
            {
                emitter.Play();
            }
        }

        if (SpiritList[selectableSpirits[currentIndex]].tag == "Spirit_Land")
    	{
    		var pull = SpiritList[selectableSpirits[currentIndex]].GetComponent<SpriritMovement_Land>();
    		if (pull.type == "Rock") 
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

            else if (pull.type == "Strong")
            {
                StrongActive = true;
                Invoke("delayedStrong", 0.5f);
            }

    		else return;
    	}

    	else if (SpiritList[selectableSpirits[currentIndex]].tag == "Spirit_Floating")
    	{
    		var pull = SpiritList[selectableSpirits[currentIndex]].gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.type == "Lamp") {
                triggerLamp = true;
                Invoke("endLampCheck", 0.5f);
            }
    	}
    }

    private void delayedStrong(){
        StrongActive = false;
    }

    private void incrementSelect()
    {
    	if (currentIndex == -1) return;
    	if (ability) {
        	callAbility();
        	ability = !ability;
        }

        int temp = currentIndex;
    	if (temp == 2) temp = 0;
    	else ++temp;
    	if (selectableSpirits[temp] == -1) return;
    	currentIndex = temp;

    	nameSelected();

    }
    private void decrementSelect()
    {
    	if (currentIndex == -1) return;
    	if (ability) {
        	callAbility();
        	ability = !ability;
        }

        int temp = currentIndex;
    	if (temp == 0) temp = 2;
    	else --temp;
    	if (selectableSpirits[temp] == -1) return;
    	currentIndex = temp;

    	nameSelected();
    }

    private void nameSelected(){
    	var send = GetComponent<PlayerUI>();
    	string type = "";
    	
    	if (currentIndex == -1 || selectableSpirits[currentIndex] == -1) type = "none";
    	else if (SpiritList[selectableSpirits[currentIndex]].tag == "Spirit_Land")
    	{
    		var pull = SpiritList[selectableSpirits[currentIndex]].GetComponent<SpriritMovement_Land>();
    		type = pull.type;
    	}
    	else if (SpiritList[selectableSpirits[currentIndex]].tag == "Spirit_Floating")
    	{
    		var pull = SpiritList[selectableSpirits[currentIndex]].GetComponent<SpiritMovement_Floating>();
            type = pull.type;
    	}
    	if (type == "") type = "undefined";

    	// send.SelectSpirit(type);
        GameManager.instance.setSelectedSpirit(type);
        GameManager.instance.setSelectedSpiritNext(GetNextSpirit());
        GameManager.instance.setSelectedSpiritPrev(GetPrevSpirit());
        GameManager.instance.SetSpiritToolTip(type);

        if (currentIndex != -1){
            var pull = SpiritList[selectableSpirits[currentIndex]].GetComponent<SpiritGlowControl>();
            if (pull != null) pull.activate = true;
        }
    }

    private void endLampCheck(){
        triggerLamp = false;
    }

    public string GetNextSpirit(){
        if (selectIndex == -1) return "none";
        int next = currentIndex + 1;
        if (next >= 3) next = 0;

        if (selectableSpirits[next] == -1) return "none";
        if (SpiritList[selectableSpirits[next]].tag == "Spirit_Land"){
            return SpiritList[selectableSpirits[next]].GetComponent<SpriritMovement_Land>().type;
        }
        else{
            return SpiritList[selectableSpirits[next]].GetComponent<SpiritMovement_Floating>().type;  
        }
    }
    public string GetPrevSpirit(){
        if (selectIndex == -1) return "none";
        int prev = currentIndex - 1;
        if (prev <= -1) prev = 2;

        if (selectableSpirits[prev] == -1) return "none";
        if (SpiritList[selectableSpirits[prev]].tag == "Spirit_Land"){
            return SpiritList[selectableSpirits[prev]].GetComponent<SpriritMovement_Land>().type;
        }
        else{
            return SpiritList[selectableSpirits[prev]].GetComponent<SpiritMovement_Floating>().type;  
        }
    }
}
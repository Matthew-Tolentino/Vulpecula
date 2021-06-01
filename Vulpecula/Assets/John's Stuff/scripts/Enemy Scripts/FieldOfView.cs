using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class FieldOfView : MonoBehaviour
{
    
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public GameObject myPlayer;
    public GameObject myBody;

    public GameObject exclamationPoint;

    private DialogueManager manager;

    //public GameObject PostFX_Vision;
    //public PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();

    //public GameObject postProc;
    //private Vignette vg;


    private SpiritHandler spiritRef;
    private Animator animator;
    private Animation_Handler animationRef;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private int seenDelay = 0;

   
    
    


    [HideInInspector]
    public bool isSeen = false;
    public List<Transform> visibleTargets = new List<Transform>();

    [SerializeField]
    private FMODUnity.StudioEventEmitter onSeeSound;
    [SerializeField]
    private FMODUnity.StudioEventEmitter onSeeHumanSound;
    [SerializeField]
    private FMODUnity.StudioEventEmitter passiveSound;
    private float lastTimeSeen = 0;

    private void Start()
    {
        animator = myBody.GetComponent<Animator>();
        spiritRef = myPlayer.GetComponent<SpiritHandler>();
        animationRef = myBody.GetComponent<Animation_Handler>();
        StartCoroutine("FindTargetsWithDelay", .2f);

        GameManager.instance.setCameraShake(false);

        //vg = postProc.GetComponent<Vignette>();
        exclamationPoint.SetActive(false);

        manager = GameObject.Find("Managers").GetComponent<DialogueManager>();



        passiveSound.Play();
    }

    private void OnDestroy()
    {
        passiveSound.Stop();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
           // CheckLight();
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        

        for (int i = 0; i < targetsInViewRadius.Length-1; i++)
        {
    
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
               
                //If player is seen, the code below is performed***********************************************************
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && !GameManager.instance.cheatNoDamage)
                {
                    if (isSeen == false) // only when first seen
                    {
                        if (onSeeHumanSound != null)
                            onSeeHumanSound.Play();
                    }
                    // onSeeSound
                    if (onSeeSound != null && (Time.time - lastTimeSeen) >= 0.8)
                    {
                        lastTimeSeen = Time.time;
                        onSeeSound.Play();
                    }
                    
                    // Look at Player when seen
                    transform.LookAt(target);
                    transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

                    // Tell other scripts that the player is seen
                    seenDelay++;
                    if(seenDelay >= 10)
                    {
                        if (!manager.isOpen)
                        {
                            animationRef.seenCounter++;
                        }
                    }
                    isSeen = true;
                    spiritRef.loseSpirit();
                   
                    // Add player to visible targets list
                    visibleTargets.Add(target);

                    // Camera Shake
                    GameManager.instance.setCameraShake(true);
                    

                    // Check if the Player is dead
                    if (animationRef.seenCounter >= 25){
                        animator.SetBool("isDead", true);
                        myPlayer.GetComponent<CharacterController>().enabled = false;
                        myPlayer.GetComponent<PlayerMovement>().enabled = false;
                        GameManager.instance.GameOver();
                    }

                    // Activate Post Processing Effect
                    //vg.intensity.value = 0.715f;

                    exclamationPoint.SetActive(true);


                   

                       
                }
                

            }
            else if(isSeen == true)
            {
                
                targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

                GameManager.instance.setCameraShake(false);
                

                seenDelay = 0;
                
                isSeen = false;
            }

        }
        if(targetsInViewRadius.Length == 0)
        {
            if (isSeen == true)
            {
                GameManager.instance.setCameraShake(false);
                
            }

            isSeen = false;
            //vg.intensity.value = 0.0f;

            exclamationPoint.SetActive(false);

            
        }
        
        
        
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

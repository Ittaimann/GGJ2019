using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Cat : Interactable
{
    public List<OffMeshLink> testLinks;

    public Transform testination;

    public float upAnimLoopUntilY, downAnimLoopUntilY;
    public float jumpLoopSpeed;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NavMeshAgent agent = null;
    [SerializeField]
    private Transform test = null;

    private int catAngerLevel;
    private AudioSource meow;

    [SerializeField]
    private AudioClip[] niceMeow;
    [SerializeField]
    private AudioClip[] annoyedMeow;
    [SerializeField]
    private AudioClip[] angerMeow;

    public GameDataScriptable gameDataScriptable;
    public GameObject[] specialLocations;
    public Transform FoodBowl;

    bool hasValidPath;
    bool onOffMeshLinkCoroutineRunning;
    Coroutine validPathCoroutine;

    void Start()
    {
        //validPathCoroutine = StartCoroutine(ValidPathCoroutine());
        meow = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Animate idle + walk based on movement
        float rate = agent.desiredVelocity.magnitude/agent.speed;
        animator.SetFloat("MoveMagnitude", Mathf.Lerp(animator.GetFloat("MoveMagnitude"), rate, 4f * Time.deltaTime));

        // Track destination test obj (testination)
        agent.destination = testination.position;

        // Monitor off mesh link -- if on one, determine up/down and play anim
        if (agent.isOnOffMeshLink && !onOffMeshLinkCoroutineRunning)
            StartCoroutine(JumpAlongOffMeshLinkCoroutine());
    }

    IEnumerator JumpAlongOffMeshLinkCoroutine()
    {
        onOffMeshLinkCoroutineRunning = true;
        OffMeshLinkData linkData = agent.currentOffMeshLinkData;

        float startDist = Vector3.Distance(linkData.startPos, transform.position);
        float endDist = Vector3.Distance(linkData.endPos, transform.position);

        // Find start node and end node (end node on link can be where we start, have to check)
        Vector3 startNode = Mathf.Min(startDist, endDist) == startDist ? linkData.startPos : linkData.endPos;
        Vector3 endNode = startNode == linkData.startPos ? linkData.endPos : linkData.startPos;

        
        // float rotDiff = transform.eulerAngles.y -
        // transform.RotateAround()
        bool goingUp = startNode.y <= endNode.y;
        bool inJumpLoop = false, afterJumpFinish = false;
        while (true)
        {
            if (!animator.GetBool("FinishJump"))
                inJumpLoop = animator.GetBool("InJumpLoop");
            
            if (!inJumpLoop)
            {
                animator.SetBool(goingUp ? "StartUpJump" : "StartDownJump", true);
                transform.LookAt(new Vector3(endNode.x, transform.position.y, endNode.z));
            }
            else
            {
                if (!animator.GetBool("FinishJump"))
                {
                    Vector3 worldDeltaPosition = Vector3.MoveTowards(transform.position, endNode, jumpLoopSpeed*Time.deltaTime) - transform.position;
                    transform.position += 0.1f*worldDeltaPosition;
                }

                Vector3 rootPos = animator.rootPosition;

                if ((goingUp && (endNode.y - rootPos.y) < upAnimLoopUntilY) || (!goingUp && rootPos.y - endNode.y >= downAnimLoopUntilY))
                {
                    animator.SetBool("FinishJump", true);
                    animator.SetBool(goingUp ? "StartUpJump" : "StartDownJump", false);
                }
            }
            

            afterJumpFinish = animator.GetBool("AfterJumpFinish");
            if (afterJumpFinish)
            {
                animator.SetBool("AfterJumpFinish", false);
                animator.SetBool("FinishJump", false);
                animator.SetBool("InJumpLoop", false);
                agent.CompleteOffMeshLink();
                agent.isStopped = false;
                transform.position = endNode;
                onOffMeshLinkCoroutineRunning = false;
                animator.rootPosition = endNode;
                agent.nextPosition = endNode;
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public override void StartDay()
    {
        base.StartDay();
        catAngerLevel = gameDataScriptable.catAngerLevel;
        gameDataScriptable.fedCat = false;

        StartCoroutine(GoToRandomSpecialLocation());
        StartCoroutine(Meow());

    }

    /// <summary>
    /// Sets NavMeshAgent destination to position.
    /// </summary>
    public void MoveToPosition(Vector3 pos)
    {
        agent.destination = pos;
    }

    /// <summary>
    /// Commands the agent to move to a random point on the NavMesh in a
    /// given radius. Good for simulating random "walking around" behavior.
    /// </summary>
    public void MoveToRandomPosInRadius(float radius)
    {
        Vector3 randomDir = Random.insideUnitSphere * radius + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, radius, 1);
        
        MoveToPosition(hit.position);
    }

    public void feedCat()
    {
        if(catAngerLevel > 0)
            catAngerLevel--;
        gameDataScriptable.fedCat = true;
    }

    IEnumerator ValidPathCoroutine()
    {
        while (true)
        {
            agent.CalculatePath(agent.destination, agent.path);
            hasValidPath = agent.path.status == NavMeshPathStatus.PathComplete;

            foreach (OffMeshLink link in testLinks)
            {
                link.UpdatePositions();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void DoMeow()
    {
        if (catAngerLevel == 0)
        {
            meow.PlayOneShot(niceMeow[Random.Range(0, niceMeow.Length)]);
        }
        else if (catAngerLevel == 1)
        {
            meow.PlayOneShot(annoyedMeow[Random.Range(0, annoyedMeow.Length)]);
        }
        else if (catAngerLevel == 2)
        {
            meow.PlayOneShot(angerMeow[Random.Range(0, angerMeow.Length)]);
        }
        meow.Play();
    }

    IEnumerator Meow()
    {
        while(catAngerLevel > 0)
        {
            DoMeow();
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        DoMeow();
    }

    IEnumerator GoToRandomSpecialLocation()
    {
        yield return new WaitForSeconds(1);
        float counter = 0;
        while(true)
        {
            if(gameDataScriptable.foodReady)
            {
                agent.SetDestination(FoodBowl.position);
                counter = 10f;
                feedCat();
                yield return new WaitForSeconds(5f);
                gameDataScriptable.foodReady = false;
            }
            else if(counter <= 0)
            {
                agent.SetDestination(specialLocations[Random.Range(0, specialLocations.Length)].transform.position);
                counter = Random.Range(5f, 10f);
            }
            counter -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : Interactable
{
    public List<OffMeshLink> testLinks;

    [SerializeField]
    private NavMeshAgent agent = null;
    [SerializeField]
    private Transform test = null;

    private int catAngerLevel;
    [SerializeField]
    private AudioSource meow;

    public GameDataScriptable gameDataScriptable;
    public GameObject[] specialLocations;
    public Transform FoodBowl;

    bool hasValidPath;

    Coroutine validPathCoroutine;

    void Start()
    {
        //validPathCoroutine = StartCoroutine(ValidPathCoroutine());
    }

    public override void StartDay()
    {
        base.StartDay();
        catAngerLevel = gameDataScriptable.catAngerLevel;
        gameDataScriptable.fedCat = false;

        StartCoroutine(GoToRandomSpecialLocation());
        StartCoroutine(AnnoyedMeow(catAngerLevel));

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

    IEnumerator AnnoyedMeow(int intensity)
    {
        while(catAngerLevel > 0)
        {
            if(intensity == 0)
            {
                //nice meow sound
            }
            else if(intensity == 1)
            {
                //annoyed meow sound
            }
            else if (intensity == 2)
            {
                //angry meow sound
            }
            meow.Play();
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
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

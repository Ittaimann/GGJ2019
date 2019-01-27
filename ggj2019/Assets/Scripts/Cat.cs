using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour
{
    public List<OffMeshLink> testLinks;

    [SerializeField]
    private NavMeshAgent agent = null;
    [SerializeField]
    private Transform test = null;

    bool hasValidPath;

    Coroutine validPathCoroutine;

    void Awake()
    {
        validPathCoroutine = StartCoroutine(ValidPathCoroutine());
    }

    void Update()
    {
        agent.destination = test.position;
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
}

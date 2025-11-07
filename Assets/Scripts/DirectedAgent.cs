using UnityEngine;
using UnityEngine.AI;

public class DirectedAgent : MonoBehaviour
{
    NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {

    }
    
    public void GoToLocation(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }
}

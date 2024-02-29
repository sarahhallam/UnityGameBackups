using UnityEngine;
using UnityEngine.AI;

public class droneAgent : MonoBehaviour
{
    [SerializeField] Transform target;
    
    NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _agent.SetDestination(target.position);
    }
}

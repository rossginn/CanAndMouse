using UnityEngine.AI;
using UnityEngine;

/**
 * KGB Team Controller
 * 
 * 1. If target insite, broadcast location to team
 * 2. When targets location is broadcast, the whole team looks towards the target
 *      2a. Team looks towards target randomly over 2 seconds, not synchronously
 * 3. When target goes out of view, team randomly begins looking around to attempt to reaquire target
 * 4. Agents will leave or join the surveilance team on scheduled intervals
 * 
 */ 
public class EnemyMovementController : MonoBehaviour
{

    // Sight vares
    float fieldOfView = .6f;

    public NavMeshAgent agent; // This pathfinding agent
    public Transform playerTransform;
    Vector3 playerPosition; // Previous player position

    // Team vars
    bool targetAquired = false;

    private void Start()
    {
        //lookAroundForTarget();
    }

    void Update()
    {
        if (canSeeTarget())
        {
            //targetAquired = true;
            //broadcastTargetLocation();
            Debug.Log("Can see target");
        }
        else
        {
            Debug.Log("Can't see target");
            //lookAroundForTarget();
        }

    }

    void followTarget()
    {
        // Only update destination if player has moved
        if (playerTransform.position != playerPosition)
        {
            agent.SetDestination(playerTransform.position);
        }
        // End by updating player postion
        playerPosition = playerTransform.position;
    }

    

    void broadcastTargetLocation()
    {
        // Aim other agents towards target
    }

    void lookAroundForTarget()
    {
        while (!targetAquired)
        {

        }
    }


    bool canSeeTarget()
    {
        Vector3 heading = playerTransform.position - transform.position;
        float dot = Vector3.Dot(heading, transform.forward);
        if (dot > fieldOfView)
        {
            // If within FOV and there's a line of sight
            RaycastHit hit;
            if (Physics.Raycast(transform.position, heading, out hit))
            {
                if(hit.collider.tag == "player")
                {
                    return true;
                }
            }
            
        }
        return false;

    }

    public void LookAtTarget(Vector3 targetPosition)
    {
        this.transform.LookAt(targetPosition);
    }
}


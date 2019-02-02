using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * TODO - switch from boolean states to an enum AQUIRED|LOOKING
 */ 
public class Watcher : MonoBehaviour
{
    private int agentNumber = 1;
    private bool looking = false;
    public bool targetAquired = false;
    public int waitSeconds = 5;
    private float fieldOfView = .6f;
    public Transform playerTransform;
    public KGBManager kgbManager;
    public Vector3 playerPosition; // Previous player position


    // Keep eyes on target, or starts looking
    void Update()
    {
        if (CanSeeTarget())
        {
            targetAquired = true;
            if (playerPosition != playerTransform.position)
            {
                Debug.Log("Update: Target Aquired");
                LookAtTarget(playerPosition);
                playerPosition = playerTransform.position;
            }
            
        }
        else if(looking == false)
        {
            // Player out of sight
            Debug.Log("Update: Target Lost");
            looking = true;
            targetAquired = false;
            StartCoroutine(LookForTarget());

        }
        
    }


    /**
     * Called from manager when another agent has spotted the target
     */
    public void LookAtTarget(Vector3 targetPosition)
    {
        looking = false;
        this.transform.LookAt(targetPosition);
    }

    /**
     * Puts this agent into looking mode, breaks out if target spotted
     * - CanSeeTarget?  
     *      No: rotate, wait
     *      Yes: targetAquired = true, break
     * 
     */
    public IEnumerator LookForTarget()
    {
        int iterations = 0;
        Debug.Log("LookForTarget: Entry");
        while (!targetAquired)
        {
            iterations++;
            Debug.Log("LookForTarget: Iterations: " + iterations);
            if (CanSeeTarget())
            {
                Debug.Log("LookForTarget: Target Aquired");
                targetAquired = true;
                looking = false;
                kgbManager.TargetAquired(playerTransform.position, agentNumber);
            }
            else
            {
                Debug.Log("LookForTarget: Rotating");
                transform.Rotate(Random.rotation * Vector3.up * Time.deltaTime);
                yield return new WaitForSeconds(waitSeconds);
            }
        }
    }

    bool CanSeeTarget()
    {
        Vector3 heading = playerTransform.position - transform.position;
        float dot = Vector3.Dot(heading, transform.forward);
        if (dot > fieldOfView)
        {
            // If within FOV and there's a line of sight
            RaycastHit hit;
            if (Physics.Raycast(transform.position, heading, out hit))
            {
                if (hit.collider.tag == "player")
                {
                    return true;
                }
            }

        }
        return false;

    }

}

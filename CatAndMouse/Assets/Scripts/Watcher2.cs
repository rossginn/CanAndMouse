using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher2 : MonoBehaviour
{

    public enum Mode {aquired, searching};

    public Transform playerTransform;
    public Mode mode = Mode.aquired;
    public int pauseSeconds = 5;
    public float fieldOfView = .6f;



    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red, 50, true);
        if (canSee())
        {
            mode = Mode.aquired;
        }
        else
        {
            if (mode != Mode.searching)
            {
                mode = Mode.searching;
                StartCoroutine(SearchForTarget(pauseSeconds));
            }
        }
    }

    bool canSee()
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

    /**
     * Looking mode - randomly rotates every X seconds.
     * 
     */
    public IEnumerator SearchForTarget(int _pauseSeconds)
    {
        Debug.Log("Enter Search");
        while (mode == Mode.searching)
        {
            Debug.Log("Tock");
            transform.Rotate(Random.rotation * Vector3.up * Time.deltaTime);
            yield return new WaitForSeconds(_pauseSeconds);
            Debug.Log("Tick");
        }
    }

    /**
     * Look at the target
     */
    public void LookAtTarget()
    {
        this.transform.LookAt(playerTransform.transform.position);
    }
}

using UnityEngine.AI;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisionFromP");
        if (collision.collider.tag == "enemy")
        {
            Debug.Log("Game over");
        }
    }

}




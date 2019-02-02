using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class KGBManager : MonoBehaviour
{

    List<GameObject> kgbAgents = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TargetAquired(Vector3 target, int agentNumber)
    {
        // Focus other agents
    }

    public void MoveRandom()
    {
        // If target hasn't been aquired in X minutes, move a random to the last place they were spotted
    }

}

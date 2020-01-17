using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixStep : MonoBehaviour
{

    public GameObject[] zones;
    bool[] zonesExplored;



    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i<4;i++)
        {
            zonesExplored[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

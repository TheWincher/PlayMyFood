using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Recipe r = Parser.CreateRecipeFromJSON(Application.dataPath + "/Recipe/Test.json");
        foreach(Step s in r.Steps)
        {
            Debug.Log(s.Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

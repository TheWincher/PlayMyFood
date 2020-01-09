using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Recipe recipe;
    int stepCurrent = 0;

    public Recipe Recipe { get => recipe;}
    public int StepCurrent { get => stepCurrent;}

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        recipe = Parser.CreateRecipeFromJSON(Application.dataPath + "/Recipe/test.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

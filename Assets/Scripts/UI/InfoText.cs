using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    Step step;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        step = gameManager.Recipe.Steps[gameManager.StepCurrent];
    }

    // Update is called once per frame
    void Update()
    {

    }
}

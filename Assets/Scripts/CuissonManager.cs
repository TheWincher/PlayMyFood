using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuissonManager : MonoBehaviour
{
    GameManager gameManager;
    StepCuisson step;
    Button buttonAdd;
    Slider sliderFeu;
    MixStep fryingPan;
    MoveSpoon spoon;
    float time = 0;
    int resAction = -1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        while(gameManager.Recipe == null)
            Debug.Log(gameManager.Recipe);
        step = (StepCuisson) gameManager.Recipe.Steps[gameManager.StepCurrent];

        buttonAdd = GameObject.Find("Button_ingredient").GetComponent<Button>();
        buttonAdd.onClick.AddListener(() => setAction(0));

        sliderFeu = GameObject.Find("Slider_feu").GetComponent<Slider>();
        fryingPan = GameObject.Find("fryingPan").GetComponent<MixStep>();
        spoon = GameObject.Find("woodenSpoon").GetComponent<MoveSpoon>();
    }

    // Update is called once per frame
    void Update()
    {
        int ind = -1;
        time += Time.deltaTime;
        for(int i = 0; i < step.Times.Count; i++)
        {
            if(time >= step.Times[i] && time <= step.Times[i] + 3f)
            {
                ind = i;
                break;
            }
        }

        if(ind != -1)
        {
            if (step.Types[ind].Contains("fire"))
            {
                if(sliderFeu.value == int.Parse(step.Types[ind][step.Types[ind].Length - 1].ToString()))
                    Debug.Log("Action feu réussite");
            }
            else 
            {
                switch(step.Types[ind])
                {
                    case "add" :
                        if(resAction == 0)
                        {
                            Debug.Log("Action ajout réussite");
                            resAction = -1;
                        }
                        else if(resAction != -1)
                            Debug.Log("Vous avez fait une erreur");
                        break;
                    case "mix":

                        fryingPan.enabled = true;

                        break;
                }
            }
        }
        else if(resAction != -1)
        {
            Debug.Log("Aucune action à effectuer");
            resAction = -1;
        }
    }

    void setAction(int i)
    {
        resAction = i;
    }
}

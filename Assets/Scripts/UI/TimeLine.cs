using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour
{
    // Start is called before the first frame update
    float sizeOfOneSec, time;
    int indIngredient = 0;
    StepCuisson step;
    Text textInfo;
    GameManager gameManager;
    List<GameObject> timeObjects;
    List<float> timesCopy;

    public GameObject timeObjectPrefab;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        step = (StepCuisson) gameManager.Recipe.Steps[gameManager.StepCurrent];

        textInfo = GameObject.Find("Text_recette").GetComponent<Text>();

        sizeOfOneSec = GetComponent<RectTransform>().sizeDelta.x / 10f;
        timesCopy = new List<float>(step.Times);
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Find("Text").GetComponent<Text>().text = time.ToString();
        foreach (float val in (float[]) timesCopy.ToArray().Clone())
        {
            if(val <= time +  10f)
            {
                Vector3 spawnPos = transform.position + new Vector3(sizeOfOneSec * (val - time), 0f, 0f);
                GameObject go = Instantiate(timeObjectPrefab, spawnPos, Quaternion.identity, transform);
                go.GetComponent<RectTransform>().localPosition = new Vector3(spawnPos.x, 0f, 0f);

                Vector3 size = go.GetComponent<RectTransform>().sizeDelta;
                size.x = sizeOfOneSec * 3;
                go.GetComponent<RectTransform>().sizeDelta = size;

               timesCopy.Remove(val);
            }
        }

        indIngredient = 0;
        for(int i = 0; i < step.Times.Count; i++)
        {
            if (time >= step.Times[i] - 0.5f && time < step.Times[i])
            {
                textInfo.text = GetInfoText(i);
                break;
            }
            if (step.Types[i] == "add")
                indIngredient++;
        }



    }

    string GetInfoText(int i)
    {
        string res = "";
        if (step.Types[i].Contains("fire"))
        {
            res = "Régler le feux sur " + step.Types[i][step.Types[i].Length - 1];
        }
        else 
        {
            switch(step.Types[i])
            {
                case "add" :
                    res = "ajouter la/le/les " + step.Ingredients[indIngredient];
                    break;

                case "mix" :
                    res = "mélanger";
                    break;

                default :
                    break;
            }
        }
        return res;
    }

}

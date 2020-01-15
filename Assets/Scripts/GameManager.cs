using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRecipe(string recipeName)
    {
        recipe = Parser.CreateRecipeFromJSON(Application.dataPath + "/Recipe/" + recipeName + ".json");
        switch(recipe.Steps[0].Name)
        {
            case "Cuisson" :
                SceneManager.LoadScene("Cuisson");
                break;
            case "Coupe" :
                SceneManager.LoadScene("CutScene");
                break;
            default : 
                Debug.Log("Step inconnu");
                break;
        }
    }

    public void nextStep()
    {
        stepCurrent++;
        switch(recipe.Steps[stepCurrent].Name)
        {
            case "Cuisson" :
                SceneManager.LoadScene("Cuisson");
                break;
            case "Coupe" :
                SceneManager.LoadScene("CutScene");
                break;
            default : 
                Debug.Log("Step inconnu");
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;

public static class Parser
{
    public static Recipe CreateRecipeFromJSON(string path)
    {
        JObject encodeRecipe = JObject.Parse(File.ReadAllText(path));
        JArray encodeSteps = (JArray) encodeRecipe["Steps"];
        string nameRecipe = (string) encodeRecipe["Name"];
        List<Step> steps = new List<Step>();

        foreach(JObject encodeStep in encodeSteps)
        {
            switch(encodeStep["Name"].ToString())
            {
                case "Cuisson" :
                    steps.Add(CreateStepCuissonFromJSON(encodeStep));
                    break;
                case "Coupe" :
                    steps.Add(CreateStepCutFromJSON(encodeStep));
                    break;
                default :
                    Debug.LogError("Mini-jeu inconnu");
                    break;
            }
        }

        return new Recipe(nameRecipe, steps);
    }

    static StepCuisson CreateStepCuissonFromJSON(JObject encodeStep)
    {
        string name = (string) encodeStep["Name"];
        List<float> times = encodeStep["Times"].ToObject<List<float>>();
        List<string> ingredients = encodeStep["Ingredients"].ToObject<List<string>>();
        List<string> types = encodeStep["Type"].ToObject<List<string>>();

        return new StepCuisson(name, times, ingredients, types);
    }

    static StepCut CreateStepCutFromJSON(JObject encodeStep)
    {
        string name = (string) encodeStep["Name"];

        return new StepCut(name);
    }
}

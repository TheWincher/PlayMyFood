using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Parser
{
    public static Recipe CreateRecipeFromJSON(string path)
    {
        JObject encodeRecipe = JObject.Parse(Resources.Load<TextAsset>(path).text);
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
        JArray encodeLegumes = (JArray) encodeStep["Legumes"];
        
        List<Legume> legumes = new List<Legume>();
        foreach(JObject encodelLegume in encodeLegumes)
        {
            string nameLegume = (string) encodelLegume["Name"];
            JArray encodeCoupes = (JArray) encodelLegume["coupes"];

            List<Coupe> coupes = new List<Coupe>();
            foreach(JObject encodeCoupe in encodeCoupes)
            {
                float[] start = encodeCoupe["start"].ToObject<float[]>();
                float[] end = encodeCoupe["end"].ToObject<float[]>();

                Vector3 startV = new Vector3(start[0], start[1], start[2]);
                Vector3 endV = new Vector3(end[0], end[1], end[2]);
                coupes.Add(new Coupe(startV, endV));
            }

            legumes.Add(new Legume(nameLegume, coupes));
        }

        return new StepCut(name, legumes);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Recipe
{
    public string Name;
    public List<Step> Steps;

    public Recipe() { }

    public static Recipe CreateFromJSON(string pathFile)
    {
        string dataFromJSON = File.ReadAllText(pathFile);
        return JsonUtility.FromJson<Recipe>(dataFromJSON);
    }
}

[Serializable]
public class Step
{

    public string Name;
    public List<float> Times;
    public List<string> Ingredients;
    public List<string> Type;

    public Step() { }

}

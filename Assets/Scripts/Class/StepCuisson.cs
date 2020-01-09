using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCuisson : Step
{
    List<float> times;
    List<string> ingredients;
    List<string> types;

    public StepCuisson(string n, List<float> t, List<string> i, List<string> typesList)
    {
        name = n;
        times = new List<float>(t);
        ingredients = new List<string>(i);
        types = new List<string>(typesList);
    }

    public List<float> Times
    {
        get {return times;}
    }

    public List<string> Ingredients
    {
        get {return ingredients;}
    }

    public List<string> Types
    {
        get {return types;}
    }
}

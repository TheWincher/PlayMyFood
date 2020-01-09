using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Recipe
{
    public string Name;
    public List<Step> Steps;

    public Recipe(string n, List<Step> s) 
    {
        Name = n;
        Steps = new List<Step>(s);
    }

}

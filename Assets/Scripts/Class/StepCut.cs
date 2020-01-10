using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCut : Step 
{
    List<Legume> legumes;
    public StepCut(string n, List<Legume> l)
    {
        name = n;
        legumes = new List<Legume>(l);
    }

    public List<Legume> Legumes
    {
        get { return legumes;}
    }
}

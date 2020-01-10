using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Legume
{
    string nomLegume;
    List<Coupe> coupes;

    public Legume(string name, List<Coupe> c)
    {
        nomLegume = name;
        coupes = new List<Coupe>(c);
    }
}

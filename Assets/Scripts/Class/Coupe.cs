using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class Coupe
{
    [SerializeField]
    public Vector3 debut;
    [SerializeField]
    public Vector3 fin;

    public Coupe(Vector3 d, Vector3 f)
    {
        debut = d;
        fin = f;
    }

    public float decalage(Coupe b)
    {
        float decDebut = magnitude(b.debut,this.debut);
        float decFin = magnitude(b.fin,this.fin);
        return decDebut+decFin;
    }

    private float magnitude (Vector3 a, Vector3 b)
    {
        Vector2 a2 = new Vector2(a.x, a.z);
        Vector2 b2 = new Vector2(b.x, b.z);
        return (a2 - b2).magnitude;
    }
}

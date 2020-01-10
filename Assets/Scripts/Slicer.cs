using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using DG.Tweening;

public class Slicer : MonoBehaviour
{

    public Transform cutPlane;
    public LayerMask layerMask;
    public Material crossMaterial;

    public GameObject sphere;

    public LineRenderer lrDecoupe;
    public LineRenderer lrRea;

    Vector3 cutPlanePos;
    Vector3 cutPlaneDirection;

    private Vector3 previousMousePos;

    [SerializeField]
    public List<Coupe> coupesPrevu;
    public List<Coupe> coupesRea;

    private List<Legume> legumes;
    int indexLegume = 0;

    // Start is called before the first frame update
    void Start()
    {
        //on récupére les légumes a couper

        lrDecoupe.positionCount = 2;
        lrDecoupe.SetPosition(0, coupesPrevu[0].debut);
        lrDecoupe.SetPosition(1, coupesPrevu[0].fin);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Init");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500f, 9))
            {
                cutPlanePos = hit.point;
                cutPlane.position = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Slice();
        }
        else if (Input.GetMouseButton(0))
        {
            transform.rotation = Camera.main.transform.rotation;
            Debug.Log("clic");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500f, 9))
            {
                cutPlaneDirection = hit.point;

                float angle = Vector3.Angle(cutPlaneDirection - cutPlanePos, Vector3.right);

                if(cutPlaneDirection.z > cutPlanePos.z)
                {
                    angle = -angle;
                }

                //Debug.Log(angle);
                cutPlane.eulerAngles = new Vector3(-90, angle, 0);
            }


            RotatePlane();
        }

    }

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.GetChild(0).position,cutPlane.localScale, cutPlane.rotation, layerMask);

        Debug.Log("Slice");
        Debug.Log(lrRea.name);
        coupesRea.Add(new Coupe(lrRea.GetPosition(0), lrRea.GetPosition(19)));

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            SlicedHull hull = SliceObject(hits[i].gameObject);
            if (hull != null && hits[i].gameObject.name!="CutPlane")
            {
                GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                AddHullComponents(bottom,0);
                AddHullComponents(top,300);
                top.layer = 0;
                Destroy(hits[i].gameObject);
            }
        }

        float diff = coupesRea[coupesRea.Count - 1].decalage(coupesPrevu[coupesRea.Count - 1]);
        Debug.Log(diff);

        //display new découpe
        if (coupesRea.Count != coupesPrevu.Count)
        {
            lrDecoupe.SetPosition(0, coupesPrevu[coupesRea.Count].debut);
            lrDecoupe.SetPosition(1, coupesPrevu[coupesRea.Count].fin);
        }
        else
        {
            Debug.Log("C est gg !!!");
            lrDecoupe.positionCount = 0;
        }
        
    }
    public void AddHullComponents(GameObject go,float explosionForce)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(explosionForce, go.transform.position, 50);
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    public void RotatePlane()
    {
        
        cutPlane.eulerAngles += new Vector3(0, 0, 0);
    }

}

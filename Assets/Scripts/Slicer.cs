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

    Vector3 cutPlanePos;
    Vector3 cutPlaneDirection;

    private Vector3 previousMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
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
            //cutPlane.position = new Vector3(world_pos.x, world_pos.y, world_pos.z);
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

                //Instantiate<GameObject>(sphere, hit.point,Quaternion.identity);

                float angle = Vector3.Angle(cutPlaneDirection - cutPlanePos, Vector3.right);

                if(cutPlaneDirection.z > cutPlanePos.z)
                {
                    angle = -angle;
                }

                Debug.Log(angle);
                cutPlane.eulerAngles = new Vector3(-90, angle, 0);
            }


            RotatePlane();
        }

    }

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.GetChild(0).position,cutPlane.localScale, cutPlane.rotation, layerMask);
        Debug.Log("Slice");

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
                AddHullComponents(top,200);
                Destroy(hits[i].gameObject);
            }
        }
    }
    public void AddHullComponents(GameObject go,float explosionForce)
    {
        go.layer = 9;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(explosionForce, go.transform.position, 20);
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

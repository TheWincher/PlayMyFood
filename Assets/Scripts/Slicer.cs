using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using DG.Tweening;
using TMPro;

public class Slicer : MonoBehaviour
{

    public Transform cutPlane;
    public LayerMask layerMask;
    public Material crossMaterial;

    public GameObject sphere;

    public LineRenderer lrDecoupe;
    public LineRenderer lrRea;

    public TextMeshProUGUI sliceScore;
    public TextMeshProUGUI sliceName;
    private int score = 0;

    Vector3 cutPlanePos;
    Vector3 cutPlaneDirection;

    private Vector3 previousMousePos;

    private Recipe recipe;
    private List<Legume> legumes;
    int indexLegume = 0;
    private GameObject currentLegume;
    private List<GameObject> hullComponents = new List<GameObject>();

    [SerializeField]
    public List<Coupe> coupesPrevu;
    public List<Coupe> coupesRea;

    // Start is called before the first frame update
    void Start()
    {
        //on récupére les légumes a couper
        recipe = Parser.CreateRecipeFromJSON(Application.dataPath + "/Recipe/test.json");
        StepCut stepCut = (StepCut)recipe.Steps[1];

        //remplissages des coupes Prévus
        legumes = stepCut.Legumes;
        coupesPrevu = stepCut.Legumes[indexLegume].Coupes;

        lrDecoupe.positionCount = 2;
        lrDecoupe.SetPosition(0, coupesPrevu[0].debut);
        lrDecoupe.SetPosition(1, coupesPrevu[0].fin);

        //instancier le bon prefab
        switch (legumes[0].NomLegume)
        {
            case "Pain":
                currentLegume = Resources.Load<GameObject>("bread_01");
                break;
            case "Carotte":
                currentLegume = Resources.Load<GameObject>("bread_01");
                break;
            default:
                Debug.LogError("Legume non trouvé");
                break;
        }

        sliceName.text = legumes[0].NomLegume;

        Instantiate(currentLegume);
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
                hullComponents.Add(bottom);
                hullComponents.Add(top);
            }
        }

        float diff = coupesRea[coupesRea.Count - 1].decalage(coupesPrevu[coupesRea.Count - 1]);
        Debug.Log(diff);

        score += CalculateScore(diff);
        sliceScore.text = score.ToString();

        //display new découpe
        if (coupesRea.Count != coupesPrevu.Count)
        {
            lrDecoupe.SetPosition(0, coupesPrevu[coupesRea.Count].debut);
            lrDecoupe.SetPosition(1, coupesPrevu[coupesRea.Count].fin);
        }
        else
        {
           
            lrDecoupe.positionCount = 0;

            indexLegume++;

            if (indexLegume == legumes.Count)
            {
                Debug.Log("C est gg !!!");
            }
            else
            {
                //génération du légume suivant
                for(int i = 0; i < hullComponents.Count; i++)
                {
                    Destroy(hullComponents[i]);
                }
                hullComponents.Clear();
                coupesPrevu = legumes[indexLegume].Coupes;
                coupesRea.Clear();

                lrDecoupe.positionCount = 2;
                lrDecoupe.SetPosition(0, coupesPrevu[0].debut);
                lrDecoupe.SetPosition(1, coupesPrevu[0].fin);

                //instancier le bon prefab
                switch (legumes[0].NomLegume)
                {
                    case "Pain":
                        currentLegume = Resources.Load<GameObject>("bread_01");
                        break;
                    case "Carotte":
                        currentLegume = Resources.Load<GameObject>("bread_01");
                        break;
                    default:
                        Debug.LogError("Legume non trouvé");
                        break;
                }

                sliceName.text = legumes[0].NomLegume;

                Instantiate(currentLegume);
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

    public int CalculateScore(float dist)
    {
        if (dist < 0.1) return 100;
        if (dist < 0.2) return 50;
        return 0;
    }

}

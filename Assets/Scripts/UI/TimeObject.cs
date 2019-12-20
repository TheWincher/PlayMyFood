using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    float sizeOfOneSec;
    // Start is called before the first frame update
    void Start()
    {
        sizeOfOneSec = transform.parent.GetComponent<RectTransform>().sizeDelta.x / 10f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localPosition -= Vector3.right * sizeOfOneSec * Time.deltaTime;

        if(transform.position.x + GetComponent<RectTransform>().sizeDelta.x <= transform.parent.position.x)
        {
            Destroy(gameObject);
        }
    }
}

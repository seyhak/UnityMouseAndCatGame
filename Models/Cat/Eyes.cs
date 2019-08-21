using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField]
    GameObject eyesLocation;
    [SerializeField]
    float length;
    [SerializeField]
    float radius;
    [SerializeField]
    GameObject trgt;
    int layer;
    [SerializeField, Range(0, 9)]
    int ignoredLayer;
    RaycastHit[] hits;
    CatScript cs;

    void Start()
    {
        layer = 1 << 8;
        ignoredLayer= ~(1 << 9);
        cs = gameObject.GetComponent<CatScript>();
    }
    void Update()
    {
        hits = Physics.CapsuleCastAll(eyesLocation.transform.position, eyesLocation.transform.forward * length, radius, eyesLocation.transform.forward * length, length, layer);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Player")
            {
                RaycastHit confirmationHit;
                if (Physics.Linecast(eyesLocation.transform.position, hit.collider.transform.position, out confirmationHit))
                {
                    if (confirmationHit.transform.tag == "Player")
                    {
                        if (!cs.attacking)
                        {
                            cs.chasing = true;
                            cs.menu = 3;
                        }
                    }
                }
            }
        }

        if (cs.chasing)
        {
            RaycastHit confirmationHit;
            if (Physics.Linecast(eyesLocation.transform.position, trgt.transform.position, out confirmationHit,ignoredLayer))
            {
                if (confirmationHit.transform.tag == "Player")
                {
                        cs.chasing = true;
                        cs.menu = 3;
                }
                else
                {
                    Debug.Log("Uciek!");
                    cs.chasing = false;
                }
            }
        }

        Debug.DrawRay(eyesLocation.transform.position, trgt.transform.position*length,Color.cyan);
        Debug.DrawRay(eyesLocation.transform.position, eyesLocation.transform.forward * length, Color.red);

    }
}

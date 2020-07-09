using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkillControler : MonoBehaviour
{
    [SerializeField]
    private LightsRayControler _lightsRayControler = null;

    [SerializeField] private GameObject Normal = null, Hiding = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lightsRayControler.RayByLights(transform.position))
        {
            //Debug.Log("out Shadow");
            Normal.SetActive(true);
            Hiding.SetActive(false);
        }
        else
        {
            //Debug.Log("in Shadow");
            Normal.SetActive(false);
            Hiding.SetActive(true);
        }
    }
    
}

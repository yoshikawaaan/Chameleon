using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightsRayControler : MonoBehaviour
{

    //ShadowSkillControlerから呼び出す
    public bool RayByLights(Vector3 target)
    {
        foreach(Transform childTransform in transform)
        {
            var haveLight = childTransform.GetComponent<Light>();
            if (RayByLight(childTransform, target))
            {
                return true;
            }
        }
        return false;
    }

    bool RayByLight(Transform light, Vector3 target)
    {
        Vector3 pos = light.position;
        Vector3 dir = target - pos;
        
        var angle = GetAngle(light.forward, dir);
        var lightAngle = light.GetComponent<Light>().spotAngle * 0.4f;
        
        //Debug.Log("angle:" + angle + "  lightangle:" + lightAngle);
        
        if (lightAngle < angle)
            return false;
        
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        
        //target(player)に当たったかどうかを返す
        //-- playerだったらtrue
        //-- 障害物ならflase
        if (Physics.Raycast(ray, out hit, 100f))
        {
            var obj = hit.collider.GetComponent<PlayerControler>();
            return obj != null;
        }
        else
            return false;
    }
    
    private double GetAngle(Vector3 v1, Vector3 v2)
    {
        v1.Normalize();
        v2.Normalize();

        var angle = Vector3.Angle(v1, v2);
        return angle;
    }
}
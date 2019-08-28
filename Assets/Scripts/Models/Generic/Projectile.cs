using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target - transform.position;

        float travelFrame = 70 * Time.deltaTime;

        if (dir.magnitude <= travelFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * travelFrame, Space.World);
    }
    void HitTarget()
    {
        Debug.Log("HitTarget");
        Destroy(gameObject);
    }
}

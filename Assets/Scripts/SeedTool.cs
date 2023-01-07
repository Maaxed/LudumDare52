using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeedTool : Tool
{
    public int tryCount = 4;
    public float exclusionRadius = 0.5f;
    public LayerMask exclusionMask;
    public GameObject prefab;
    public Transform cropParent;

    protected override void OnUse()
    {
        Vector3 mousePos = MousePosition;
        List<Collider2D> collidersInArea = Physics2D.OverlapCircleAll(mousePos, radius, exclusionMask).ToList();
        for (int i = 0; i<tryCount; i++)
        {
            Vector3 pos = mousePos + (Vector3)(Random.insideUnitCircle * radius);
            pos.z = 0;
            bool collided = false;
            foreach (var collider in collidersInArea)
            {
                if ((collider.transform.position - pos).sqrMagnitude<exclusionRadius* exclusionRadius)
                {
                    collided = true;
                    break;
                }
            }

            if (collided)
            {
                continue;
            }

            collidersInArea.AddRange(Instantiate(prefab, pos, Quaternion.identity, cropParent).GetComponentsInChildren<Collider2D>());
        }
    }
}

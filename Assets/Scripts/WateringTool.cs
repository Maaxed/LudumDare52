using System.Linq;
using UnityEngine;

public class WateringTool : Tool
{
    public int tryCount = 1;
    public LayerMask mask;

    protected override void OnUse()
    {
        Collider2D[] collidersInArea = Physics2D.OverlapCircleAll(MousePosition, radius, mask);
        Crop[] cropsInArea = collidersInArea.Select(collider => collider.GetComponent<Crop>()).Where(crop => crop != null).ToArray();
        if (cropsInArea.Length == 0)
            return;

        for (int i = 0; i < tryCount; i++)
        {
            cropsInArea[Random.Range(0, cropsInArea.Length)].SetWatered();
        }
    }
}

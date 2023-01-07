using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabTool : Tool
{
    public MouseObject mouse;
    public LayerMask mask;

    private List<LivingPlant> grabbed = new List<LivingPlant>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach (LivingPlant living in grabbed)
            {
                living.Ungrab();
            }

            grabbed.Clear();
        }

        if (toggle.isOn && !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Collider2D[] collidersInArea = Physics2D.OverlapCircleAll(mouse.transform.position, radius, mask);

            Crop[] cropsInArea = collidersInArea.Select(collider => collider.GetComponent<Crop>()).Where(crop => crop != null).ToArray();

            List<LivingPlant> plants = collidersInArea.Select(collider => collider.GetComponent<LivingPlant>()).Where(living => living != null).ToList();

            foreach (Crop crop in cropsInArea)
            {
                LivingPlant harvested = crop.TryHarvest();
                if (harvested != null)
                {
                    plants.Add(harvested);
                }
            }

            grabbed.AddRange(plants);

            foreach (LivingPlant plant in plants)
            {
                plant.Grab(mouse);
            }
        }
    }

    protected override void OnUse()
    { }
}

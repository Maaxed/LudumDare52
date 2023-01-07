using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public float growTime = 1.0f;
    public Animator animator;
    public LivingPlant livingPrefab;
    public Transform livingParent;

    private State state = State.Planted;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (livingParent == null)
        {
            livingParent = transform.parent;
        }
    }
    
    public void SetWatered()
    {
        if (state == State.Planted)
        {
            state = State.Watered;
            StartCoroutine(Water());
        }
    }

    private IEnumerator Water()
    {
        if (animator != null)
            animator.SetTrigger("water");

        yield return new WaitForSeconds(growTime);

        state = State.Grown;

        if (animator != null)
            animator.SetTrigger("grow");

    }

    public LivingPlant TryHarvest()
    {
        if (state != State.Grown)
            return null;

        Destroy(gameObject);
        return Instantiate(livingPrefab, transform.position, Quaternion.identity, livingParent);
    }

    private enum State
    {
        Planted,
        Watered,
        Grown
    }
}

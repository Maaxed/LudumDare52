using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class Tool : MonoBehaviour
{
    public Camera cam;
    public float radius = 2.0f;
    public Toggle toggle;

    protected Vector3 MousePosition => cam.ScreenToWorldPoint(Input.mousePosition);

    void Awake()
    {
        if (toggle == null)
        {
            toggle = GetComponentInChildren<Toggle>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggle.isOn && !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0))
        {
            OnUse();
        }
    }

    protected abstract void OnUse();
}

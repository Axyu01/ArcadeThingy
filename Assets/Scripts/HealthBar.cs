using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    Entity entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInParent<Entity>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + GameManager.Instance.MainCamera.transform.forward);
        UpdateHelath();
    }
    void UpdateHelath()
    {
        if (slider != null && entity != null)
        {
            slider.value = entity.Health / entity.MAX_HEALTH;
        }
    }
}

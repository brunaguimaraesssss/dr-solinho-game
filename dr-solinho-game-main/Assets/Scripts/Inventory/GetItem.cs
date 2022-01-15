using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public static event System.Action<ItemScript> ToInventory;
    public static event System.Action<ItemReference> UpdateQtd;

    private bool m_IsCreated;
    private ItemReference m_Reference;


    private void Start()
    {
        m_Reference = GetComponent<ItemReference>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !m_IsCreated)
        {
            ToInventory?.Invoke(m_Reference.GetItem());
            UpdateQtd?.Invoke(m_Reference);
            Destroy(this.gameObject);
        }       
    }

    private void OnTriggerExit2D(Collider2D collision) =>
        m_IsCreated = false;

    public void Created() =>
        m_IsCreated = true;
}

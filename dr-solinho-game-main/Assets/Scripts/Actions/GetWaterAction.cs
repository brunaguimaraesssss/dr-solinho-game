using UnityEngine;
using UnityEngine.EventSystems;

public class GetWaterAction : Actions
{
    [SerializeField]
    private ItemReference m_Reference=null;

    public override void Control()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0.5f, 0);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if(hit)
            {
                if(hit.collider.CompareTag("Water"))
                {
                    ToInventoryEvent(m_Reference.GetItem());
                    UpdateQtdEvent(m_Reference);
                    Sucess();
                }
                else
                    Failed();

            }
            else
                Failed();
        }
    }
}

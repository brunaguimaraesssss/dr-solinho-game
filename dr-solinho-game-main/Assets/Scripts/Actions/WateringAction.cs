using UnityEngine;
using UnityEngine.EventSystems;

public class WateringAction : Actions, IGameStart
{
    public override void Control()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if(hit)
            {
                if(hit.collider.CompareTag("Planting") && HaveItem("Água"))
                {
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

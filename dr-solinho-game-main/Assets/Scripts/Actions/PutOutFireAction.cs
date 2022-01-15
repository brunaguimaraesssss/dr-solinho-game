using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PutOutFireAction : Actions
{
    [SerializeField]
    private Tilemap m_TileMap = null;


    public override void Control()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int currentCell = m_TileMap.WorldToCell(pos);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if(hit)
            {
                if(hit.collider.CompareTag("Fire") && HaveItem("Água"))
                {
                    m_TileMap.SetTile(currentCell, null);
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

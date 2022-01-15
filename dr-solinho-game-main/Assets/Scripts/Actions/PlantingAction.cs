using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlantingAction : Actions
{
    [SerializeField]
    private Tile m_Tile = null;
    [SerializeField]
    private Tilemap m_TileMap = null;


    public override void Control()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int currentCell = m_TileMap.WorldToCell(pos);

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(HaveItem("Semente"))
            {
                m_TileMap.SetTile(currentCell, m_Tile);
                Sucess();
                QuestCount();
            }
            else
                Failed();
        }
    }

}

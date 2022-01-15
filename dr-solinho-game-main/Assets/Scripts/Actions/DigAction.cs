using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DigAction : Actions
{
    [SerializeField]
    private RuleTile m_Tile = null;
    [SerializeField]
    private Tilemap m_TileMap = null;

    private int m_Count=0;

    public override void Control()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int currentCell = m_TileMap.WorldToCell(pos);

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            m_TileMap.SetTile(currentCell, m_Tile);
            m_Count++;

            if(m_Count % 7 == 0)
                QuestCount();
            
        }
    }

}
    
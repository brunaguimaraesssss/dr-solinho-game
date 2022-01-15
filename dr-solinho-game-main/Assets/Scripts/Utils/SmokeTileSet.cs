using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SmokeTileSet : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Particle = null;

    private Tilemap m_TileMap;
    private GameObject[] m_Smoke;
    private List<Vector3Int> m_TilePos = new List<Vector3Int>();

    void Start()
    {

        m_TileMap = GetComponent<Tilemap>();


        foreach(var pos in m_TileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = m_TileMap.CellToWorld(localPlace);
            if(m_TileMap.HasTile(localPlace))
            {
                m_TilePos.Add(localPlace);
            }

        }

        m_Smoke = new GameObject[m_TilePos.Count];


        for(int i = 0; i < m_TilePos.Count; i++)
        {
            m_Smoke[i] = new GameObject("Smoke")
            {
                layer = 10
            };
            m_Smoke[i] = Instantiate(m_Particle);
            m_Smoke[i].transform.SetParent(transform);
            m_Smoke[i].transform.position = m_TileMap.CellToWorld(m_TilePos[i]);


        }

    }

    private void Update()
    {
        for(int i = 0; i < m_TilePos.Count; i++)
        {
            var tile = m_TileMap.GetTile(m_TilePos[i]);
            if(!tile)
            {
                m_Smoke[i].SetActive(false);
            }
        }
    }

}

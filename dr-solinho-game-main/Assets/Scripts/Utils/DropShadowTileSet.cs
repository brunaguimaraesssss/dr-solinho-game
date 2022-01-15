using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DropShadowTileSet : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_ShadowOffset = Vector2.zero;
    [SerializeField]
    private Vector2 m_ShadowScale = Vector2.zero;
    [SerializeField]
    private Material m_ShadowMaterial = null;


    public Vector3 m_Rotation;

    private Tilemap m_TileMap;
    private TilemapRenderer m_Renderer;
    private GameObject[] m_Shadow;
    private List<Vector3Int> m_TilePos = new List<Vector3Int>();

    void Start()
    {

        m_TileMap = GetComponent<Tilemap>();
        m_Renderer = GetComponent<TilemapRenderer>();       
        

        foreach(var pos in m_TileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = m_TileMap.CellToWorld(localPlace);
            if(m_TileMap.HasTile(localPlace))
            {
                m_TilePos.Add(localPlace);                   
            }

        }

        m_Shadow = new GameObject[m_TilePos.Count];
       

        for(int i =0; i< m_TilePos.Count; i++)
        {
            m_Shadow[i] = new GameObject("Shadow");
            m_Shadow[i].layer = 10;
            m_Shadow[i].AddComponent<SpriteRenderer>().sprite = m_TileMap.GetSprite(m_TilePos[i]);
            m_Shadow[i].GetComponent<SpriteRenderer>().material = m_ShadowMaterial;
            m_Shadow[i].GetComponent<SpriteRenderer>().sortingOrder = m_Renderer.sortingOrder - 1;
            m_Shadow[i].GetComponent<SpriteRenderer>().sortingLayerName = m_Renderer.sortingLayerName;
            m_Shadow[i].gameObject.transform.localScale = m_ShadowScale;
            m_Shadow[i].transform.SetParent(transform);
            m_Shadow[i].transform.position = m_TileMap.CellToWorld(m_TilePos[i]) + (Vector3)m_ShadowOffset;

            m_Shadow[i].transform.rotation = Quaternion.Euler(m_Rotation);

        }

    }

    private void Update()
    {
        for(int i = 0; i < m_TilePos.Count; i++)
        {
            m_Shadow[i].GetComponent<SpriteRenderer>().sprite = m_TileMap.GetSprite(m_TilePos[i]);
            m_Shadow[i].GetComponent<SpriteRenderer>().material = m_ShadowMaterial;
            m_Shadow[i].gameObject.transform.localScale = m_ShadowScale;
            m_Shadow[i].transform.position = m_TileMap.CellToWorld(m_TilePos[i]) + (Vector3)m_ShadowOffset;
            m_Shadow[i].transform.rotation = Quaternion.Euler(m_Rotation);

        }
    }

}

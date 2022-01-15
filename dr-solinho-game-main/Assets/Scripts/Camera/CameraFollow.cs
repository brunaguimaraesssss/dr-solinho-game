using UnityEngine;

public class CameraFollow : MonoBehaviour, IGameStart, IGamePlay
{
    public Transform m_Player;

    public float m_Speed;
    public Transform m_LimitLeft, m_LimitRight, m_LimitUp, m_LimitDown;    

    public void Init()
    {
        CamController();
    }

    void CamController()
    {
        float posCamX = m_Player.position.x;
        float posCamY = m_Player.position.y;

        if(transform.position.x < m_LimitLeft.transform.position.x && m_Player.position.x < m_LimitLeft.transform.position.x)
            posCamX = m_LimitLeft.transform.position.x;

        else if(transform.position.x > m_LimitRight.transform.position.x && m_Player.position.x > m_LimitRight.transform.position.x)
            posCamX = m_LimitRight.transform.position.x;

        if(transform.position.y > m_LimitUp.transform.position.y && m_Player.position.y > m_LimitUp.transform.position.y)
            posCamY = m_LimitUp.transform.position.y;

        else if(transform.position.y < m_LimitDown.transform.position.y && m_Player.position.y < m_LimitDown.transform.position.y)
            posCamY = m_LimitDown.transform.position.y;

        Vector3 posCam = new Vector3(posCamX, posCamY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, posCam, m_Speed * Time.deltaTime);

    }

    public void GamePlay()
    {
        CamController();
    }
}

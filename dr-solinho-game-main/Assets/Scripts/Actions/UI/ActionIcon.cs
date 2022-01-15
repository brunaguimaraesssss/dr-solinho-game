using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionIcon : MonoBehaviour, IGameStart
{
    [SerializeField]
    private ActionSet[] m_Actions= null;

    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;

    private bool m_IsActionSet;

    private Dictionary<string, Sprite> m_Icons = new Dictionary<string, Sprite>();
    private Dictionary<string, RuntimeAnimatorController> m_AnimatorSets = new Dictionary<string, RuntimeAnimatorController>();

    public void Init()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        ActionsManager.SetAction += SetAnimation;
        ActionsManager.SetAction += SetIcon;
        ActionsManager.SetHold += EnableAction;




        foreach(ActionSet action in m_Actions)
        {
            m_Icons.Add(action.name, action.sprite);
            m_AnimatorSets.Add(action.name, action.animation);
        }

    }

    private void OnDestroy()
    {
        ActionsManager.SetAction -= SetAnimation;
        ActionsManager.SetAction -= SetIcon;
        ActionsManager.SetHold -= EnableAction;
    }

    private void SetIcon(string action)
    {
        m_SpriteRenderer.sprite =
                m_Icons.TryGetValue(action, out Sprite sprite) ? sprite : null;

        m_SpriteRenderer.enabled = true;
        Cursor.visible = false;

    }

    private void SetAnimation(string action)
    {
        m_Animator.runtimeAnimatorController =
            m_AnimatorSets.TryGetValue(action, out RuntimeAnimatorController anim) ? anim : null;

        m_Animator = GetComponent<Animator>();
    }

    public void EnableAction(bool hold)
    {
        m_IsActionSet = hold;
        m_SpriteRenderer.enabled = hold; 
    }

    private void Update()
    {
        transform.position = MousePosition();
        if(m_IsActionSet)
        {
            StartAnimation();
            CursorOnOff();
        }
        
    }

    private void CursorOnOff()
    {
        Cursor.visible = EventSystem.current.IsPointerOverGameObject() ? true : false;
    }

    private void StartAnimation() => m_Animator.SetBool("isAnimating", Input.GetMouseButton(0));
    

    private Vector3 MousePosition()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x, pos.y+0.25f, 0);
    }
}

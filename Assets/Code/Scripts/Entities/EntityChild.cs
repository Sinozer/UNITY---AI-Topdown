// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EntityChild : MonoBehaviour
{
    public Entity Entity
    {
        get
        {
            if (_entity == null)
                _entity = transform.root.GetComponentInChildren<Entity>();

            return _entity;
        }
    }
    private Entity _entity;

    public GameObject Render
    {
        get
        {
            if (_render == null)
                _render = transform.root.Find("Render").gameObject;

            return _render;
        }
    }
    private GameObject _render;

    public GameObject Physics
    {
        get
        {
            if (_physics == null)
                _physics = transform.root.Find("Physics").gameObject;

            return _physics;
        }
    }
    private GameObject _physics;

    public GameObject Actions
    {
        get
        {
            if (_actions == null)
                _actions = transform.root.Find("Actions").gameObject;

            return _actions;
        }
    }
    private GameObject _actions;
}
// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

abstract public class EntityChild : ObjectChild<Entity>
{
    public Entity Entity => Object;

    public EntityBrain Brain
    {
        get
        {
            if (_brain == null)
                _brain = Entity.GetComponentInChildren<EntityBrain>();

            return _brain;
        }
    }
    private EntityBrain _brain;

    public Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = Entity.GetComponentInChildren<Animator>();

            return _animator;
        }
    }
    private Animator _animator;

    #region Checks
    public bool NPC => Entity.IsNpc;
    public bool NotNPC => !Entity.IsNpc;
    public bool Alive => Entity.IsAlive;
    public bool Dead => Entity.IsDead;
    #endregion Checks
}
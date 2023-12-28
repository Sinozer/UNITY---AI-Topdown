// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;

public class EntityDash : EntityChild, IEntityAction
{
    private bool _canDash = true;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashPower;

    public void TryDash()
    {
        if (_canDash == false)
            return;

        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        _canDash = false;

        AudioManager.PlaySFX("Dash");
        Vector2 _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (_targetPosition - (Vector2)Entity.transform.position).normalized;
        Rigidbody2D.velocity += _dashPower * dashDirection;

        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    public void SetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void ResetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }
}
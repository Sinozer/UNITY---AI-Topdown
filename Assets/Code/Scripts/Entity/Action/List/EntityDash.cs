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
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashPower;

    private Coroutine _dashCoroutine;
    public float LastTimeDash => _lastTimeDash;
    private float _lastTimeDash;

    public bool IsInDashCooldown => _lastTimeDash + _dashCooldown > Time.time;

    public void StartDash()
    {
        if (_dashCoroutine != null)
            return;

        _dashCoroutine = StartCoroutine(Dash());
    }

    public void StopDash()
    {
        if (_dashCoroutine == null)
            return;

        StopCoroutine(_dashCoroutine);

        _dashCoroutine = null;
    }

    private IEnumerator Dash()
    {
        if (IsInDashCooldown)
            yield return new WaitForSeconds(_lastTimeDash + _dashCooldown - Time.time);

        AudioManager.PlaySFX("Dash");
        Vector2 _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (_targetPosition - (Vector2)Entity.transform.position).normalized;
        Rigidbody2D.velocity += _dashPower * dashDirection;

        _lastTimeDash = Time.time;

        yield return null;
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
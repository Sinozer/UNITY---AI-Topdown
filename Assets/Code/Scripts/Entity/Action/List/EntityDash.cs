// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EntityDash : EntityAction
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AudioSource _sfxDash;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashPower;

    private float _timer;
    private Vector2 _dashDirection;

    private void Awake()
    {
        _timer = Time.time - _dashCooldown;
    }

    public void TryDash()
    {
        if (_timer + _dashCooldown < Time.time)
        {
            _timer = Time.time;
            Dash();
        }
    }

    private void Dash()
    {
        if(_sfxDash != null)
            _sfxDash.Play();
        Vector2 _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _dashDirection = (_targetPosition - (Vector2)Entity.transform.position).normalized;
        _rb.velocity += 5 * _dashDirection * _dashPower;
        //_rb.AddForce(_dashDirection * _dashPower * 500, ForceMode2D.Impulse);
    }
}
// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _aim;
    public GameObject Aim => _aim;

    private void Start()
    {
        if (_aim == null)
            _aim = GameObject.FindGameObjectWithTag("Aim");
    }
}
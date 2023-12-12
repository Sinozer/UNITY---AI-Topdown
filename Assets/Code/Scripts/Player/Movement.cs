// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Player;
    
    public Vector2 MoveInput { get; set; }
    
    [Header("Sats")]
    public float Speed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Move()
    {
        Player.Translate(MoveInput * (Time.deltaTime * Speed));
    }
}
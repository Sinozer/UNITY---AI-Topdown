// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform _player;

    private void Start()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public Transform GetPlayer()
    {
        return _player;
    }
}
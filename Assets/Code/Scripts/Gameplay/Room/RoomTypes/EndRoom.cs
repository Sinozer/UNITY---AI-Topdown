// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EndRoom : Room
{
    public int NextLevelId => _nextLevelId;
    [SerializeField] private int _nextLevelId;
    
    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.End;
    }

    protected override void Update()
    {
        base.Update();
    }
}
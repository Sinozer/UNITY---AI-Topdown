// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class JoinRoom : Room
{
    protected override void Start()
    {
        base.Start();
        
        _roomType = ERoomType.Join;
        _isLocked = false;
        
    }

    protected override void Update()
    {
        base.Update();
    }
}
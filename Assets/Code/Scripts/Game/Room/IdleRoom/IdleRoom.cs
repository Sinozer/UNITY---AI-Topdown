// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class IdleRoom : Room
{
    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.Idle;
    }

    protected override void Update()
    {
        base.Update();
    }
}
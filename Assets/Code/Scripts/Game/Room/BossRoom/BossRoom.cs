// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class BossRoom : Room
{
    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.Boss;
    }

    protected override void Update()
    {
        base.Update();
    }
}
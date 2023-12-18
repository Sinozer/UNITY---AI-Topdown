// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class TreasureRoom : Room
{
    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.Treasure;
    }

    protected override void Update()
    {
        base.Update();
    }
}
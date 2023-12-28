// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class CombatRoom : Room
{
    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.Combat;
    }

    protected override void Update()
    {
        base.Update();
    }
}
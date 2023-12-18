// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

//[CreateAssetMenu(fileName = "Managers", menuName = "Managers")]
public class Managers : ScriptableObject
{
    [SerializeField] private GameManager _gameManager;
    public GameManager GameManager { get { return _gameManager; } }
}
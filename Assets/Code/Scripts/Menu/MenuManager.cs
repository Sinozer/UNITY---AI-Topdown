// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 01/01/24
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage the menus.
/// </summary>
/// <remarks> You cannot add new menus at runtime. </remarks>
public class MenuManager : Singleton<MenuManager>
{
    #region Events
    /// <summary>
    /// This event is called when the menu is shown.
    /// </summary>
    public event System.Action OnMenuShow;

    /// <summary>
    /// This event is called when the menu is hidden.
    /// </summary>
    public event System.Action OnMenuHide;

    /// <summary>
    /// This event is called when a menu is opened.
    /// </summary>
    /// <returns> The name of the menu. </returns>
    public event System.Action<string> OnMenuOpen;

    /// <summary>
    /// This event is called when a menu is closed.
    /// </summary>
    /// <returns> The name of the menu. </returns>
    public event System.Action<string> OnMenuClose;
    #endregion Events

    /// <summary>
    /// The current menus stack.
    /// </summary>
    private Stack<GameObject> _stack;

    /// <summary>
    /// The list of menus.
    /// </summary>
    /// <remarks>
    /// The default menu should be named like the <see cref="DefaultMenuName"/> field.
    /// </remarks>
    private Dictionary<string, GameObject> _menus;

    /// <summary>
    /// The default menu name.
    /// </summary>
    // Show it in the inspector.
    [OdinSerialize, ShowInInspector]
    public string DefaultMenuName { get; set; } = "Main";

    /// <summary>
    /// This tells if the menu need to be shown.
    /// </summary>
    [Title("Menu Manager", "Menu manager class related inspector", TitleAlignments.Centered)]
    [SerializeField] private bool _isMenuOpen;
    public bool IsMenuOpen
    {
        get => _isMenuOpen;
        set
        {
            _isMenuOpen = value;
            if (_isMenuOpen == true)
            {
                OnMenuShow?.Invoke();

                OpenMenu(DefaultMenuName);
            }
            else
            {
                CloseAllMenus();

                OnMenuHide?.Invoke();
            }
        }
    }

    /// <summary>
    /// This method try to show the menu with the given name.
    /// </summary>
    /// <param name="menuName"> The name of the menu to show which need to be in the <see cref="_menus"/> dictionnary. </param>
    /// <returns> True if the menu was found and shown, false otherwise. </returns>
    public bool OpenMenu(string menuName)
    {
        if (_menus.TryGetValue(menuName, out GameObject menu) == false)
        {
            Debug.LogError($"Menu \"{menuName}\" not found.");
            return false;
        }

        OnMenuOpen?.Invoke(menuName);

        if (_stack.Count > 0)
            _stack.Peek().SetActive(false);

        _stack.Push(menu);
        menu.SetActive(true);

        return true;
    }

    /// <summary>
    /// This method try to unshow the current menu.
    /// </summary>
    public void CloseMenu()
    {
        switch (_stack.Count)
        {
            case 0:
                return;
            case 1:
                OnMenuClose?.Invoke(_stack.Peek().name);

                IsMenuOpen = false;
                break;
            default:
                OnMenuClose?.Invoke(_stack.Peek().name);

                _stack.Pop().SetActive(false);
                _stack.Peek().SetActive(true);
                break;
        }
    }

    /// <summary>
    /// This method try to unshow all the menus.
    /// </summary>
    /// <remarks> This method is called when the <see cref="IsMenuOpen"/> property is set to false, so it doesn't need to be called manually. </remarks>
    private void CloseAllMenus()
    {
        while (_stack.Count > 0)
        {
            OnMenuClose?.Invoke(_stack.Peek().name);

            _stack.Pop().SetActive(false);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _stack ??= new Stack<GameObject>();
        _menus ??= new Dictionary<string, GameObject>();

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            _menus.Add(child.name, child.gameObject);
        }

        IsMenuOpen = _isMenuOpen;
    }
}
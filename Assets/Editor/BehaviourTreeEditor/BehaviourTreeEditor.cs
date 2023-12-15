using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset _visualTreeAsset = default;

    private BehaviourTreeView _treeView;
    private InspectorView _inspectorView;
    
    [MenuItem("Tools/Behavior Tree Editor")]
    public static void ShowExample()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("Behavior Tree Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        _visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Editor/BehaviorTreeEditor/BehaviorTreeEditor.uxml");
        _visualTreeAsset.CloneTree(root);
        
        StyleSheet styleSheet = 
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor/BehaviorTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<BehaviourTreeView>();
        _inspectorView = root.Q<InspectorView>();
    }

    private void OnSelectionChange()
    {
        if (Selection.activeObject is BehaviourTree)
        {
            _treeView.PopulateView(Selection.activeObject as BehaviourTree);
        }
    }
}
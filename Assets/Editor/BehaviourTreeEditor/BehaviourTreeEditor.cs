using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset _visualTreeAsset = default;

    private BehaviourTreeView _treeView;
    private InspectorView _inspectorView;
    private BlackboardView _blackboardView;
    
    [MenuItem("Tools/Behaviour Tree Editor")]
    public static void CreateWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("Behaviour Tree Editor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject is BehaviourTree)
        {
            CreateWindow();
            return true;
        }

        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        _visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Editor/BehaviourTreeEditor/BehaviourTreeEditor.uxml");
        _visualTreeAsset.CloneTree(root);
        
        StyleSheet styleSheet = 
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<BehaviourTreeView>();
        _inspectorView = root.Q<InspectorView>();
        _blackboardView = root.Q<BlackboardView>();
        _treeView.OnNodeSelected = OnNodeSelectionChanged;
        Selection.selectionChanged += TreeSelected;
        TreeSelected();
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        switch(change)
        {
            case PlayModeStateChange.EnteredPlayMode:
                TreeSelected();
                break;
            case PlayModeStateChange.EnteredEditMode:
                TreeSelected();
                break;
        }
    }

    private void TreeSelected()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            _treeView.PopulateView(tree);
            if (_blackboardView != null)
                _blackboardView.InspectBlackboard(tree.Blackboard);
            else
                Debug.Log("Blackboard view is null");
            return;
        }

        if (!Selection.activeGameObject)
            return;

        BehaviorTreeRunner tr = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
        if (!tr)
            return;

        if(tr.Tree)
        {
            _treeView.PopulateView(tr.Tree);
            _blackboardView.InspectBlackboard(tr.Tree.Blackboard);
        }
    }

    private void OnNodeSelectionChanged(NodeView node)
    {
        _inspectorView.UpdateSelection(node);
    }

    private void Update()
    {
        if (!Application.isPlaying)
            return;

        _treeView?.UpdateTreeGUI();
    }
}
// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Current phase
    public int Phase => _phase;
    [SerializeField] protected int _phase = 0;

    // List of base data for each phase
    // Can't be empty
    public List<SOEntity> PhaseBaseData => _phaseBaseData;
    [SerializeField, InlineEditor] protected List<SOEntity> _phaseBaseData;

#if UNITY_EDITOR
    [Button("Add New Base Data")]
    public void AddEntity()
    {
        var newEntity = ScriptableObject.CreateInstance<SOEntity>();
        newEntity.name = "Phase_";

        var path = $"Assets/Code/ScriptableObjects/Entities/List/Boss/{newEntity.name}0.asset";
        var i = 1;
        while (System.IO.File.Exists(path))
        {
            path = $"Assets/Code/ScriptableObjects/Entities/List/Boss/{newEntity.name}{i}.asset";
            i++;
        }
        UnityEditor.AssetDatabase.CreateAsset(newEntity, path);

        _phaseBaseData.Add(newEntity);

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif

    // Base data for current phase
    public SOEntity BaseData => _baseData;

    new private void Awake()
    {
        if (_phaseBaseData == null)
            throw new System.Exception("Phase base data is null");

        _baseData = _phaseBaseData[_phase];

        base.Awake();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
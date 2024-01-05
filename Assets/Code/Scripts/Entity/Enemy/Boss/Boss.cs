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
    // List of base data for each phase
    // Can't be empty
    public List<SOPhase> PhaseBaseData => _phaseBaseData;
    [Required, SerializeField, InlineEditor] protected List<SOPhase> _phaseBaseData;

#if UNITY_EDITOR
    [Button("Add New Base Data")]
    public void AddEntity()
    {
        var newPhase = ScriptableObject.CreateInstance<SOPhase>();
        newPhase.name = "Phase_";

        var path = $"Assets/Code/ScriptableObjects/Entities/List/Boss/{newPhase.name}0.asset";
        var i = 1;
        while (System.IO.File.Exists(path))
        {
            path = $"Assets/Code/ScriptableObjects/Entities/List/Boss/{newPhase.name}{i}.asset";
            i++;
        }
        UnityEditor.AssetDatabase.CreateAsset(newPhase, path);

        _phaseBaseData.Add(newPhase);

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif

    // Base data for current phase
    public SOEntity BaseData
    {
        get => _baseData;
        set
        {
            _baseData = value;
            SetValuesFromBaseData(true);
        }
    }

    new private void Awake()
    {
        if (_phaseBaseData == null || _phaseBaseData.Count == 0)
            throw new System.Exception("Phase base data is null");

        _baseData = _phaseBaseData[0];

        base.Awake();
    }
}
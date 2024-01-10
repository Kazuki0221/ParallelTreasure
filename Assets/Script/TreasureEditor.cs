using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(TreasureController))]
public class TreasureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TreasureController instance = target as TreasureController;

        switch (instance.TreasureType)
        {
            case TreasureType.Speed:
                {
                    instance._speed = EditorGUILayout.FloatField("Speed", instance._speed);
                    instance._speedBufImage = EditorGUILayout.ObjectField("BuffImage", instance._speedBufImage, typeof(GameObject), true) as GameObject;
                }
                break;
            case TreasureType.Jump:
                {
                    instance._jumpPower = EditorGUILayout.FloatField("JumpPower", instance._jumpPower);
                    instance._jumpBufImage = EditorGUILayout.ObjectField("BuffImage", instance._jumpBufImage, typeof(GameObject), true) as GameObject;
                }
                break;
            default:
                break;
        }

    }
}
#endif
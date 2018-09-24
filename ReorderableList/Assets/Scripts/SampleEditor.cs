using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Sample))]
public class SampleEditor : Editor
{
	private ReorderableList list;

	private void OnEnable()
	{
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("sampleData"), true, true, true, true);
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Sample))]
public class SampleEditor : Editor
{
	private bool flag;

	private bool isWarning
	{
		set
		{
			flag = value;
			if(flag)
			{
                list.onRemoveCallback =
                (ReorderableList list) =>
                {
                    if (EditorUtility.DisplayDialog("Check again", "Are you sure remove this?", "Sure", "No"))
                    {
                        ReorderableList.defaultBehaviours.DoRemoveButton(list);
                    }
                };
			}
			else
			{
                list.onRemoveCallback =
				(ReorderableList list) =>
				{
					ReorderableList.defaultBehaviours.DoRemoveButton(list);
				};
            }
		}
		get
		{
			return flag;
		}
	}
	private ReorderableList list;
	
	private void OnEnable()
	{
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("sampleData"), true, true, true, true);

		list.drawHeaderCallback = 
		(Rect rect) =>
		{
			EditorGUI.LabelField(rect, "SampleData");
		};

		list.drawElementCallback = 
		(Rect rect, int index, bool isActive, bool isFocused) => 
		{
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("name"), GUIContent.none);

				
			EditorGUI.PropertyField(
				new Rect(rect.x + 60, rect.y, rect.width - 60, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("damage"), GUIContent.none);
		};
	}

	public override void OnInspectorGUI()
	{
		isWarning = EditorGUILayout.Toggle("isWarning", isWarning);
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}

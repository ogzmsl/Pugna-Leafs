
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MegaBookPageAttach))]
public class MegaBookPageAttachEditor : Editor
{
	public override void OnInspectorGUI()
	{
		MegaBookPageAttach mod = (MegaBookPageAttach)target;

#if !UNITY_5 && !UNITY_2017 && !UNITY_2018 && !UNITY_2019 && !UNITY_2020 && !UNITY_2021 && !UNITY_2022
		EditorGUIUtility.LookLikeControls();
#endif

		mod.book = (MegaBookBuilder)EditorGUILayout.ObjectField("Book", mod.book, typeof(MegaBookBuilder), true);

		if ( mod.book )
			mod.page = EditorGUILayout.IntSlider("Page", mod.page, 0, mod.book.NumPages);

		mod.attachforward = EditorGUILayout.Vector3Field("Attach Fwd", mod.attachforward);
		mod.AxisRot = EditorGUILayout.Vector3Field("Axis Rot", mod.AxisRot);
		mod.offset = EditorGUILayout.FloatField("Offset", mod.offset);
		mod.visibleObj = (GameObject)EditorGUILayout.ObjectField("Visible Obj", mod.visibleObj, typeof(GameObject), true);

		Vector3 pos = EditorGUILayout.Vector3Field("Pos", mod.pos);
		if ( pos != mod.pos )
		{
			mod.pos = pos;
			mod.AttachItUV();
		}

		if ( GUI.changed )
			EditorUtility.SetDirty(mod);
	}
}
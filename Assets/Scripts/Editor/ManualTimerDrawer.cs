using UnityEditor;
using UnityEngine;

namespace GameJam
{
	// A property drawer customizes how something appears in the editor.
	//
	// This property drawer creates a single-line progress bar and float-field
	// to edit the maximum time.
	//
	// For more information, check out editor scripting in the Unity
	// documentation.

	[CustomPropertyDrawer(typeof(ManualTimer))]
	public class ManualTimerDrawer : PropertyDrawer
	{
		private const float Pad = 3;

		public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(pos, label, prop);
			var r = EditorGUI.PrefixLabel(pos, label);
			float width = (r.width - Pad) / 3;
			var prevIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			SerializedProperty duration = prop.FindPropertyRelative("duration");
			SerializedProperty elapsed = prop.FindPropertyRelative("elapsed");

			// Bar
			r.width = 2 * width;
			var progress = Mathf.Clamp01(elapsed.floatValue / duration.floatValue);
			EditorGUI.ProgressBar(r, progress, string.Format("{0:f2}s", elapsed.floatValue));

			// Field
			r.x += r.width + Pad;
			r.width = width;
			EditorGUI.PropertyField(r, duration, GUIContent.none);

			EditorGUI.indentLevel = prevIndent;
			EditorGUI.EndProperty();
		}
	}
}

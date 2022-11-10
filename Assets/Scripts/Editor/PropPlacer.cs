using UnityEditor;
using UnityEngine;

public enum PlacerShape
{
	Sphere,
	Cuboid
}

public class PropPlacer : EditorWindow
{
	[MenuItem("Custom/Prop Placer")]
	public static void OpenPlacer() => GetWindow<PropPlacer>();

	public PlacerShape placerShape = PlacerShape.Sphere;
	public Vector3 placerSize = Vector3.one;
	public float placerDiameter = 1;

	public float spawnDensity = 5f;

	Vector3[] randPoints;
	Vector3 placerPoint;

	SerializedObject so;
	SerializedProperty propPlacerShape;
	SerializedProperty propPlacerSize;
	SerializedProperty propPlacerDiameter;
	SerializedProperty propSpawnDensity;

	private void OnEnable()
	{
		SceneView.duringSceneGui += DuringSceneGUI;

		so = new SerializedObject(this);

		propPlacerShape = so.FindProperty("placerShape");
		propPlacerSize = so.FindProperty("placerSize");
		propPlacerDiameter = so.FindProperty("placerDiameter");
		propSpawnDensity = so.FindProperty("spawnDensity");

		PlaceRandomPoints();
	}
	private void OnDisable() => SceneView.duringSceneGui -= DuringSceneGUI;

	private void OnGUI()
	{
		so.Update();

		EditorGUILayout.PropertyField(propPlacerShape);
		switch (placerShape)
		{
			case PlacerShape.Sphere:
				EditorGUILayout.PropertyField(propPlacerDiameter);
				propPlacerDiameter.floatValue = Mathf.Max(propPlacerDiameter.floatValue, 0.1f);
				break;
			case PlacerShape.Cuboid:
				EditorGUILayout.PropertyField(propPlacerSize);
				Vector3 placerPoint = propPlacerSize.vector3Value;
				placerPoint.x = Mathf.Max(placerPoint.x, 0.1f);
				placerPoint.y = Mathf.Max(placerPoint.y, 0.1f);
				placerPoint.z = Mathf.Max(placerPoint.z, 0.1f);
				propPlacerSize.vector3Value = placerPoint;
				break;
		}
		EditorGUILayout.PropertyField(propSpawnDensity);

		if (GUILayout.Button("Place Props"))
		{
			PlaceRandomPoints();
			SceneView.RepaintAll();
		}

		if (so.ApplyModifiedProperties())
		{
			SceneView.RepaintAll();
		}

		if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
		{
			GUI.FocusControl(null);
			Repaint();
		}
	}

	void DuringSceneGUI(SceneView sceneView)
	{
		Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

		if (Event.current.type == EventType.MouseMove)
		{
			sceneView.Repaint();
		}

		bool holdingAlt = (Event.current.modifiers & EventModifiers.Alt) != 0;

		if (Event.current.type == EventType.ScrollWheel && holdingAlt)
		{
			float scrollDir = Mathf.Sign(Event.current.delta.y);

			so.Update();

			switch (placerShape)
			{
				case PlacerShape.Sphere:
					propPlacerDiameter.floatValue += scrollDir;
					propPlacerDiameter.floatValue = Mathf.Max(propPlacerDiameter.floatValue, 0.1f);
					break;
				case PlacerShape.Cuboid:
					propPlacerSize.vector3Value += Vector3.one * scrollDir;
					Vector3 placerPoint = propPlacerSize.vector3Value;
					placerPoint.x = Mathf.Max(placerPoint.x, 0.1f);
					placerPoint.y = Mathf.Max(placerPoint.y, 0.1f);
					placerPoint.z = Mathf.Max(placerPoint.z, 0.1f);
					propPlacerSize.vector3Value = placerPoint;
					break;
			}

			so.ApplyModifiedProperties();

			Repaint();
			SceneView.RepaintAll();

			Event.current.Use();
		}



		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			DrawPlacer(hit.point, hit.normal);
		}

		for (int i = 0; i < randPoints.Length; i++)
		{
			DrawPlacedPoint(randPoints[i]);
		}
	}

	void DrawPlacer(Vector3 center, Vector3 normal)
	{
		Color colorCache = Handles.color;
		Handles.color = Color.cyan;
		placerPoint = center;

		switch (placerShape)
		{
			case PlacerShape.Sphere:
				Handles.SphereHandleCap(0, placerPoint, Quaternion.identity, placerDiameter, EventType.Repaint);
				//Handles.DrawAAPolyLine (5, center, center + normal);
				break;
			case PlacerShape.Cuboid:
				Handles.DrawWireCube(placerPoint, placerSize);
				break;
		}

		Handles.color = colorCache;
	}

	void DrawPlacedPoint(Vector3 center)
	{
		Handles.SphereHandleCap(0, center, Quaternion.identity, 0.5f, EventType.Repaint);
	}

	void PlaceRandomPoints()
	{
		float volume = 0;

		switch (placerShape)
		{
			case PlacerShape.Sphere:
				volume = Mathf.PI * 1.33333f * Mathf.Pow(placerDiameter * 0.5f, 3);
				break;
			case PlacerShape.Cuboid:
				volume = placerSize.x * placerSize.y * placerSize.z;
				break;
		}

		int pointCount = Mathf.RoundToInt(volume * spawnDensity);

		randPoints = new Vector3[pointCount];

		for (int i = 0; i < pointCount; i++)
		{
			switch (placerShape)
			{
				case PlacerShape.Sphere:
					randPoints[i] = placerPoint + Random.insideUnitSphere * placerDiameter * 0.5f;
					break;
				case PlacerShape.Cuboid:
					randPoints[i] = placerPoint + new Vector3(
						(Random.value - 0.5f) * placerSize.x,
						(Random.value - 0.5f) * placerSize.y,
						(Random.value - 0.5f) * placerSize.z);
					break;
			}
		}
	}
}

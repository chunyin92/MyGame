using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Water))]
public class WaterEditor : Editor
{
    SerializedProperty Type;
    SerializedProperty DisablePixelLights;
    SerializedProperty TextureSize;
    SerializedProperty ReflectClipPlaneOffset;
    SerializedProperty ReflectLayers;
    SerializedProperty RefractLayers;
    SerializedProperty RefractionAngle;

    GUIContent[] _renderTextureOptions = new GUIContent[8] {new GUIContent("16"), new GUIContent("32"), new GUIContent("64"), new GUIContent("128"),
                                         new GUIContent("256"), new GUIContent("512"), new GUIContent("1024"), new GUIContent("2048") };
    int[] _renderTextureSize = new int[8] { 16, 32, 64, 128, 256, 512, 1024, 2048 };

    void OnEnable()
    {
        Type = serializedObject.FindProperty("Type");
        DisablePixelLights = serializedObject.FindProperty("DisablePixelLights");
        TextureSize = serializedObject.FindProperty("TextureSize");
        ReflectClipPlaneOffset = serializedObject.FindProperty("ReflectClipPlaneOffset");
        RefractionAngle = serializedObject.FindProperty("RefractionAngle");
        ReflectLayers = serializedObject.FindProperty("ReflectLayers");
        RefractLayers = serializedObject.FindProperty("RefractLayers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(Type);
        EditorGUILayout.HelpBox("Use Both, Reflection or Refraction do not work", MessageType.Info);
        EditorGUILayout.PropertyField(DisablePixelLights);
        EditorGUILayout.IntPopup(TextureSize, _renderTextureOptions, _renderTextureSize, new GUIContent("Texture Size"));
        EditorGUILayout.Slider(ReflectClipPlaneOffset, 0, 0.1f, new GUIContent("ClipPlane Offset"));
        EditorGUILayout.Slider(RefractionAngle, 0, 1, new GUIContent("Refraction Angle"));
        EditorGUILayout.PropertyField(ReflectLayers);
        EditorGUILayout.PropertyField(RefractLayers);

        serializedObject.ApplyModifiedProperties();
    }
}
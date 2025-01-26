using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Texture2D))]
public class Texture2DDrawer : PropertyDrawer
{

    private static GUIStyle s_TempStyle = new GUIStyle();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.isEditingMultipleObjects)
        {
            GUI.Label(position, "Texture2D multiediting not supported");
            return;
        }

        var ident = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect spriteRect;

        //create object field for the sprite
        spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.name, property.objectReferenceValue, typeof(Texture2D), false);

        //if this is not a repain or the property is null exit now
        if (Event.current.type != EventType.Repaint || property.objectReferenceValue == null)
            return;

        //draw a sprite
        Texture2D sp = property.objectReferenceValue as Texture2D;

        spriteRect.y += EditorGUIUtility.singleLineHeight + 4;
        spriteRect.width = 167;
        spriteRect.height = 256;
        s_TempStyle.normal.background = sp;
        s_TempStyle.Draw(spriteRect, GUIContent.none, false, false, false, false);

        EditorGUI.indentLevel = ident;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 70f;
    }
}


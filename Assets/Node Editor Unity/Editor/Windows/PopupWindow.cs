using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PopupWindow : EditorWindow {
  static PopupWindow currentPopUp;
  string wantedName = "Enter a name";

  public static void InitNodePopUp () {
    currentPopUp = (PopupWindow) EditorWindow.GetWindow<PopupWindow> ();
    currentPopUp.titleContent = new GUIContent ("Node PopUp");
  }
  private void OnGUI () {
    GUILayout.Space (20);
    GUILayout.BeginHorizontal ();
    GUILayout.Space (20);
    GUILayout.BeginVertical ();
    EditorGUILayout.LabelField ("Create New Graph", EditorStyles.boldLabel);
    wantedName = EditorGUILayout.TextField ("Enter name:", wantedName);
    GUILayout.Space (10);
    GUILayout.BeginHorizontal ();
    if (GUILayout.Button ("Create Graph", GUILayout.Height (40))) {
      if (!string.IsNullOrEmpty (wantedName) && !wantedName.Equals ("Enter a name")) {
        NodeUtils.CreateNewNodeGraph (wantedName);
        currentPopUp.Close ();
      }
      else {
        EditorUtility.DisplayDialog ("Node Message:", "The name cannot be null or Empty. Please enter an valid name!", "OK");
      }
    }
    if (GUILayout.Button ("Cancel", GUILayout.Height (40))) {
      currentPopUp.Close ();
    }
    GUILayout.EndHorizontal ();
    GUILayout.EndVertical ();
    GUILayout.Space (20);
    GUILayout.EndHorizontal ();
    GUILayout.Space (20);

  }
}

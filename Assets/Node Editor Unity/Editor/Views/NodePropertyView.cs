using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodePropertyView : ViewBase {

  public bool showProperties = false;

  public NodePropertyView () : base ("Property View") {}

  public override void UpdateView (Event e, Rect _editorRect, Rect _percentageRect, NodeGraph _graph) {
    base.UpdateView (e, _editorRect, _percentageRect, _graph);
    GUI.Box (viewRect, viewTitle, this.viewSkin.GetStyle ("ViewBG"));
    GUILayout.BeginArea (viewRect);
    // currentGraph.selected.DrawNodeProperties ();
    GUILayout.Space (60);
    GUILayout.BeginHorizontal ();
    GUILayout.Space (30);
    if (!currentGraph.showProperties) {
      EditorGUILayout.LabelField ("No Node Selected");
    }
    else {
      currentGraph.selected.DrawNodeProperties ();
    }
    GUILayout.Space (30);
    GUILayout.EndHorizontal ();
    GUILayout.EndArea ();
    ProcessEvents (e);
    // if (e.type == EventType.Layout) {
    //   if (currentGraph != null) {
    //     if (currentGraph.selected != null) {
    //       showProperties = true;
    //     }
    //     else {
    //       showProperties = false;
    //     }
    //   }
    //   else {
    //     showProperties = false;
    //   }
    // }
  }
  public override void ProcessEvents (Event e) {
    base.ProcessEvents (e);
    if (viewRect.Contains (e.mousePosition)) {
      //TODO colocar uma skin de highlight sobre a property view
    }
  }
}

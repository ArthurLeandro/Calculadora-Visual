using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodeWorkView : ViewBase {

  private Vector2 mousePosition;
  int deleteNodeID = 0;
  public NodeWorkView () : base ("Work View") {}

  public override void UpdateView (Event e, Rect _editorRect, Rect _percentageRect, NodeGraph _graph) {
    base.UpdateView (e, _editorRect, _percentageRect, _graph);
    GUI.Box (viewRect, viewTitle, this.viewSkin.GetStyle ("ViewBG"));
    NodeUtils.DrawGrid (viewRect, 50f, 0.25f, Color.white);
    GUILayout.BeginArea (viewRect);
    if (currentGraph != null) {
      currentGraph.UpdateGraphGUI (e, viewRect, this.viewSkin);
    }
    GUILayout.EndArea ();
    ProcessEvents (e);
  }
  public override void ProcessEvents (Event e) {
    base.ProcessEvents (e);
    if (viewRect.Contains (e.mousePosition)) {
      if (e.button == 0) {
        if (e.type == EventType.MouseDown) {}
        if (e.type == EventType.MouseDrag) {}
        if (e.type == EventType.MouseUp) {}
      }
      if (e.button == 1) {
        if (e.type == EventType.MouseDown) {
          mousePosition = e.mousePosition;
          bool overNode = false;
          deleteNodeID = 0;
          if (currentGraph != null) {
            if (currentGraph.nodes.Count > 0) {
              for (int i = 0; i < currentGraph.nodes.Count; i++) {
                if (currentGraph.nodes [i].nodeRect.Contains (e.mousePosition)) {
                  overNode = true;
                  deleteNodeID = i;
                }
              }
            }
          }
          int valueOfId = 0;
          if (overNode) {
            valueOfId = 1;
          }
          ProcessContentMenu (e, valueOfId);
        }
      }
    }
  }

  private void ProcessContentMenu (Event e, int contextID) {
    GenericMenu menu = new GenericMenu ();
    if (contextID == 0) {
      menu.AddItem (new GUIContent ("Create Graph"), false, ContextCallback, "0");
      menu.AddItem (new GUIContent ("Load Graph"), false, ContextCallback, "1");
      if (currentGraph != null) {
        // If we're working with an existing graph
        menu.AddItem (new GUIContent ("Unload Graph"), false, ContextCallback, "2");
        menu.AddSeparator ("");
        menu.AddItem (new GUIContent ("Float Node"), false, ContextCallback, "3");
        menu.AddItem (new GUIContent ("Add Node"), false, ContextCallback, "4");
      }
    }
    if (contextID == 1) {
      if (currentGraph != null) {
        menu.AddSeparator ("");
        menu.AddItem (new GUIContent ("Delete Node"), false, ContextCallback, "5");
      }
    }
    menu.ShowAsContext ();
    e.Use ();
  }
  void ContextCallback (object obj) {
    switch (obj.ToString ()) {
    case "0":
      PopupWindow.InitNodePopUp ();
      break;
    case "1":
      NodeUtils.LoadGraph ();
      break;
    case "2":
      NodeUtils.UnloadGraph ();
      break;
    case "3":
      NodeUtils.CreateNode (currentGraph, NodeType.FLOAT, mousePosition);
      break;
    case "4":
      NodeUtils.CreateNode (currentGraph, NodeType.ADD, mousePosition);
      break;
    case "5":
      NodeUtils.DeleteNode (deleteNodeID, currentGraph);
      break;
    default:
      break;

    }
  }
}

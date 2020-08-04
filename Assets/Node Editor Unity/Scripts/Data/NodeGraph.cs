using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
public class NodeGraph : ScriptableObject {
  public bool showProperties = false;
  public string graphName = "";
  public List<NodeBase> nodes;
  public NodeBase selected;
  public bool wantsConnection = false;
  public NodeBase connectionNode;
  private void OnEnable () {
    if (nodes == null) {
      nodes = new List<NodeBase> ();
    }
  }
  public void InitGraph () {
    int nodeAmount = nodes.Count;
    if (nodeAmount > 0) {
      for (int i = 0; i < nodeAmount; i++) {
        nodes [i].InitNode ();
      }
    }
  }
  public void UpdateGraphGUI (Event e, Rect viewRect, GUISkin _viewSkin) {
    if (nodes.Count > 0) {
      ProcessEvents (e, viewRect);
      foreach (NodeBase node in nodes) {
        node.UpdateNodeGUI (e, viewRect, _viewSkin);
      }
    }
    if (wantsConnection) {
      if (connectionNode != null) {
        DrawConnectionToMouse (e.mousePosition);
      }
    }

    if (e.type == EventType.Layout) {
      if (selected != null) {
        showProperties = true;
      }
    }
    EditorUtility.SetDirty (this);
  }
  public void UpdateGraph (Event e, Rect viewRect) {
    if (nodes.Count > 0) {}
  }
  public void ProcessEvents (Event e, Rect viewRect) {
    if (viewRect.Contains (e.mousePosition)) {
      if (e.button == 0) {
        if (e.type == EventType.MouseDown) {
          DeselectAllNodes ();
          showProperties = false;
          bool setNode = false;
          selected = null;
          for (int i = 0; i < nodes.Count; i++) {
            if (nodes [i].nodeRect.Contains (e.mousePosition)) {
              nodes [i].isSelected = true;
              setNode = true;
              selected = nodes [i];
            }
          }
          if (!setNode) {
            DeselectAllNodes ();
            // wantsConnection = false;
            // connectionNode = null;
          }
          if (wantsConnection) {
            wantsConnection = false;
          }
        }
      }
    }
  }
  public void DeselectAllNodes () {
    foreach (NodeBase node in nodes) {
      node.isSelected = false;
    }
  }
  void DrawConnectionToMouse (Vector2 mousePosition) {
    Handles.BeginGUI ();
    Handles.color = Color.cyan;
    Handles.DrawLine (
      new Vector3 (connectionNode.nodeRect.x + (connectionNode.nodeRect.width + 24f),
        connectionNode.nodeRect.y + (connectionNode.nodeRect.height * 0.5f), 0f),
      new Vector3 (mousePosition.x, mousePosition.y, 0f)
    );
    Handles.EndGUI ();
  }
}

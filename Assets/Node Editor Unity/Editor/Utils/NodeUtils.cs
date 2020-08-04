using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public static class NodeUtils {
	public static void CreateNewNodeGraph (string _name) {
		NodeGraph graph = (NodeGraph) ScriptableObject.CreateInstance<NodeGraph> ();
		if (graph != null) {
			graph.graphName = _name;
			graph.InitGraph ();
			AssetDatabase.CreateAsset (graph, "Assets/Node Editor Unity/Database/" + _name + ".asset");
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
			NodeEditorWindow curWindow = (NodeEditorWindow) EditorWindow.GetWindow<NodeEditorWindow> ();
			if (curWindow != null) {
				curWindow.currentGraph = graph;
			}
		}
		else {
			EditorUtility.DisplayDialog ("Node Message:", "Unable to create new graph!", "OK");
		}
	}
	public static void UnloadGraph () {
		NodeEditorWindow curWindow = (NodeEditorWindow) EditorWindow.GetWindow<NodeEditorWindow> ();
		if (curWindow != null) {
			curWindow.currentGraph = null;
		}
	}
	public static void LoadGraph () {
		NodeGraph currentGraph = null;
		string graphPath = EditorUtility.OpenFilePanel ("Load Graph", Application.dataPath + "Node Editor Unity/Database/", "");
		int appPathLen = Application.dataPath.Length;
		string finalPath = graphPath.Substring (appPathLen - 6);
		currentGraph = (NodeGraph) AssetDatabase.LoadAssetAtPath (finalPath, typeof (NodeGraph));
		if (currentGraph != null) {
			NodeEditorWindow curWindow = (NodeEditorWindow) EditorWindow.GetWindow<NodeEditorWindow> ();
			if (curWindow != null) {
				curWindow.currentGraph = currentGraph;
			}
		}
		else {
			EditorUtility.DisplayDialog ("Node Message:", "Unable to load selected graph", "OK");
		}
	}

	public static void CreateNode (NodeGraph _currentGraph, NodeType _type, Vector2 _position) {
		NodeBase currNode = null;
		if (_currentGraph != null) {
			switch (_type) {
			case NodeType.FLOAT:
				currNode = (FloatNode) ScriptableObject.CreateInstance<FloatNode> ();
				currNode.name = "Float Node";
				currNode.nodeName = "Float Node";
				break;
			case NodeType.ADD:
				currNode = (AddNode) ScriptableObject.CreateInstance<AddNode> ();
				currNode.name = "Add Node";
				currNode.nodeName = "Add Node";
				break;
			default:
				break;
			}
			if (currNode != null) {
				currNode.InitNode ();
				currNode.nodeRect.x = _position.x;
				currNode.nodeRect.y = _position.y;
				currNode.parentGraph = _currentGraph;
				_currentGraph.nodes.Add (currNode);
				AssetDatabase.AddObjectToAsset (currNode, _currentGraph);
				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();
			}
		}
	}
	public static void DeleteNode (int _nodeID, NodeGraph _graph) {
		if (_graph != null) {
			if (_graph.nodes.Count >= _nodeID) {
				NodeBase deleteNode = _graph.nodes [_nodeID];
				if (deleteNode != null) {
					_graph.nodes.RemoveAt (_nodeID);
					GameObject.DestroyImmediate (deleteNode, true);
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
				}
			}
		}
	}

	public static void DrawGrid (Rect viewRect, float gridSpace, float gridOpacity, Color gridColor) {
		int widthDivs = Mathf.CeilToInt (viewRect.width / gridSpace);
		int heigthDivs = Mathf.CeilToInt (viewRect.width / gridSpace);
		Handles.BeginGUI ();
		Handles.color = new Color (gridColor.r, gridColor.g, gridColor.b, gridOpacity);
		for (int i = 0; i < widthDivs; i++) {
			Handles.DrawLine (new Vector3 (gridSpace * i, 0f, 0f),
				new Vector3 (gridSpace * i, viewRect.height, 0f));
		}
		for (int i = 0; i < heigthDivs; i++) {
			Handles.DrawLine (new Vector3 (0f, gridSpace * i, 0f),
				new Vector3 (viewRect.width, gridSpace * i, 0f));
		}
		Handles.color = Color.white;
		Handles.EndGUI ();
	}

}

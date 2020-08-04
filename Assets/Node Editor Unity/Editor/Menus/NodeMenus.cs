using UnityEditor;
using UnityEngine;

public class NodeMenus {

	[MenuItem ("Node Editor/Launch Editor")]
	public static void InitNodeEditor () {
		NodeEditorWindow.InitEditorWindow ();
	}
}

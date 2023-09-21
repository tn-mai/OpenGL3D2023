#define UNITY_EDITOR
#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Collections.Specialized;

/**
* GameObjectの名前、トランスフォーム等をJSONファイルに出力するエディタ拡張
*
* プロジェクトのEditorフォルダ(無ければ適当な場所に作成)に配置して使う
*/
class ExportGameObjectToJson : EditorWindow
{
    bool flipYZAxis = false; // XZ平面のマップを-90度回転して、XY平面として出力する
    bool forceExportHiddens = false;

    [MenuItem("File/Export/GameObject To Json")]

    static void Init()
    {
        var window = EditorWindow.GetWindow<ExportGameObjectToJson>("Export GameObject To Json");
        window.Show();
    }

    void OnGUI()
    {
        forceExportHiddens = GUILayout.Toggle(forceExportHiddens, "Force Export Hiddens");
        flipYZAxis = GUILayout.Toggle(flipYZAxis, "Flip YZ Axis");
        if (GUILayout.Button("Export")) {
            Export(flipYZAxis, forceExportHiddens);
            Close();
        }
    }

    private static void Export(bool flipYZAxis, bool forceExportHiddens)
    {
        var path = EditorUtility.SaveFilePanel(
            "Export GameObject To Json",
            "",
            "GameObjectMap.json",
            "json");
        if (path.Length == 0) {
            return;
        }

        try {
            var sb = new StringBuilder(10000);
            sb.Append("[");

            // すべてのゲームオブジェクトを列挙する
            var gameObjects = new System.Collections.Generic.List<GameObject>();
            foreach(GameObject go in SceneManager.GetActiveScene().GetRootGameObjects()) {
              foreach(UnityEngine.Component e in go.GetComponentsInChildren<Transform>()) {
                  gameObjects.Add(e.gameObject);
              }
            }

            int count = 0;
            SceneVisibilityManager svm = SceneVisibilityManager.instance;
            foreach (GameObject go in gameObjects) {
                // ヒエラルキーで非表示設定になっているオブジェクトは無視する
                if (!forceExportHiddens && svm.IsHidden(go, false)) {
                    continue;
                }

                // TODO: アセットパスは独立した項目として出力してもよいかもしれない
                string meshName = null;
                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(go);
                if (prefab != null) {
                    meshName = prefab.name;//AssetDatabase.GetAssetPath(prefab);
                } else {
                    MeshFilter m = go.GetComponent<MeshFilter>();
                    if (m != null && m.sharedMesh != null) {
                        meshName = m.sharedMesh.name;
                        //meshName = AssetDatabase.GetAssetPath(m.sharedMesh);
                        //if (meshName == null || meshName.Equals(System.String.Empty)) {
                        //    meshName = m.sharedMesh.name;
                        //}
                    }
                }
                var t = go.transform.position;
                var r = go.transform.eulerAngles;
                var s = go.transform.localScale;
                if (flipYZAxis) {
                    (t.y, t.z) = (t.z, t.y);
                    var q = Quaternion.AngleAxis(-90, Vector3.right) * go.transform.rotation;
                    r = q.eulerAngles;
                }
                sb.AppendLine();
                sb.Append("  ");
                sb.Append("{ \"name\" : \"" + go.name + "\"");
                if (go.transform.parent) {
                    sb.Append(", \"parent\" : \"" + go.transform.parent.gameObject.name + "\"");
                }
                if (meshName != null && !meshName.Equals(System.String.Empty)) {
                    sb.Append(", \"mesh\" : \"" + meshName + "\"");
                }
                sb.Append(", \"translate\" : [ " + t.x + ", " + t.y + ", " + t.z + " ]");
                sb.Append(", \"rotate\" : [ " + r.x + ", " + r.y + ", " + r.z + " ]");
                sb.Append(", \"scale\" : [ " + s.x + ", " + s.y + ", " + s.z + " ]");
                if (go.tag != "Untagged") {
                    sb.Append(", \"tag\" : \"" + go.tag + "\"");
                }
                List<BoxCollider> boxes = new List<BoxCollider>(go.GetComponents<BoxCollider>());
                List<SphereCollider> spheres = new List<SphereCollider>(go.GetComponents<SphereCollider>());
                List<CapsuleCollider> capsules = new List<CapsuleCollider>(go.GetComponents<CapsuleCollider>());
                if (prefab) {
                  boxes.AddRange(prefab.GetComponents<BoxCollider>());
                  spheres.AddRange(prefab.GetComponents<SphereCollider>());
                  capsules.AddRange(prefab.GetComponents<CapsuleCollider>());
                }
                if (boxes.Count > 0) {
                    sb.Append(", \"BoxCollider\" : [");
                    foreach (BoxCollider box in boxes) {
                        sb.Append(" { \"center\" : [ " + box.center.x + ", " + box.center.y + ", " + box.center.z + " ]");
                        sb.Append(", \"size\" : [ " + box.size.x + ", " + box.size.y + ", " + box.size.z + " ]");
                        sb.Append(" },");
                    }
                    sb.Length--;
                    sb.Append(" ]");
                }
                if (spheres.Count > 0) {
                    sb.Append(", \"SphereCollider\" : [");
                    foreach (SphereCollider sphere in spheres) {
                        sb.Append(" { \"center\" : [ " + sphere.center.x + ", " + sphere.center.y + ", " + sphere.center.z + " ]");
                        sb.Append(", \"radius\" : " + sphere.radius);
                        sb.Append(" },");
                    }
                    sb.Length--;
                    sb.Append(" ]");
                }
                if (capsules.Count > 0) {
                    sb.Append(", \"CapsuleCollider\" : [");
                    foreach (CapsuleCollider capsule in capsules) {
                        sb.Append(" { \"center\" : [ " + capsule.center.x + ", " + capsule.center.y + ", " + capsule.center.z + " ]");
                        sb.Append(", \"direction\" : " + capsule.direction);
                        sb.Append(", \"height\" : " + capsule.height);
                        sb.Append(", \"radius\" : " + capsule.radius);
                        sb.Append(" },");
                    }
                    sb.Length--;
                    sb.Append(" ]");
                }
                sb.Append(" },");
                ++count;
            }
            // JSON準拠のため、末尾のカンマを消す
            if (count > 0) {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.AppendLine();
            sb.AppendLine("]");

            System.IO.StreamWriter saveFile = new System.IO.StreamWriter(path, false);
            saveFile.Write(sb);
            saveFile.Flush();
            saveFile.Close();
            Debug.Log("Export " + count + " GameObject To " + path);
        }
        catch (System.Exception ex) {
            Debug.Log(ex.Message);
        }
    }

    private static Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static float Max(Vector3 a)
    {
        float max = a.x;
        if (max < a.y) {
            max = a.y;
        }
        if (max < a.z) {
            max = a.z;
        }
        return max;
    }
}
#endif

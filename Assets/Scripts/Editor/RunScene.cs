using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class PlayFromFirstScene
{
    private const string MenuPath = "Tools/Play From First Scene %#p"; // Có thể đổi phím tắt ở đây
    private const string PrefKey = "PlayFromFirstScene_Enabled";

    // Tự động khôi phục trạng thái mỗi khi Unity compile lại code
    static PlayFromFirstScene()
    {
        EditorApplication.delayCall += UpdateStartScene;
    }

    // Phím tắt F5 để chạy ngay từ Scene đầu tiên (dù đang ở scene nào)
    [MenuItem("Tools/Play First Scene Now _F5")]
    public static void PlayFirstSceneNow()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogError("Chưa có Scene nào trong Build Settings (File -> Build Settings)!");
            return;
        }

        // Hỏi lưu Scene hiện tại nếu có chỉnh sửa chưa lưu
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            return;
        }

        // Gán Scene đầu tiên làm PlayMode Start Scene và chạy game
        string firstScenePath = EditorBuildSettings.scenes[0].path;
        SceneAsset firstScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(firstScenePath);
        
        if (firstScene != null)
        {
            EditorSceneManager.playModeStartScene = firstScene;
            EditorApplication.isPlaying = true;
        }
    }

    // Bật/Tắt chế độ: Bấm nút Play mặc định của Unity cũng luôn chạy từ Scene 0
    [MenuItem("Tools/Always Play From First Scene")]
    private static void ToggleAlwaysPlayFromFirstScene()
    {
        bool current = EditorPrefs.GetBool(PrefKey, false);
        EditorPrefs.SetBool(PrefKey, !current);
        UpdateStartScene();
    }

    [MenuItem("Tools/Always Play From First Scene", true)]
    private static bool ToggleAlwaysPlayFromFirstSceneValidate()
    {
        Menu.SetChecked("Tools/Always Play From First Scene", EditorPrefs.GetBool(PrefKey, false));
        return true;
    }

    private static void UpdateStartScene()
    {
        bool enabled = EditorPrefs.GetBool(PrefKey, false);
        if (enabled && EditorBuildSettings.scenes.Length > 0)
        {
            string firstScenePath = EditorBuildSettings.scenes[0].path;
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(firstScenePath);
        }
        else
        {
            EditorSceneManager.playModeStartScene = null;
        }
    }
}

using UnityEditor;

class Build {
    static void PerformBuild() {
        string[] scenes = { "Assets/TestScene.unity" };
        BuildPipeline.BuildPlayer(scenes, "Build/android/disparity.apk", BuildTarget.Android, BuildOptions.None);
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Digger
{
    // ensure class initializer is called whenever scripts recompile
    [InitializeOnLoad]
    public class PlayModeStateChanged
    {
        // register an event handler when the class is initialized
        static PlayModeStateChanged()
        {
            SceneManager.sceneLoaded += SceneLoaded;
            EditorApplication.playModeStateChanged += LogPlayModeState;
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            switch (state) {
                case PlayModeStateChange.EnteredEditMode:
                {
                    //Debug.Log("LogPlayModeState: EnteredEditMode");
                    if (SceneManager.GetActiveScene().IsValid()) {
                        DiggerMasterEditor.LoadAllChunks(SceneManager.GetActiveScene());
                    }

                    break;
                }
                case PlayModeStateChange.ExitingEditMode:
                {
                    //Debug.Log("LogPlayModeState: ExitingEditMode");
                    for (var i = 0; i < SceneManager.sceneCount; ++i) {
                        var scene = SceneManager.GetSceneAt(i);
                        if (scene.IsValid()) {
                            DiggerMasterEditor.OnEnterPlayMode(scene);
                        }
                    }

                    break;
                }
            }
        }

        private static void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (Application.isPlaying) {
                DiggerMasterEditor.OnEnterPlayMode(scene);
            }
        }
    }
}
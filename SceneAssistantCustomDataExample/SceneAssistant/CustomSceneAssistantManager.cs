using Naninovel;
using NaninovelSceneAssistant;
using UnityEngine;

[InitializeAtRuntime(@override: typeof(SceneAssistantManager))]
public class CustomSceneAssistantManager : SceneAssistantManager
{
    public CustomSceneAssistantManager() { }

    protected override void ResetCamera()
    {
        var camera = new CustomCameraData();
        ObjectList.Add(camera.Id, camera);
    }
}
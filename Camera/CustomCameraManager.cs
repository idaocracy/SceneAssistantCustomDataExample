using Naninovel;
using System;
using UnityEngine;

[InitializeAtRuntime(@override: typeof(CameraManager))]
public class CustomCameraManager : CameraManager, ICustomCameraManager
{
    public CustomCameraManager(CameraConfiguration config, IInputManager inputManager, IEngineBehaviour engineBehaviour) : base(config, inputManager, engineBehaviour)
    { }


    [Serializable]
    public class CustomCameraGameState
    {
        public float FieldOfView;
    }
    public virtual float FieldOfView { get => fieldOfView; set => SetFieldOfView(value); }

    private float fieldOfView;
    private readonly Tweener<FloatTween> fovTweener = new Tweener<FloatTween>();

    public override void ResetService()
    {
        base.ResetService();
        FieldOfView = Camera.orthographicSize;
    }

    public override void SaveServiceState(GameStateMap stateMap)
    {
        var gameState = new CustomCameraGameState
        {
            FieldOfView = FieldOfView
        };
        stateMap.SetState(gameState);
        base.SaveServiceState(stateMap);
    }

    public override UniTask LoadServiceStateAsync(GameStateMap stateMap)
    {
        var state = stateMap.GetState<CustomCameraGameState>();
        if (state is null)
        {
            return base.LoadServiceStateAsync(stateMap);
        }

        FieldOfView = state.FieldOfView;
        return base.LoadServiceStateAsync(stateMap);
    }

    protected virtual void SetFieldOfView(float value)
    {
        CompleteFoVTween();
        fieldOfView = value;
        ApplyFieldOfView(value);
    }
    
    private void CompleteFoVTween()
    {
        if (fovTweener.Running) fovTweener.CompleteInstantly();
    }

    protected virtual void ApplyFieldOfView(float fov)
    {
        if(Orthographic) Camera.orthographicSize = fov;
        else Camera.fieldOfView = fov;
    }

    public virtual async UniTask ChangeFoVAsync(float fov, float duration, EasingType easingType = default, AsyncToken asyncToken = default)
    {
        CompleteFoVTween();

        if (duration > 0)
        {
            var currentFov = this.fieldOfView;
            this.fieldOfView = fov;
            var tween = new FloatTween(currentFov, fov, duration, ApplyFieldOfView, false, easingType);
            await fovTweener.RunAsync(tween, asyncToken, Camera);
        }
        else FieldOfView = fov;
    }
}

public interface ICustomCameraManager : ICameraManager
{
    float FieldOfView { get; set; }
}
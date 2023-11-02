using Naninovel.Commands;
using Naninovel;
using System.Collections.Generic;
using System;

[CommandAlias("camera")]
public class CustomModifyCamera : ModifyCamera
{
    public DecimalParameter Fov;
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        if (Assigned(Zoom)) await base.ExecuteAsync(asyncToken);

        if (Assigned(Fov))
        {
            var tasks = new List<UniTask>();

            var cameraManager = Engine.GetService<CustomCameraManager>();
            var easingType = cameraManager.Configuration.DefaultEasing;
            if (Assigned(EasingTypeName)) Enum.TryParse(EasingTypeName, true, out easingType);

            tasks.Add(base.ExecuteAsync(asyncToken));
            tasks.Add(cameraManager.ChangeFoVAsync(Fov, Duration, easingType, asyncToken));

            await UniTask.WhenAll(tasks);
        }

        await base.ExecuteAsync(asyncToken);
    }
}
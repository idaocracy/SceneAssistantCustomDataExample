using UnityEngine;
using NaninovelSceneAssistant;

public class CustomCameraData : CameraData
{
    // TypeId is for naming the toggleable type option below the Copy Selected button. If not specified, it will be named CustomCameraData.  
    public new static string TypeId => "Camera";
    
    private const string Fov = "Fov";

    protected override void AddCommandParameters()
    {
        // Normally you can call base.AddCommandParameters() with the CommandParameters populated but since I'm organising them in a specific manner, I build the list from scratch. 

        ICommandParameterData rotationData = null;
        ICommandParameterData rollData = null;
        ICommandParameterData zoomData = null;
        ICommandParameterData fovData = null;

        CommandParameters.Add(new CommandParameterData<Vector3>(Offset, () => CameraManager.Offset, v => CameraManager.Offset = v, (i, p) => i.Vector3Field(p)));
        CommandParameters.Add(rotationData = new CommandParameterData<Vector3>(Rotation, () => CameraManager.Rotation.eulerAngles, v => CameraManager.Rotation = Quaternion.Euler(v), (i, p) => i.Vector3Field(p, toggleGroup: new ToggleGroupData(rollData))));
        CommandParameters.Add(rollData = new CommandParameterData<float>(Roll, () => CameraManager.Rotation.eulerAngles.z, v => CameraManager.Rotation = Quaternion.Euler(CameraManager.Rotation.eulerAngles.x, CameraManager.Rotation.eulerAngles.y, v), (i, p) => i.FloatField(p, toggleGroup: new ToggleGroupData(rotationData))));
        CommandParameters.Add(zoomData = new CommandParameterData<float>(Zoom, () => CameraManager.Zoom, v => CameraManager.Zoom = (float)v, (i, p) => i.FloatSliderField(p, 0f, 1f, toggleGroup:new ToggleGroupData(fovData)), defaultValue: 0f));
        CommandParameters.Add(new CommandParameterData<bool>(Orthographic, () => CameraManager.Orthographic, v => CameraManager.Orthographic = (bool)v, (i, p) => i.BoolField(p), defaultValue: true));
        
        if(Service is ICustomCameraManager customCameraManager)
        {
            //The parameters for CommandParameterData are as follows:
            // 1. Name of the parameter. Should be equal to the parameter specified in the command, the first letter will be set to lower case on command generation.
            // 2. The value getter. 
            // 3. The value setter. Since customCameraManager.FieldOfView executes SetFieldOfView on value set, the value will be reflected immediately in game.
            // 4. Select the desired ISceneAssistantLayout field. The FloatField variant has the following parameter options:
            //      a. ICommandParameterData, eg this parameter data.
            //      b. Min and max values. Optional in most cases, sliders require them. 
            //      c. ToggleGroupData functions similarly to the Unity UI component with the same name. With this you can toggle off another data that would
            //      normally conflict with this data when enabled. There's an optional resetOnToggle parameter for determining whether to reset the data on toggle when switching
            //      from another toggle group data.   
            // 5. Default value. Optional parameter that will grab the type default if left unspecified.
            // 6. Conditions. Optional parameter that can be used to conditionally display this data field. For example, if you want to only display this parameter
            // when the camera's Projection property is set to Perspective, you can add conditions:() => !customCameraManager.Orthographic. You can find working conditions in my PostProcessing extension.

            fovData = new CommandParameterData<float>(
                Fov,
                () => customCameraManager.FieldOfView,
                v => customCameraManager.FieldOfView = (float)v,
                (i, p) => i.FloatField(p, min: 0f, toggleGroup: new ToggleGroupData(zoomData)),
                defaultValue: customCameraManager.Orthographic ? customCameraManager.Camera.orthographicSize : customCameraManager.Camera.fieldOfView
                );

            CommandParameters.Add(fovData);
        }

        AddCameraComponentParams();
    }


}

# SceneAssistantCustomDataExample
An example on how to override Scene Assistant with custom data.

<img width="340" alt="image" src="https://github.com/idaocracy/SceneAssistantCustomDataExample/assets/77254066/7ad5e571-7bf2-4903-b1e7-5403d43d2928">


This example overrides CameraManager with a **FieldOfView** property that lets you freely adjust the Field of View and Orthographic Size properties of the main Naninovel camera. SceneAssistantManager is then overridden with a custom CameraData that exposes this value. See **CustomSceneAssistantManager.cs** and **CustomCameraData.cs** for more information. 
# Setup
Place the SceneAssistantCustomDataExample anywhere in the project (that is not managed by another plugin, including the Naninovel and NaninovelData folders).

# Sample Script

The following script demonstrates the fov parameter in **@camera**:

```
@char Missingno pos:50,45
The camera is orthographic by default. Specifying a fov value will set the orthographic size of the camera. 
@camera fov:3 time:1
Orthographic size is 3.
After switching to perspective mode, the fov will now increase the field of view.
@camera fov:100 time:3 ortho:false
Field of View is 100.
@stop
```

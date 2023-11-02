# SceneAssistantCustomDataExample
An example on how to override Scene Assistant with custom data.

<img width="340" alt="image" src="https://github.com/idaocracy/SceneAssistantCustomDataExample/assets/77254066/7ad5e571-7bf2-4903-b1e7-5403d43d2928">


This example overrides CameraManager with a **FieldOfView** property that lets you freely adjust the Field of View and Orthographic Size properties of the main Naninovel camera. SceneAssistantManager is then overridden with a custom CameraData that exposes this value. See **CustomSceneAssistantManager.cs** and **CustomCameraData.cs** for more information. 
# Setup
Place the folders anywhere in the project (that is not managed by another plugin, including the Naninovel and NaninovelData folders).

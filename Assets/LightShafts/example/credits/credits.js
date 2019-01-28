#pragma strict

var sliderx:float=0;
var slidery:float=0;
var sliderz:float=0;
var window:Transform;


function OnGUI () {
if(GUILayout.Button("Models created by VYTAUTAS RAMANAUSKAS")) {
Application.OpenURL("http://vytautasramanauskas.wix.com/portfolio");
}
if(GUILayout.Button("Alzheimer LightShafts developer")) {
Application.OpenURL("http://markduisters.blogspot.be/p/what-is-alzheimer-studio.html");
}
if(GUILayout.Button("Buy Alzheimer LightShafts")) {
Application.OpenURL("https://www.assetstore.unity3d.com/#/content/13196");
}
if(GUILayout.Button("Alzheimer LightShafts forum page")) {
Application.OpenURL("http://forum.unity3d.com/threads/214905-Light-shaft-system");
}

GUILayout.Label("Window shaft's x axis rotation speed");
sliderx = GUI.HorizontalSlider (Rect (0, 120, 200, 30), sliderx, -3, 3);

GUILayout.Label("Window shaft's y axis rotation speed");
slidery = GUI.HorizontalSlider (Rect (0, 145, 200, 30), slidery, -3, 3);

GUILayout.Label("Window shaft's z axis rotation speed");
sliderz = GUI.HorizontalSlider (Rect (0, 170, 200, 30), sliderz, -3, 3);

window.GetComponent(LightShaftGenerator).DynRot.z=sliderz;
window.GetComponent(LightShaftGenerator).DynRot.y=slidery;
window.GetComponent(LightShaftGenerator).DynRot.x=sliderx;

}
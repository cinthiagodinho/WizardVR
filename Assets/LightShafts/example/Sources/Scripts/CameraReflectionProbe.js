#pragma strict

var cam:Camera;

function Update () {
transform.position.y= -cam.transform.position.y;
transform.position.x= cam.transform.position.x;
transform.position.z= cam.transform.position.z;
}
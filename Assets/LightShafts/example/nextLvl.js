#pragma strict
var lvl:String;

function OnTriggerEnter (coll:Collider) {
Application.LoadLevel(lvl);
}

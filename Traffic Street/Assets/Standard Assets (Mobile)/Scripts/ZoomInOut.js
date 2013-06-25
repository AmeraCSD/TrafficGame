var zoom : int = 20;
var normal : int = 60;
var smooth : float = 5;
private var isZoomed = false;



function Update () {
     if(Input.GetKeyDown("z")){
          isZoomed = !isZoomed; 
     }
 
     if(isZoomed == true){
          camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,zoom,Time.deltaTime*smooth);
     }
     else{
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,normal,Time.deltaTime*smooth);
     }
}
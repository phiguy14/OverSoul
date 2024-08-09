using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Player;
public class UIManager : MonoBehaviour
{
    UIDocument document;
    VisualTreeAsset hud, menu;
    UIHUD hudManager;
    UIMenu menuManager;
    PlayerManager pm;
    PlayerInput playerInput;

    public void Start()
    {
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        document = this.GetComponent<UIDocument>();
        Addressables.LoadAssetAsync<VisualTreeAsset>("menu").Completed += LoadMenu;
        Addressables.LoadAssetAsync<VisualTreeAsset>("hud").Completed += LoadHUD;

    }
    public void LoadHUD(AsyncOperationHandle<VisualTreeAsset> handle)
    {
        try { hud = handle.Result; }
        catch { }
        document.visualTreeAsset = hud;
        hudManager = new UIHUD(document);
    }
    public void LoadMenu(AsyncOperationHandle<VisualTreeAsset> handle)
    {
        try { menu = handle.Result; }
        catch { }
    }
    public void Close(InputAction.CallbackContext a)
    {
        if (a.performed)
        {

            playerInput.SwitchCurrentActionMap("Player");
            document.visualTreeAsset = hud;
            menuManager.Unregister();
            menuManager = null;
            hudManager = new UIHUD(document);
        };
    }
    public void Open(InputAction.CallbackContext a)
    {
        if (a.performed)
        {

            playerInput.SwitchCurrentActionMap("UI");
            document.visualTreeAsset = menu;
            hudManager.Unregister();
            hudManager = null;
            menuManager = new UIMenu(document, this.gameObject.GetComponent<PlayerManager>());
        };
    }
    /*
    private float currentAngle = 0, desiredAngle= .5f, velocity = 0.0f;
    private bool rotate, reverse = false;
    private void Start(){
        velocity = 0.0f;
    }

    public void Update()
    {
        if (rotate){

            currentAngle = transform.eulerAngles.y;
            
            if (!reverse){
                if (currentAngle <= 270 && currentAngle>=180){
                    RotateAngle(270,true);
                    Screen1();
                }
                else{
                    Stop(true);
                    Screen2();
                }
            }
            else{  
                if (currentAngle >= 359){
                    RotateAngle(360,false);                    
                    Screen2();
                }
                else{
                    Stop(false);
                    Screen1();
                }
            }
        }
    }
    public void Rotate(){ rotate = true; }

    private void Stop(bool negative){
        var target = negative ? -desiredAngle : desiredAngle;
        currentAngle = Mathf.SmoothDampAngle(currentAngle, target, ref velocity, -0.0000001f);
        transform.Rotate(new Vector3(0, currentAngle, 0), Space.Self);        
    }
    private void Screen1(){
       // MenuHandler.inventoryScreen.gameObject.SetActive(true);
       // MenuHandler.equipScreen.gameObject.SetActive(true);
    }
    private void Screen2(){
      //  MenuHandler.abilityScreen.gameObject.SetActive(false);
      //  MenuHandler.tempAbilityScreen.gameObject.SetActive(false);
    }

    private void RotateAngle(int angle, bool rev){
        transform.Rotate(0, -(transform.eulerAngles.y-angle ), 0);
        rotate = false;
        reverse = rev;
       // MenuHandler.inventoryScreen.SetToDefaultSlot();
    }*/
}

using UnityEngine;
using UnityEngine.EventSystems;


public class RemoveEventsOnSelfAndChildrenOnDisable : MonoBehaviour{
    public void OnDisable(){

        foreach(Transform transform in this.gameObject.transform){

            EventTrigger eventTrigger =transform.gameObject.GetComponent<EventTrigger>();

            if(eventTrigger)
                foreach(EventTrigger.Entry entry in eventTrigger.triggers){
                    entry.callback.RemoveAllListeners();
                }                                        
        }
    }
}

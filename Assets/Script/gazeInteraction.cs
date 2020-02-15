using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class gazeInteraction : MonoBehaviour
{
   public float gazeTime = 2f;
   private float timer;
   private bool gazedAt;
    // Update is called once per frame
    void Update()
    {
        if(gazedAt){
            timer += Time.deltaTime;
            if(timer >= gazeTime){
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                timer = 0f;            }
        }
    }

    public void PointerEnter(){
        gazedAt = true;
        Debug.Log("Pointer Enter");
    }

    public void PointerExit(){
        gazedAt = false;
        Debug.Log("Pointer Exit");
    }
}

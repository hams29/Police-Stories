using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            //Ž‹ŠE‚ÌŠp“x“à‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward,posDelta);

            if(target_angle < angle) //target_angle‚ªangle‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Ray‚ðŽg—p‚µ‚Ätarget‚É“–‚½‚Á‚Ä‚¢‚é‚©”»•Ê
                if (Physics.Raycast(pos,posDelta,out RaycastHit hit)) 
                {
                    if(hit.collider == other)
                    {
                        //Core‚ÌŽæ“¾
                        Core otherCore = other.transform.GetComponentInChildren<Core>();
                        if(otherCore != null)
                        {
                            //CoreComponent‚ÌŽæ“¾
                            Show otherShow = null;
                            otherCore.GetCoreComponent(ref otherShow);
                            if(otherShow != null)
                            {
                                otherShow.ShowObject();
                            }
                        }
                    }
                }
            }
        }
    }
}

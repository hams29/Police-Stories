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
            //���E�̊p�x���Ɏ��܂��Ă��邩
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward,posDelta);

            //target_angle��angle�Ɏ��܂��Ă��邩�ǂ���
            if (target_angle < angle) 
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Ray���g�p����target�ɓ������Ă��邩����
                if (Physics.Raycast(pos,posDelta,out RaycastHit hit)) 
                {
                    if(hit.collider == other)
                    {
                        //���E���Ɏ��܂��Ă���ꍇ
                        SetShow(other.gameObject);
                    }
                    else if(hit.collider.gameObject.layer == other.gameObject.layer)
                    {
                        SetShow(other.gameObject);
                    }
                    else
                    {
                        //�^�[�Q�b�g�ƃv���C���[�̊Ԃɕʂ̃I�u�W�F�N�g���������ꍇ
                        SetBlind(other.gameObject);
                    }
                }
            }
            else
            {
                //�p�x���Ɏ��܂��Ă��Ȃ��ꍇ
                SetBlind(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            //���E����^�[�Q�b�g���������ꍇ
            SetBlind(other.gameObject);
        }
    }

    private void SetBlind(GameObject other)
    {
        Core otherCore = other.GetComponentInChildren<Core>();
        if (otherCore != null)
        {
            //CoreComponent�̎擾
            Show otherShow = null;
            otherCore.GetCoreComponent(ref otherShow);
            if (otherShow != null)
            {
                otherShow.BlindObject();
            }
        }
    }

    private void SetShow(GameObject other)
    {
        Core otherCore = other.GetComponentInChildren<Core>();
        if (otherCore != null)
        {
            //CoreComponent�̎擾
            Show otherShow = null;
            otherCore.GetCoreComponent(ref otherShow);
            if (otherShow != null)
            {
                otherShow.ShowObject();
            }
        }
    }
}

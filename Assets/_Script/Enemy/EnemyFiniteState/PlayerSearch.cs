using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    public bool isPlayerFind { get; private set; }
    public Vector3 playerPos { get; private set; }
    public bool isPlayerDead { get; private set; }

    private void Awake()
    {
        isPlayerFind = false;
        isPlayerDead = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //���E�̊p�x���Ɏ��܂��Ă��邩
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            //target_angle��angle�Ɏ��܂��Ă��邩�ǂ���
            if (target_angle < angle)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Ray���g�p����target�ɓ������Ă��邩����
                if (Physics.Raycast(pos, posDelta, out RaycastHit hit))
                {
                    if (hit.collider == other)
                    {
                        //���E���Ɏ��܂��Ă���ꍇ
                        playerPos = other.transform.position;
                        States state = null;
                        other.GetComponentInChildren<Core>().GetCoreComponent(ref state);
                        if (state != null)
                            isPlayerDead = state.dead;
                        isPlayerFind = true;
                    }
                    else if (hit.collider.gameObject.layer == other.gameObject.layer)
                    {
                        playerPos = other.transform.position;
                        States state = null;
                        other.GetComponentInChildren<Core>().GetCoreComponent(ref state);
                        if (state != null)
                            isPlayerDead = state.dead;
                        isPlayerFind = true;
                    }
                    else
                    {
                        //�^�[�Q�b�g�ƃv���C���[�̊Ԃɕʂ̃I�u�W�F�N�g���������ꍇ
                        isPlayerFind = false;
                    }
                }
            }
            else
            {
                //�p�x���Ɏ��܂��Ă��Ȃ��ꍇ
                isPlayerFind = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isPlayerFind = false;
    }
}

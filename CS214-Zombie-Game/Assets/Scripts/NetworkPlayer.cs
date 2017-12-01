using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{

    PhotonView pv;
    Animator graphicAnim;
    Animator anim;
    float vertical;
    float horizontal;
    bool shouldAttack;
    bool swing02;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (pv.isMine)
        {
            transform.Find("Graphic").gameObject.SetActive(false);
            anim = transform.Find("MainCamera").Find("Arms").GetComponent<Animator>();
        }
        else
        {
            transform.Find("MainCamera").gameObject.SetActive(false);
            GetComponent<PlayerController>().enabled = false;
            graphicAnim = transform.Find("Graphic").GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (!pv.isMine)
        {
            if (graphicAnim)
            {
                graphicAnim.SetFloat("InputY", vertical);
                graphicAnim.SetFloat("InputX", horizontal);
                if (shouldAttack)
                {
                    graphicAnim.SetTrigger("Swing01");
                    shouldAttack = false;
                }
            }

           // transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Input.GetAxis("Vertical"));
            stream.SendNext(Input.GetAxis("Horizontal"));
            stream.SendNext(GetComponent<PlayerController>().isAttacking);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            GetComponent<PlayerController>().isAttacking = false;
        }
        else
        {
            vertical = (float)stream.ReceiveNext();
            horizontal = (float)stream.ReceiveNext();
            shouldAttack = (bool)stream.ReceiveNext();
        }
    }
}

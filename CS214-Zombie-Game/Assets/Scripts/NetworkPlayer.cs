using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {

    PhotonView pv;
    Animator anim;
    float vertical;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
       if(pv.isMine)
        {
            transform.Find("Graphic").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("MainCamera").gameObject.SetActive(false);
            GetComponent<PlayerController>().enabled = false;
            anim = transform.Find("Graphic").GetComponent<Animator>();
        }        
    }

    private void Update()
    {
        if (!pv.isMine)
        {
            if (anim)
            {
                anim.SetFloat("InputY", vertical);
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(Input.GetAxis("Vertical"));
        }
        else
        {
            vertical = (float)stream.ReceiveNext();
        }
    }
}

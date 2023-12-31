using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwordCollider : MonoBehaviourPunCallbacks
{
    //[SerializeField] private GameObject swordFXHit;
    [SerializeField] private float damageAmount = 25;
    public string sender;
    private PhotonView pv;
    public bool openHit = false;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.15f);
        openHit = false;
    }

    [PunRPC]
    public void Set(string sn)
    {
        sender = sn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) 
            return;

        if (collision.gameObject.CompareTag("Enemy") && !openHit)
        {
            openHit = true;
            Debug.Log("Player hit");

            HealthPlayerHandler health = collision.gameObject.GetComponent<HealthPlayerHandler>();

            if (health != null)
            {
                health.photonView.RPC("TakeDamage", RpcTarget.AllBuffered, damageAmount, photonView.Owner.NickName);
                Debug.Log(sender + " sender");
            }

        }

        Debug.Log("Enemy hit");
    }
}

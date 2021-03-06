﻿using UnityEngine;
using Network;

class GamePeer : MonoBehaviour
{
    NetworkPeer _peer;    
    
    void Awake( )
    {
        _peer = new NetworkPeer("game", SharedNet.Instance.MsgMeta );

        _peer.RegisterEvent(NetworkEvent.Connected, msg =>
        {
            var req = new gamedef.EnterGameREQ();
            _peer.SendMsg(req);

        });

        _peer.RegisterMessage<gamedef.EnterGameACK>(msg =>
        {
            Debug.Log("EnterGameACK!");


        });
    }

    public void StartGame( string address )
    {
        _peer.Start(address);
    }

    void OnDisable( )
    {
        if ( _peer != null )
        {
            _peer.Stop();
        }
    }

    void Update( )
    {
        if ( _peer != null )
        {
            _peer.Polling();
        }
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreate;
    public InputField roomName;
    public InputField RoomPerson;
    public Transform RoomContent;

    // �� ����� �����ϱ� ���� �ڷᱸ��
    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if (roomName.text.Length > 0 && RoomPerson.text.Length > 0)
            roomCreate.interactable = true;
        else
            roomCreate.interactable = false;
    }

    // �뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    // RoomCatalog�� ���� ���� Value���� ���ִٸ� RoomInfo�� �־��ݴϴ�.
    public void CreateRoomObject()
    {
        foreach(RoomInfo info in RoomCatalog.Values)
        {
            // ���� �����մϴ�
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContent�� ���� ������Ʈ�� �����մϴ�
            room.transform.SetParent(RoomContent);

            // �� ������ �Է��մϴ�
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}

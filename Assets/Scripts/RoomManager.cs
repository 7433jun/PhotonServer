using Photon.Pun;
using Photon.Realtime;
using System;
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

    public void OnClickCreateRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions room = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        room.MaxPlayers = byte.Parse(RoomPerson.text);

        // ���� ���� ���θ� �����մϴ�.
        room.IsOpen = true;

        // �κ񿡼� �� ����� ���� ��ų�� �����մϴ�.
        room.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomName.text, room);
    }

    public void AllDeleteRoom()
    {
        // Transform ������Ʈ�� �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ������ �õ��մϴ�.
        foreach(Transform trans in RoomContent)
        {
            // Transform�� ������ �ִ� ���� ������Ʈ�� �����մϴ�.
            Destroy(trans.gameObject);
        }
    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    private void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0;i<roomList.Count; i++)
        {
            // �ش� �̸��� RoomCatalog�� key ������ �����Ǿ� �ִٸ�
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) �뿡�� ������ �Ǿ��� ��
                if (roomList[i].RemovedFromList)
                {
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }

            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }
}

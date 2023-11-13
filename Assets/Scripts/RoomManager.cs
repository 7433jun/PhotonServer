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

    // 룸 목록을 저장하기 위한 자료구조
    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if (roomName.text.Length > 0 && RoomPerson.text.Length > 0)
            roomCreate.interactable = true;
        else
            roomCreate.interactable = false;
    }

    // 룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    // RoomCatalog에 여러 개의 Value값이 들어가있다면 RoomInfo에 넣어줍니다.
    public void CreateRoomObject()
    {
        foreach(RoomInfo info in RoomCatalog.Values)
        {
            // 룸을 생성합니다
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContent의 하위 오브젝트로 설정합니다
            room.transform.SetParent(RoomContent);

            // 룸 정보를 입력합니다
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}

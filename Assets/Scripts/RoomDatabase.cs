using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomDatabase : MonoBehaviour
{
    [SerializeField] private List<Room> rooms;
    private Dictionary<string, Room> _dictionary;
    void Awake()
    {
        _dictionary = rooms.ToDictionary(r => r.GetID(), r => r);
    }
    public Room Get(string id)
    {
        return _dictionary[id];
    }

    public (List<string>, string) GetEmptyRoomIDsAndLastVisitedRoomID()
    {
        List<string> allEmptyRooms = new List<string>();
        string lastVisitedRoomByPlayer = string.Empty;

        foreach (var room in rooms)
        {
            if (room.IsEmpty)
            {
                allEmptyRooms.Add(room.GetID());
            }
            if (room.IsPlayerIn)
            {
                lastVisitedRoomByPlayer = room.GetID();
            }
        }
        return (allEmptyRooms, lastVisitedRoomByPlayer);
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "RoomPresets", menuName = "RoomPresets", order = 0)]
    public class RoomsPresents : ScriptableObject
    {
        [SerializeField] private List<RoomData> _roomDatas;

        public List<RoomData> GetRoomVariants => _roomDatas;
    }
}
using System;

namespace WebService.Repository.Context
{
    public partial class Room
    {
        public int RoomId { get; set; }
        public int SettingsId { get; set; }
        public string Name { get; set; }
        public string DeviceEui { get; set; }
    }
}

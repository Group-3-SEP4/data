

namespace WebService.Models
{
    public partial class Room
    {
        public int RoomId { get; set; }
        public int SettingsId { get; set; }
        public string Name { get; set; }
        public string DeviceEui { get; set; }
        public virtual Settings Settings { get; set; }
    }
}

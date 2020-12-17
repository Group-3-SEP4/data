using System;
using System.Linq;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Room
{
    public class RoomDAO : IRoomDAO
    {
        private EnvironmentContext context;
        private int ppmMin;
        private int ppmMax;
        private float temperatureSetPoint;

        public RoomDAO(EnvironmentContext context) {
            this.context = context;
            this.ppmMin = 400;
            this.ppmMax = 5000;
            this.temperatureSetPoint = 18;
        }

        public bool InitRoom(string deviceEUI)
        {
            Context.Measurement measurement = null;
            try
            {
                measurement = context.Measurement.AsEnumerable().Where(r => r.DeviceEui.Equals(deviceEUI)).Last();
            }
            catch (InvalidOperationException e)
            {
            }

            if (measurement != null) {
                Context.Room roomForDevice = null;
            
                try {
                    roomForDevice = context.Room.AsEnumerable().Where(r => r.DeviceEui.Equals(deviceEUI)).Last();
                }
                catch (InvalidOperationException e) {
                }
            
                if (roomForDevice == null){
                    Context.Settings newSettings = new Context.Settings();
                    newSettings.TemperatureSetpoint = temperatureSetPoint;
                    newSettings.PpmMax = ppmMax;
                    newSettings.PpmMin = ppmMin;
                    newSettings.LastUpdated = DateTime.UtcNow;

                    context.Settings.Add(newSettings);
                    context.SaveChanges();

                    Context.Room newRoom = new Context.Room();
                    newRoom.DeviceEui = deviceEUI;
                    newRoom.Name = "New device";
                    newRoom.SettingsId = newSettings.SettingsId;
                    context.Room.Add(newRoom);
                    context.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public Context.Room GetRoom(string deviceEUI) {
            return context.Room.Where(room => room.DeviceEui.Equals(deviceEUI)).Single();
        }

        public Context.Room UpdateRoom(Context.Room room)
        {
            
            var entity = context.Room.FirstOrDefault(item => item.RoomId == room.RoomId);

            if (entity != null)
            {
                entity.Name = room.Name;
                context.Room.Update(entity);
                context.SaveChanges();
            }
            Context.Room newRoom = context.Room.FirstOrDefault(item => item.RoomId == room.RoomId);
            return newRoom;
        }
    }
}

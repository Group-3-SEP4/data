using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using WebService.Repository.Context;

namespace WebService.Repository.DAO.Room
{
    public class RoomDAO : IRoomDAO
    {
        private EnviormentContext context;
        private int ppmMin;
        private int ppmMax;
        private float temperatureSetPoint;

        public RoomDAO(EnviormentContext context)
        {
            this.context = context;
            this.ppmMin = 400;
            this.ppmMax = 5000;
            this.temperatureSetPoint = 18;
        }

        public bool InitRoom(string deviceEUI) {
            Context.Room roomForDevice = null;
            try
            {
                roomForDevice = context.Room.AsEnumerable().Where(r => r.DeviceEui.Equals(deviceEUI)).Last();
            }
            catch (InvalidOperationException e)
            {
            }
            
            if (roomForDevice != null)
                return false;
            else {
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
                return true;
            }
        }

        public Context.Room GetRoom(string deviceEUI)
        {
            return context.Room.Where(room => room.DeviceEui.Equals(deviceEUI)).Single();
        }
    }
}

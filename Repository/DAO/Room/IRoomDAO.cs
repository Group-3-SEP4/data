
namespace WebService.Repository.DAO.Room
{
    interface IRoomDAO
    {
        bool InitRoom(string deviceEUI);
        Context.Room GetRoom(string deviceEUI);
        Context.Room UpdateRoom(Context.Room room);
    }
}

using WebService.Repository.Context;



namespace WebService.Repository.DAO.Room
{
    interface IRoomDAO
    {
        bool InitRoom(string deviceEUI);
        Context.Room GetRoom(string deviceEUI);
    }
}


namespace WebService.Repository.DAO.Settings
{
    interface ISettingsDAO
    {
        Context.Settings GetSettings(string deviceEUI);
        Context.Settings PostSettings(Context.Settings settings);
    }
}

using PlatformService.Models;

namespace PlatformServices.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);

        void CreatePlatform(Platform platform);

    }
}
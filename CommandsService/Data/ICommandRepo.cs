using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        //Platforms
        IEnumerable<Platform>GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int PlatformId);
        bool ExternalPlatformExists(int externalPlatformId);

        //Commands
        IEnumerable<Command> GetCommandsForPlatform(int PlatformId);
        Command GetCommand(int PlatformId,int CommandId);
        void CreateCommand(int PlatformId,Command command);

    }
}
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int PlatformId, Command command)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            command.PlatformId = PlatformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform ==null) throw new ArgumentNullException(nameof(platform));
            /*if(PlatformExists(platform.Id)){
                Console.WriteLine("Already exists");
            }else{*/
                _context.Platforms.Add(platform);
            //}
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _context.Platforms.Any(p=>p.ExternalID==externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms;
        }

        public Command GetCommand(int PlatformId, int CommandId)
        {
            return _context.Commands.Where(c => c.PlatformId == PlatformId && c.Id == CommandId).FirstOrDefault();
            
        }

        public IEnumerable<Command> GetCommandsForPlatform(int PlatformId)
        {
            return _context.Commands.Where(c => c.PlatformId == PlatformId).OrderBy(c=>c.Platform.Name);
        }

        public bool PlatformExists(int PlatformId)
        {
            return _context.Platforms.Any(p=>p.Id==PlatformId);
        }

        public bool SaveChanges()
        {
            return(_context.SaveChanges()>=0);
        }
    }
}
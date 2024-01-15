using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModel;

namespace GameZone.Services
{
    public class GamesServices : IGameServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public GamesServices(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public IEnumerable<Games> GetAll()
        => _dbContext.Games.Include(G=>G.Category)
            .Include(G=>G.GameDevices)
            .ThenInclude(D=>D.Device)
            .AsNoTracking()
            .ToList();

        public Games? GetById(int id)
        => _dbContext.Games.Include(g => g.Category)
            .Include(g => g.GameDevices)
            .ThenInclude(d => d.Device)
            .AsNoTracking()
            .FirstOrDefault(g => g.Id == id);

        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Games game = new Games() 
            {
                Name= model.Name,
                Description=model.Description,
                CategoryId=model.CategoryId,
                Cover=coverName,
                GameDevices=model.SelectedDevices.Select(d=>new GameDevice {DeviceId=d }).ToList()
            }; 
            _dbContext.Games.Add(game);
            _dbContext.SaveChanges();
        }

        public async Task<Games?> Update(EditGameFormViewModel model)
        {
            var game = _dbContext.Games
                .Include(G=>G.GameDevices)
                .FirstOrDefault(G=>G.Id==model.Id);
            if(game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name= model.Name;
            game.Description= model.Description;
            game.CategoryId= model.CategoryId;
            game.GameDevices = model.SelectedDevices.Select(D=>new GameDevice {DeviceId=D }).ToList();
            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

          var effetedRows=  _dbContext.SaveChanges();
            if (effetedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else 
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;

            }
               


        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _dbContext.Games.Find(id);
            if (game is null)
                return isDeleted;

            _dbContext.Games.Remove(game);

            var effectedRows=_dbContext.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover); 
            }

            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }

        
    }
}

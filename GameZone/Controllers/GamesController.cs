using GameZone.Services;
using GameZone.ViewModel;


namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoiresService _categoiresService;
        private readonly IDevicesService _devicesService;
        private readonly IGameServices _gamesServices;

        public GamesController(ICategoiresService categoiresService, IDevicesService devicesService, IGameServices gamesServices)
        {
            _categoiresService = categoiresService;
            _devicesService = devicesService;
            _gamesServices = gamesServices;
        }
        public IActionResult Index()
		{
			var games = _gamesServices.GetAll();
			return View(games);
		}

        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetById(id);
            if (game is null)
                return NotFound();
            return View(game   );
        }

        [HttpGet]
        public IActionResult Create()
        {


            CreateGameFormViewModel viewModel = new CreateGameFormViewModel()
            {
                Categories = _categoiresService.GetSelectList(),
                Devices = _devicesService.GetListItems()
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoiresService.GetSelectList();
                model.Devices = _devicesService.GetListItems();
                return View(model);
            }

             await _gamesServices.Create(model);

            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesServices.GetById(id);
            if(game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new EditGameFormViewModel()
            {
                Id=id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.GameDevices.Select(D=>D.DeviceId).ToList(),
                Categories=_categoiresService.GetSelectList(),
                Devices = _devicesService.GetListItems(),
                CurrentCover=game.Cover

            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model )
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoiresService.GetSelectList();
                model.Devices = _devicesService.GetListItems();
                return View(model);
            }
            var game=await _gamesServices.Update(model);
            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted=_gamesServices.Delete(id);


            return isDeleted ? Ok() : BadRequest() ;
        }
    }
}

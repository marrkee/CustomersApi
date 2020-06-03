namespace Customers.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Customers.Persistence.Models;
    using Customers.Persistence.Models.DataModels;
    using Customers.Persistence.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [ApiController]
    [Route("[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly IManagersService managersService;
        private readonly IMapper mapper;

        public ManagersController(IManagersService managersService, IMapper mapper)
        {
            this.managersService = managersService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ManagersViewModel>>> GetManagers([FromQuery] ManagersPagingParameters managersPagingParameters)
        {
            var managers = await this.managersService.GetManagers(managersPagingParameters);

            if (managers == null)
            {
                return this.StatusCode(402);
            }

            var metadata = new
            {
                managers.TotalCount,
                managers.PageSize,
                managers.CurrentPage,
                managers.TotalPages,
                managers.HasNext,
                managers.HasPrevious,
            };
            this.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            this.Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            return managers;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ManagersViewModel>> GetManager(int id)
        {
            var manager = this.mapper.Map<Manager, ManagersViewModel>(await this.managersService.GetManager(id));
            if (manager == null)
            {
                return this.StatusCode(404);
            }

            return manager;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateNewManager([FromBody] ManagersViewModel manager)
        {
            if (manager == null)
            {
                return this.BadRequest();
            }

            await this.managersService.CreateManager(this.mapper.Map<ManagersViewModel, Manager>(manager));
            return this.Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateManager([FromBody] ManagersViewModel manager)
        {
            if (manager == null)
            {
                return this.BadRequest();
            }

            await this.managersService.UpdateManager(this.mapper.Map<ManagersViewModel, Manager>(manager));
            return this.Ok();
        }
    }
}
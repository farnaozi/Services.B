using AutoMapper;
using Services.B.Core.Dtos;
using Services.B.Core.Interfaces;
using Services.B.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IServiceRepo _repo;
        private readonly IMapper _mapper;

        public HomeController(IServiceRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("ForwardMessage")]
        public async Task<IActionResult> ForwardMessage(string message)
        {
            return await Task.FromResult(Ok(_repo.ForwardMessage(message)));
        }
    }
}
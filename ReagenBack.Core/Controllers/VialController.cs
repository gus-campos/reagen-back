using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VialsController(
    VialService crudService
) : CrudControllerBase<Vial, VialCreateDto, VialReadDto>(crudService)
{}
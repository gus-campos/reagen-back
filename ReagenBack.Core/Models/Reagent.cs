
using System.Text.Json;

namespace ReagenBack.Core.Models;

public class Reagent : IWithId
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Dimension Dimension { get; set; }

    public int? ControlAgencyId { get; set; }
    public ControlAgency? ControlAgency { get; set; }
}

public class ReagentCreateDto
{
    public string Name { get; set; } = default!;
    public Dimension Dimension { get; set; }
    public int? ControlAgencyId { get; set; }
}

public class ReagentReadDto : IWithId
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Dimension Dimension { get; set; }
    public ControlAgencyReadDto? ControlAgency { get; set; }
}


public class ReagentMapper(
    ControlAgencyMapper controlAgencyMapper
) : IMapper<Reagent, ReagentCreateDto, ReagentReadDto>
{
    public Reagent ToEntity(ReagentCreateDto dto, int id)
    {    
        return new()
        {    
            Id = id,
            Name = dto.Name,
            Dimension = dto.Dimension,
            ControlAgencyId = dto.ControlAgencyId
        };
    } 

    public ReagentReadDto ToReadDto(Reagent reagent)
    {
        return new() 
        {    
            Id = reagent.Id,
            Name = reagent.Name,
            Dimension = reagent.Dimension,
            ControlAgency = reagent.ControlAgency == null 
                ? null 
                : controlAgencyMapper.ToReadDto(reagent.ControlAgency)
        };
    }
}
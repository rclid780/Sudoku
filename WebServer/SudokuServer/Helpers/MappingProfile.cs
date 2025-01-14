using AutoMapper;
using SudokuServer.Models;

namespace SudokuServer.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PuzzleDTO, Puzzle>().ConvertUsing<Dto2Puzzle>();
        CreateMap<Puzzle, PuzzleDTO>().ConvertUsing<Puzzle2Dto>();
        
        CreateMap<Solution, SolutionDTO>().ConvertUsing<Solution2Dto>();
    }
}
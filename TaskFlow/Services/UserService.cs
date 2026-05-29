using AutoMapper;
using TaskFlow.DTOs.User;
using TaskFlow.Exceptions;
using TaskFlow.Models;
using TaskFlow.Models.Enums;
using TaskFlow.Repositories.Interfaces;
using TaskFlow.Services.Interfaces;

namespace TaskFlow.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException($"User with id {id} not found");
        
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new BadRequestException("Email already exists");

        var user = _mapper.Map<User>(dto);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        user.IsActive = true;
        user.CreatedAt = DateTime.UtcNow;
        user.Role = Enum.Parse<UserRole>(dto.Role);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException($"User with id {id} not found");

        user.FullName = dto.FullName;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException($"User with id {id} not found");

        await _unitOfWork.Users.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
using TaskFlow.DTOs.Auth;

namespace TaskFlow.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto requestDto);
}
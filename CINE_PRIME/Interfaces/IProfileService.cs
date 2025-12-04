using CINE_PRIME.ViewModels;

namespace CINE_PRIME.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileVM?> GetProfileAsync(string userId);
        Task<(bool Success, string ErrorMessage)> UpdateProfileAsync(ProfileVM model, IFormFile? profilePicture);
        Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(ProfileVM model);

    }
}

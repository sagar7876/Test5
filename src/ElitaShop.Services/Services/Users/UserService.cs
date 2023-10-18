﻿using ElitaShop.Domain.Exceptions.Users;
using ElitaShop.Services.Interfaces.Users;

namespace ElitaShop.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UserService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFileService fileService)
        {
            this._userRepository = unitOfWork.UserRepository;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._fileService = fileService;
        }

        public async Task<bool> CreateAsync(UserCreateDto userCreateDto)
        {
            string imagePath = await _fileService.UploadAvatarAsync(userCreateDto.UserAvatar);

            User user = _mapper.Map<User>(userCreateDto);
            user.UserAvatar = imagePath;

            await _userRepository.AddAsync(user);
            int result = await _unitOfWork.CommitAsync();

            return result != 0;

        }

        public async Task<bool> DeleteAsync(long userId)
        {
            User user = await _userRepository.GetAsync(user => user.Id == userId);
            if (user is null) throw new UserNotFoundException();

            var image = await _fileService.DeleteAvatarAsync(user.UserAvatar);
            if (image == false) throw new ImageNotFoundException();

            _userRepository.Remove(user);
            int result = await _unitOfWork.CommitAsync();

            return result > 0;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.ToList();
        }

        public async Task<User> GetByIdAsync(long userId)
        {
            User user = await _userRepository.GetAsync(user => user.Id == userId);
            if (user is null) throw new UserNotFoundException();

            return user;
        }

        public async Task<UserGetDto> GetImageAsync(long userId)
        {
            var user = await GetByIdAsync(userId);
            var userget = _mapper.Map<UserGetDto>(user);
            userget.UserAvatar = await _fileService.LoadImageAsync(user.UserAvatar);
            return userget;
        }

        public async Task<bool> UpdateAsync(long userId, UserUpdateDto userUpdateDto)
        {
            User user = await  _userRepository.GetAsync(user => user.Id == userId);
            if (user is null) throw new UserNotFoundException();

            User userMap = _mapper.Map<User>(userUpdateDto);
            userMap.Id = userId;

            if(userUpdateDto.UserAvatar is not null)
            {
                var image = await _fileService.DeleteAvatarAsync(user.UserAvatar);
                if(image == false) throw new ImageNotFoundException();

                string newImagePath = await _fileService.UploadImageAsync(userUpdateDto.UserAvatar);
                userMap.UserAvatar = newImagePath;
            }

            userMap.UpdatedAt = DateTime.UtcNow;

            _userRepository.Update(userMap);
            int result = await _unitOfWork.CommitAsync();

            return result > 0;
        }
    }
}

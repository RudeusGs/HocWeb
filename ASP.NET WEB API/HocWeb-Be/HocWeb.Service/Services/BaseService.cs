using HocWeb.Infrastructure;
using HocWeb.Service.Common.IServices;

namespace HocWeb.Service.Services
{
    public abstract class BaseService
    {
        protected readonly DataContext _dataContext;
        protected readonly DateTime _now = DateTime.UtcNow.AddHours(7);
        protected readonly IUserService _userService;
        protected readonly string _userName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public BaseService(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _userName = _userService.UserName;
        }
    }
}

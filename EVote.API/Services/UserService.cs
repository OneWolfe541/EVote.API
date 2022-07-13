using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EVote.API.Context;
using EVote.API.Models;
using EVote.API.ViewModels;

namespace EVote.API.Services
{
    public class UserService
    {
        public async Task<UserModel> CompareAsync(ElectionContext context, UserModel model)
        {
            if (model == null)
                throw new Exception();

            EVoteHashService _hashService = new EVoteHashService("YouWillNeverGuess");

            // Find first user with matching name
            var userItem = await context.Locations.Where(l => l.LocationName.ToUpper() == model.UserName.ToUpper() && l.Active == true).Select(u => new UserModel
                            {
                                UserId = u.LocationId,
                                UserName = u.LocationName,
                                Login = u.Login.ToUpper(),
                                RollId = u.RollId
                            }
                            )
                            .FirstOrDefaultAsync();

            UserModel user = new UserModel();

            // Validate matching user
            if (userItem != null)
            {
                if(_hashService.GenerateHash(userItem.Login.ToUpper(), "WormholeExtreme", 100) == model.Login)
                {
                    user = userItem;
                    user.Login = null;
                }
            }

            return user;
        }
    }
}

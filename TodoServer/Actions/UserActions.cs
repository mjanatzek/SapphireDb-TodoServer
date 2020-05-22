using System;
using System.Threading.Tasks;
using SapphireDb.Actions;

namespace TodoServer.Actions
{
    public class UserActions : ActionHandlerBase
    {
        private readonly JwtIssuer _jwtIssuer;

        public UserActions(JwtIssuer jwtIssuer)
        {
            _jwtIssuer = jwtIssuer;
        }
        
        public async Task<string> Login(Guid id, string username)
        {
            return await _jwtIssuer.GenerateEncodedToken(id, username);
        }
    }
}
using GameShowcase.Models;
using GameShowcase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameShowcase.Data
{
    public class CurrentAccountData
    {
        private HttpContext HttpContext { get; set; }

        public CurrentAccountData(Controller controller)
        {
            HttpContext = controller.HttpContext;
        }

        public bool LoggedIn => IsAdmin || IsLeader;

        public bool IsAdmin {
            get => HttpContext.Session.Get<bool>(SessionKeyIsAdmin);
            set => HttpContext.Session.Set(SessionKeyIsAdmin, value);
        }

        public string LeaderStuNum {
            get => HttpContext.Session.Get<string>(SessionKeyLeaderStuNum);
            set => HttpContext.Session.Set(SessionKeyLeaderStuNum, value);
        }

        public bool IsLeader => !string.IsNullOrEmpty(LeaderStuNum);

        public int LoggedId {
            get => HttpContext.Session.Get<int>(SessionKeyLoggedId);
            set => HttpContext.Session.Set(SessionKeyLoggedId, value);
        }

        public void SignOff()
        {
            IsAdmin = false;
            LeaderStuNum = null;
        }

        public void AdminLogIn(Admin admin)
        {
            IsAdmin = true;
            LeaderStuNum = null;
            LoggedId = admin.Id;
        }

        public void LeaderLogin(Leader leader)
        {
            IsAdmin = false;
            LeaderStuNum = leader.StudentNumber;
            LoggedId = leader.Id;
        }

        private const string SessionKeyIsAdmin = "_IsAdmin";
        private const string SessionKeyLeaderStuNum = "_LeaderStuNum";
        private const string SessionKeyLoggedId = "_LoggedId";
    }
}

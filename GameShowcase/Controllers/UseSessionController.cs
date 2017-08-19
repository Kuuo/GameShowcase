using GameShowcase.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.Controllers
{
    public class UseSessionController : Controller
    {
        private CurrentAccountData _CurrentAccountData;

        protected CurrentAccountData CurrentAccountData {
            get {
                if (_CurrentAccountData == null)
                    _CurrentAccountData = new CurrentAccountData(this);

                return _CurrentAccountData;
            }
        }
    }
}

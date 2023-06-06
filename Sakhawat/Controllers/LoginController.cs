using Microsoft.AspNetCore.Mvc;
using Sakhawat.Constract.IRepository;
using Sakhawat.Data;
using Sakhawat.Model.Dtos;
using Sakhawat.Model.Models;

namespace Sakhawat.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork context;

        private readonly ApplicationContext appCon;

        public LoginController(IUnitOfWork context, ApplicationContext _applicationContext)
        {
            this.context = context;
            this.appCon = _applicationContext;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        //GET LOGIN
        [HttpGet]
        public IActionResult Dashboard()
        {
            //IEnumerable<SecBUserInfo> userInfos = context.SecBUserInfo.GetAll();
            //return View(userInfos);

            var MainNavBarList=from a in appCon.SecDScreenInfos.Where(x=>x.ParentId==0)
                               join b in appCon.SecELinkUserGroupScreenInfos on a.ScreenId equals b.ScreenId into t1
                               from b in t1.DefaultIfEmpty()
                               join c in appCon.SecAUserGroupInfos on b.UserGroupId equals c.UserGroupId into t2
                               from c in t2.DefaultIfEmpty()
                               join d in appCon.SecBUserInfos.Where(x=>x.UserId==1) on c.UserGroupId equals d.UserGroupId into t3
                               from d in t3.DefaultIfEmpty()
                               join e in appCon.SoftConfigIModuleInfos on a.ModuleId equals e.ModuleId into t4
                               from e in t4.DefaultIfEmpty()
                               join f in appCon.SoftConfigJCompanyLinkModules on e.ModuleId equals f.ModuleId into t5
                               from f in t5.DefaultIfEmpty()
                               join g in appCon.CompACompanyInfos.Where(x=>x.CompanyId==1) on f.CompanyId equals g.CompanyId into t6
                               from g in t6.DefaultIfEmpty()

        }

        // GET LOGIN
        [HttpPost]
        public JsonResult LoginUser(LoginDto model)
        {
            if (model != null)
            {
                SecBUserInfo userInfo = context.SecBUserInfo.GetFirstOrDefault(u => u.LoginNameEnglish.Trim().ToLower() == model.LoginNameEnglish.Trim().ToLower() && u.Password == model.Password);
                if (userInfo == null)
                {
                    return Json("Invalid userid or password");
                }

                if (context.UserBranchInfo.GetFirstOrDefault(u => u.UserId == userInfo.UserId && u.BranchId == model.BranchId) != null)
                {
                    return Json(new { });
                }
                else
                {
                    return Json("User do not have the branch access!");
                }
            }
            return Json("Invalid userid or password");
        }


        /// <summary>
        /// Get All Branches
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllBranch()
        {
            IEnumerable<CompBBranchInfo> branchInfos = context.CompBBranchInfo.GetAll();
            return Json(branchInfos);
        }
    }
}
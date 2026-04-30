using Microsoft.AspNetCore.Mvc;
using MSLX.SDK.IServices;
using MSLX.SDK.Models;
using Newtonsoft.Json.Linq;

namespace MSLX.Plugin.Demo.Controllers;

[ApiController]
// 路由规范：api/plugin/{plugin-id}/[controller]
[Route("api/plugins/mslx-plugin-demo/demo")]
public class DemoController : ControllerBase
{
    private readonly IMCServerService _serverService;

    public DemoController(IMCServerService serverService)
    {
        _serverService = serverService;
    }

    [HttpGet]
    public IActionResult GetServers()
    {
        var running = _serverService.HasRunningServers();
        return Ok(new ApiResponse<object>()
        {
            Code = 200,
            Message = "用户信息错误",
            Data = new JObject() {
                ["hostStatus"] = running ? "有服务器火热运行中" : "没有服务器实例运行中",
                ["plugin"] = "mslx-plugin-demo",
            }
        });
    }
}